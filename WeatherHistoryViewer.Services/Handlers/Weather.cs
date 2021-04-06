using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeatherHistoryViewer.Core.Constants;
using WeatherHistoryViewer.Core.Models.DataWarehouse;
using WeatherHistoryViewer.Db;
using WeatherHistoryViewer.Services.Converter;
using WeatherHistoryViewer.Services.Helpers;
using WeatherHistoryViewer.Services.Requester;

namespace WeatherHistoryViewer.Services.Handlers
{
    public class WeatherHandler
    {
        private readonly WeatherStackApi _apiRequester;
        private readonly DatabaseHandler _database;
        private readonly DateHelper _dateHelper;
        private readonly WeatherModelConverter _weatherModelConverter;

        public WeatherHandler()
        {
            _apiRequester = new WeatherStackApi();
            _weatherModelConverter = new WeatherModelConverter();
            _dateHelper = new DateHelper();
            _database = new DatabaseHandler();
        }

        public void UpdateWeatherToDb(string cityName, string date)
        {
            using var context = new ApplicationDbContext();

            try
            {
                var weatherStackApiKey = UserSecrets.WeatherStackApiKey;

                var response =
                    _apiRequester.GetHistoricalWeather(weatherStackApiKey, cityName, date);
                if (response?.Historical != null)
                {
                    var weatherModel =
                        _weatherModelConverter.ToHistoricalWeatherModelConverter(response, date);
                    _database.AddHistoricalWeather(weatherModel);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateHistoricalWeatherRangeToDb(string locationName,
            string oldestDate = null, string newestDate = null)
        {
            var dateList = new List<string>();
            if (oldestDate == null)
            {
                dateList = _dateHelper.GetAllDates();
            }
            else
            {
                dateList = !_dateHelper.IsDateOlderThenOldestDate(oldestDate)
                    ? _dateHelper.GetRangeOfDates(oldestDate, newestDate)
                    : _dateHelper.GetRangeOfDates(newestDateString: newestDate);
            }

            using var context = new ApplicationDbContext();
            var excludedDates = new HashSet<string>(context.Weather.Include(w => w.Location)
                .Where(w => w.Location.Name == locationName).Select(w => w.Date));
            dateList = dateList.Where(p => !excludedDates.Contains(p)).ToList();

            foreach (var date in dateList)
            {
                Debug.WriteLine(
                    $"Place: {locationName}; Day: {date}; ExecutedTime: {DateTime.Now.Minute}:{DateTime.Now.Second}");
                UpdateWeatherToDb(locationName, date);
            }
        }

        public void UpdateAllSavedHistoricalWeather()
        {
            try
            {
                var dateHelper = new DateHelper();
                var oldestDate = dateHelper.GetDateStringOfDaysAgo(WeatherConstants.OldestDaysAgo);
                var newestDate = dateHelper.GetDateStringOfDaysAgo(WeatherConstants.NewestDaysAgo);
                var locations = new LocationHandler().GetLocationNames();
                Task.Run(() =>
                {
                    foreach (var locationName in locations)
                        new WeatherHandler().UpdateHistoricalWeatherRangeToDb(locationName,
                            oldestDate,
                            newestDate);
                });

                if (new WeatherHelper().DoesWeatherWarehouseNeedToBeUpdated(MinCachingDaysBeforeUpdatingWeatherDb.WeekPage))
                    new DataWarehouseHandlers().UpdateWeatherWarehouse();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}