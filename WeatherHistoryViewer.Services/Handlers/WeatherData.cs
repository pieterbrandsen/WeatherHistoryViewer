using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;

namespace WeatherHistoryViewer.Services.Handlers
{
    public interface IWeatherData
    {
        public void AddWeatherToDb(string cityName, string date, HourlyInterval hourlyInterval);

        public void AddHistoricalWeatherRangeToDb(string cityName,
            HourlyInterval hourlyInterval = HourlyInterval.Hours3, string oldestDate = null, string newestDate = null);
    }

    public class WeatherData : IWeatherData
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomWeatherClassConverter _customWeatherClassConverter;
        private readonly IDatabase _database;
        private readonly IDateData _dateData;
        private readonly IApiRequester _requester;
        private readonly ISecretRevealer _secretRevealer;

        public WeatherData(ApplicationDbContext context, ISecretRevealer secretRevealer,
            IApiRequester requester, ICustomWeatherClassConverter customWeatherClassConverter, IDateData dateData,
            IDatabase database)
        {
            _context = context;
            _secretRevealer = secretRevealer;
            _requester = requester;
            _customWeatherClassConverter = customWeatherClassConverter;
            _dateData = dateData;
            _database = database;
        }

        public void AddWeatherToDb(string cityName, string date, HourlyInterval hourlyInterval)
        {
                var weatherStackApiKey = _secretRevealer.RevealWeatherStackApiKey();
            if (_context.Weather.Any() && _context.Weather.Include(o => o.Location)
                .FirstOrDefault(w => w.Date == date && w.Location.Name == cityName) != null) return;

            try
            {
                var response =
                    _requester.GetHistoricalWeather(weatherStackApiKey, cityName, date, hourlyInterval);
                var weatherModel =
                    _customWeatherClassConverter.ToHistoricalWeatherModelConverter(response, date, hourlyInterval);

                _database.AddHistoricalWeather(weatherModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void AddHistoricalWeatherRangeToDb(string cityName,
            HourlyInterval hourlyInterval = HourlyInterval.Hours3, string oldestDate = null, string newestDate = null)
        {
            var dateList = oldestDate == null
                ? _dateData.GetAllRequestableDates()
                : _dateData.GetRangeOfRequestableDates(oldestDate, newestDate);


            foreach (var date in dateList)
            {
                Debug.WriteLine($"Place: {cityName}; Day: {date}; ExecutedTime: {DateTime.Now.Minute}:{DateTime.Now.Second}");
                AddWeatherToDb(cityName, date, hourlyInterval);
            }
        }
    }
}