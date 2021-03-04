using System;
using System.IO;
using System.Net;
using System.Text.Json;
using WeatherHistoryViewer.Core.Models.Weather;

namespace WeatherHistoryViewer.Services
{
    public interface IApiRequester
    {
        HistoricalWeatherResponse GetHistoricalWeather(string apiKey, string cityName, string date,
            HourlyInterval hourlyInterval);

        CurrentWeatherResponse GetCurrentWeather(string apiKey, string cityName, string units);
    }

    public class ApiRequester : IApiRequester
    {
        public HistoricalWeatherResponse GetHistoricalWeather(string apiKey, string cityName, string date,
            HourlyInterval hourlyInterval)
        {
            var uri =
                $"https://api.weatherstack.com/historical?access_key={apiKey}& query={cityName}& historical_date={date}& hourly=1&interval={(int) hourlyInterval}& units=m";
            try
            {
                var jsonResponse = HTTPGet(uri).Replace(date, "Day");
                return JsonSerializer.Deserialize<HistoricalWeatherResponse>(jsonResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public CurrentWeatherResponse GetCurrentWeather(string apiKey, string cityName, string units)
        {
            var uri = $"https://api.weatherstack.com/current?access_key={apiKey}& query={cityName}& units={units}";
            try
            {
                var jsonResponse = HTTPGet(uri);
                return JsonSerializer.Deserialize<CurrentWeatherResponse>(jsonResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static string HTTPGet(string uri)
        {
            var request = (HttpWebRequest) WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using var response = (HttpWebResponse) request.GetResponse();
            using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }
    }
}