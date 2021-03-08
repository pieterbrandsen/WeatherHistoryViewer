using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;
using WeatherHistoryViewer.Services.Converter;
using WeatherHistoryViewer.Services.Helper;
using WeatherHistoryViewer.Services.Requester;

namespace WeatherHistoryViewer.Services.Handlers
{
    public class WeatherHandler
    {
        private readonly WeatherStackAPI _apiRequester;
        private readonly Database _database;
        private readonly DateHelper _dateHelper;
        private readonly WeatherModelConverter _weatherModelConverter;

        public WeatherHandler()
        {
            _apiRequester = new WeatherStackAPI();
            _weatherModelConverter = new WeatherModelConverter();
            _dateHelper = new DateHelper();
            _database = new Database();
        }

        private bool DoesDateAndCityExistInDb(string cityName, string date)
        {
            using var context = new ApplicationDbContext();
            if (context.Weather.Any() && context.Weather.Include(o => o.Location)
                .FirstOrDefault(w => w.Date == date && w.Location.Name == cityName) != null) return true;
            return false;
        }

        public void UpdateWeatherToDb(string cityName, string date, HourlyInterval hourlyInterval)
        {
            using var context = new ApplicationDbContext();

            try
            {
                var weatherStackApiKey = UserSecrets.WeatherStackApiKey;

                if (DoesDateAndCityExistInDb(cityName, date)) return;
                var response =
                    _apiRequester.GetHistoricalWeather(weatherStackApiKey, cityName, date, hourlyInterval);
                var weatherModel =
                    _weatherModelConverter.ToHistoricalWeatherModelConverter(response, date, hourlyInterval);

                _database.AddHistoricalWeather(weatherModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateHistoricalWeatherRangeToDb(string cityName,
            HourlyInterval hourlyInterval = HourlyInterval.Hours3, string oldestDate = null, string newestDate = null)
        {
            var dateList = oldestDate == null
                ? _dateHelper.GetAllRequestableDates()
                : _dateHelper.GetRangeOfRequestableDates(oldestDate, newestDate);


            foreach (var date in dateList)
            {
                Debug.WriteLine(
                    $"Place: {cityName}; Day: {date}; ExecutedTime: {DateTime.Now.Minute}:{DateTime.Now.Second}");
                UpdateWeatherToDb(cityName, date, hourlyInterval);
            }
        }

        public List<HistoricalWeather> GetWeatherOfDateInLast15Y(string cityName, string date)
        {
            using var context = new ApplicationDbContext();
            try
            {
                var dates = _dateHelper.GetDateInLast15Y(date);
                context.Weather.AsNoTracking();
                context.Locations.AsNoTracking();
                context.WeatherHourly.AsNoTracking();
                var weather = context.Weather.Include(w => w.Location).Include(w => w.SnapshotsOfDay)
                    .Where(w => w.Location.Name == cityName && dates.Contains(w.Date))
                    .OrderByDescending(o => o.DateEpoch).ToList();
                return weather;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}