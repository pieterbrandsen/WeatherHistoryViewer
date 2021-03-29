using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WeatherHistoryViewer.Db;
using WeatherHistoryViewer.Config;
using WeatherHistoryViewer.Services.Converter;
using WeatherHistoryViewer.Services.Helpers;
using WeatherHistoryViewer.Services.Requester;
using Microsoft.EntityFrameworkCore;
using WeatherHistoryViewer.Core.Models.DataWarehouse;

namespace WeatherHistoryViewer.Services.Handlers
{
    public class WeatherHandler
    {
        private readonly WeathertackAPI _apiRequester;
        private readonly DatabaseHandler _database;
        private readonly DateHelper _dateHelper;
        private readonly WeatherModelConverter _weatherModelConverter;

        public WeatherHandler()
        {
            _apiRequester = new WeathertackAPI();
            _weatherModelConverter = new WeatherModelConverter();
            _dateHelper = new DateHelper();
            _database = new DatabaseHandler();
        }

        public void UpdateWeatherToDb(string cityName, string date)
        {
            using var context = new ApplicationDbContext();

            try
            {
                var WeathertackApiKey = UserSecrets.WeathertackApiKey;

                var response =
                    _apiRequester.GetHistoricalWeather(WeathertackApiKey, cityName, date);
                if (response.Historical != null)
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
            if(oldestDate == null)
            {
                dateList = _dateHelper.GetAllRequestableDates();
            }
            else
            {
                if (!_dateHelper.IsDateOlderThenOldestDate(oldestDate)) dateList = _dateHelper.GetRangeOfRequestableDates(oldestDate, newestDate);
                else dateList = _dateHelper.GetRangeOfRequestableDates(newestDateString:newestDate);
            }

            using var context = new ApplicationDbContext();
            var excludedDates = new HashSet<string>(context.Weather.Include(w=>w.Location).Where(w=>w.Location.Name == locationName).Select(w => w.Date));
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
                var oldestDate = dateHelper.GetDateStringOfDaysAgo(365);
                var newestDate = dateHelper.GetDateStringOfDaysAgo(WebConfig.NewestDaysAgo);
                var locations = new LocationHandler().GetLocationNames();
                Task.Run(() =>
                {
                    foreach (var locationName in locations)
                    {
                        new WeatherHandler().UpdateHistoricalWeatherRangeToDb(locationName,
                            oldestDate,
                            newestDate);
                    }
                });

                if (new WeatherHelper().DoesWeatherWarehouseNeedToBeUpdated(MinDaysBeforeUpdatingWeather.Week))
                    new DataWarehouseHandlers().UpdateWeatherWarehouse();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}