using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;

namespace WeatherHistoryViewer.Services.Handlers
{
    public interface IWeatherData
    {
        public void UpdateWeatherToDb(string cityName, string date, HourlyInterval hourlyInterval);

        public void UpdateHistoricalWeatherRangeToDb(string cityName,
            HourlyInterval hourlyInterval = HourlyInterval.Hours3, string oldestDate = null, string newestDate = null);

        public List<HistoricalWeather> GetWeatherOfDateInLast10Y(string cityName, string date);
    }

    public class WeatherData : IWeatherData
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly ICustomWeatherClassConverter _customWeatherClassConverter;
        private readonly IDatabase _database;
        private readonly IDateData _dateData;
        private readonly IApiRequester _requester;
        private readonly ISecretRevealer _secretRevealer;

        public WeatherData(IDbContextFactory<ApplicationDbContext> contextFactory, ISecretRevealer secretRevealer,
            IApiRequester requester, ICustomWeatherClassConverter customWeatherClassConverter, IDateData dateData,
            IDatabase database)
        {
            _contextFactory = contextFactory;
            _secretRevealer = secretRevealer;
            _requester = requester;
            _customWeatherClassConverter = customWeatherClassConverter;
            _dateData = dateData;
            _database = database;
        }

        public void UpdateWeatherToDb(string cityName, string date, HourlyInterval hourlyInterval)
        {
            using var context = _contextFactory.CreateDbContext();
                var weatherStackApiKey = _secretRevealer.RevealWeatherStackApiKey();
                if (context.Weather.Any() && context.Weather.Include(o => o.Location)
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
                finally
                {
                    context.Dispose();
                }
        }

        public void UpdateHistoricalWeatherRangeToDb(string cityName,
            HourlyInterval hourlyInterval = HourlyInterval.Hours3, string oldestDate = null, string newestDate = null)
        {
            var dateList = oldestDate == null
                ? _dateData.GetAllRequestableDates()
                : _dateData.GetRangeOfRequestableDates(oldestDate, newestDate);


            foreach (var date in dateList)
            {
                Debug.WriteLine($"Place: {cityName}; Day: {date}; ExecutedTime: {DateTime.Now.Minute}:{DateTime.Now.Second}");
                UpdateWeatherToDb(cityName, date, hourlyInterval);
            }
        }

        public List<HistoricalWeather> GetWeatherOfDateInLast10Y(string cityName, string date)
        {
            using var context = _contextFactory.CreateDbContext();
            try
            {
                var dates = _dateData.GetDateInLast10Y(date);
                int index1 = dates.IndexOf(dates[2]);
                int index2 = dates.IndexOf("d");
                var weather = context.Weather.Include(w => w.Location).Include(w => w.SnapshotsOfDay)
                    .Where(w => w.Location.Name == cityName && dates.Contains(w.Date)).ToList();
                return weather;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                context.Dispose();
            }
        }
    }
}