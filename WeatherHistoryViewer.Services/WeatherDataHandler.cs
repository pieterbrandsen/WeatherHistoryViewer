using System.Collections.Generic;
using System.Linq;
using WeatherHistoryViewer.Core.Models;
using WeatherHistoryViewer.Db;

namespace WeatherHistoryViewer.Services
{
    public interface IWeatherDataHandler
    {
        public void AddCurrentWeatherToDB();
        public List<WeatherModel> GetAllWeatherModels();
    }

    public class WeatherDataHandlerHandler : IWeatherDataHandler
    {
        private readonly ApplicationDbContext _context;
        private readonly IRequester _requester;
        private readonly ISecretRevealer _secretRevealer;

        public WeatherDataHandlerHandler(ApplicationDbContext context, ISecretRevealer secretRevealer, IRequester requester)
        {
            _context = context;
            _secretRevealer = secretRevealer;
            _requester = requester;
        }

        public void AddCurrentWeatherToDB()
        {
            var secrets = _secretRevealer.RevealUserSecrets();
            var response = _requester.GetCurrentWeather(secrets.ApiKeys.WeatherStack);

            var weather = CreateNewWeatherModel(response);
            _context.Weather.Add(weather);
            _context.SaveChanges();
        }

        private WeatherModel CreateNewWeatherModel(CurrentWeatherHTTPResponse response)
        {
            var location = new LocationWKey();
            var responseLocation = response.Location;
            location.Country = responseLocation.Country;
            location.Lat = responseLocation.Lat;
            location.Localtime = responseLocation.Localtime;
            location.LocaltimeEpoch = responseLocation.LocaltimeEpoch;
            location.Lon = responseLocation.Lon;
            location.Name = responseLocation.Name;
            location.Region = responseLocation.Region;
            location.TimezoneId = responseLocation.TimezoneId;
            location.UtcOffset = responseLocation.UtcOffset;

            var weather = new CurrentWeatherWKey();
            var responseWeather = response.Current;
            weather.Cloudcover = responseWeather.Cloudcover;
            weather.Feelslike = responseWeather.Feelslike;
            weather.Humidity = responseWeather.Humidity;
            weather.IsDay = responseWeather.IsDay;
            weather.ObservationTime = responseWeather.ObservationTime;
            weather.Precip = responseWeather.Precip;
            weather.Pressure = responseWeather.Pressure;
            weather.Temperature = responseWeather.Temperature;
            weather.UvIndex = responseWeather.UvIndex;
            weather.Visibility = responseWeather.Visibility;
            weather.WeatherCode = responseWeather.WeatherCode;
            weather.WindDegree = responseWeather.WindDegree;
            weather.WindDir = responseWeather.WindDir;
            weather.WindSpeed = responseWeather.WindSpeed;


            return new WeatherModel {Location = location, CurrentWeather = weather};
        }

        public List<WeatherModel> GetAllWeatherModels()
        {
            return _context.Weather.ToList();
        }
    }
}