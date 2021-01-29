using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;

namespace WeatherHistoryViewer.Services
{
    public interface IWeatherDataHandler
    {
        public void AddHistoricalWeatherToDb(string cityName, string date, HourlyInterval hourlyInterval);
        public List<HistoricalWeather> GetAllWeatherHistory(string cityName);
    }

    public class WeatherDataHandlerHandler : IWeatherDataHandler
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomWeatherClassConverter _customWeatherClassConverter;
        private readonly IApiRequester _requester;
        private readonly ISecretRevealer _secretRevealer;

        public WeatherDataHandlerHandler(ApplicationDbContext context, ISecretRevealer secretRevealer,
            IApiRequester requester, ICustomWeatherClassConverter customWeatherClassConverter)
        {
            _context = context;
            _secretRevealer = secretRevealer;
            _requester = requester;
            _customWeatherClassConverter = customWeatherClassConverter;
        }

        public void AddHistoricalWeatherToDb(string cityName, string date, HourlyInterval hourlyInterval)
        {
            if (_context.Weather.Any() && _context.Weather.Include(o => o.Location)
                .FirstOrDefault(w => w.Date == date && w.Location.Name == cityName) != null) return;
            var secrets = _secretRevealer.RevealUserSecrets();
            var response =
                _requester.GetHistoricalWeather(secrets.ApiKeys.WeatherStack, cityName, date, hourlyInterval);
            var weatherModel =
                _customWeatherClassConverter.ToHistoricalWeatherModelConverter(response, date, hourlyInterval);

            _context.Weather.Add(weatherModel);

            _context.Database.OpenConnection();
            _context.SaveChanges();
            _context.Database.CloseConnection();
        }

        public List<HistoricalWeather> GetAllWeatherHistory(string cityName)
        {
            var returnList = _context.Weather.ToList();
            return returnList;
        }
    }
}