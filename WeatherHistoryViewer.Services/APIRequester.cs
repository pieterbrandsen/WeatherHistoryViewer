using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using WeatherHistoryViewer.Core.Models;
using WeatherHistoryViewer.Core.Models.Weather;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WeatherHistoryViewer.Services
{
    public interface IApiRequester
    {
        HistoricalWeatherResponse GetHistoricalWeather(string apiKey, string cityName, string date, HourlyInterval hourlyInterval);
        CurrentWeatherResponse GetCurrentWeather(string apiKey, string cityName);
    }

    public class ApiRequester : IApiRequester
    {
        private string HTTPGet(string uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using var response = (HttpWebResponse)request.GetResponse();
            using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public HistoricalWeatherResponse GetHistoricalWeather(string apiKey, string cityName, string date, HourlyInterval hourlyInterval)
        {
            var uri = $"http://api.weatherstack.com/historical?access_key={apiKey}& query={cityName}& historical_date={date}& hourly=1&interval={(int)hourlyInterval}& units=m";
            var jsonResponse = HTTPGet(uri).Replace(date, "Day");   
            var responseObject = JsonSerializer.Deserialize<HistoricalWeatherResponse>(jsonResponse);
            return responseObject;
        }
        public CurrentWeatherResponse GetCurrentWeather(string apiKey, string cityName)
        {
            var uri = $"http://api.weatherstack.com/current?access_key={apiKey}& query={cityName}& units=m";
            var jsonResponse = HTTPGet(uri);
            var responseObject = JsonSerializer.Deserialize<CurrentWeatherResponse>(jsonResponse);
            return responseObject;
        }
    }
}