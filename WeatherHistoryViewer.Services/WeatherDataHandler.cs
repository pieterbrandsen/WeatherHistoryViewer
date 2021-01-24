using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHistoryViewer.Core.Models;
using WeatherHistoryViewer.Db;

namespace WeatherHistoryViewer.Services
{
    public interface IWeatherData
    {
        public void AddCurrentWeatherToDB();
    }
    public class WeatherDataHandler : IWeatherData
    {
        private readonly ApplicationDbContext _context;
        private readonly ISecretRevealer _secretRevealer;
        private readonly IRequester _requester;

        public WeatherDataHandler(ApplicationDbContext context, ISecretRevealer secretRevealer, IRequester requester)
        {
            _context = context;
            _secretRevealer = secretRevealer;
            _requester = requester;
        }

        private WeatherModel CreateNewWeatherModel(CurrentWeatherHTTPResponse response)
        {
            LocationWKey location = new LocationWKey();
            Location responseLocation = response.Location;
            location.Country = responseLocation.Country;
            location.Lat = responseLocation.Lat;
            location.Localtime = responseLocation.Localtime;
            location.LocaltimeEpoch = responseLocation.LocaltimeEpoch;
            location.Lon = responseLocation.Lon;
            location.Name = responseLocation.Name;
            location.Region = responseLocation.Region;
            location.TimezoneId = responseLocation.TimezoneId;
            location.UtcOffset = responseLocation.UtcOffset;

            CurrentWeatherWKey weather = new CurrentWeatherWKey();
            CurrentWeather responseWeather = response.Current;
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


            return new WeatherModel { Location = location, CurrentWeather = weather};
        }

        public void AddCurrentWeatherToDB()
        {
            SecretKeys secrets = _secretRevealer.RevealSecretKeys();
            CurrentWeatherHTTPResponse response = _requester.GetCurrentWeather(secrets.WeatherStack);

            WeatherModel weather = CreateNewWeatherModel(response);
            _context.Weather.Add(weather);
            _context.SaveChanges();
        }
    }
}
