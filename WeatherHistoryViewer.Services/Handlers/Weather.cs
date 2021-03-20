using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;
using WeatherHistoryViewer.Services.Converter;
using WeatherHistoryViewer.Services.Helpers;
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


            foreach (var date in dateList)
            {
                Debug.WriteLine(
                    $"Place: {cityName}; Day: {date}; ExecutedTime: {DateTime.Now.Minute}:{DateTime.Now.Second}");
                UpdateWeatherToDb(cityName, date, hourlyInterval);
            }
        }
    }
}