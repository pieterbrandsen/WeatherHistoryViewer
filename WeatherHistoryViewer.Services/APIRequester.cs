using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading;
using WeatherHistoryViewer.Core.Models.Weather;

namespace WeatherHistoryViewer.Services
{
    public interface IApiRequester
    {
        HistoricalWeatherResponse GetHistoricalWeather(string apiKey, string cityName, string date,
            HourlyInterval hourlyInterval, int tryCount = 0);

        CurrentWeatherResponse GetCurrentWeather(string apiKey, string cityName, int tryCount = 0);
    }

    public class ApiRequester : IApiRequester
    {
        public HistoricalWeatherResponse GetHistoricalWeather(string apiKey, string cityName, string date,
            HourlyInterval hourlyInterval, int tryCount = 0)
        {
            Thread.Sleep(2 * 1000);
            var uri =
                $"https://api.weatherstack.com/historical?access_key={apiKey}& query={cityName}& historical_date={date}& hourly=1&interval={(int) hourlyInterval}& units=m";
            try
            {
                var jsonResponse = HTTPGet(uri).Replace(date, "Day");
                var objectResponse = JsonSerializer.Deserialize<HistoricalWeatherResponse>(jsonResponse);
                if (objectResponse?.Current != null)
                    return objectResponse;

                if (tryCount < 3) return GetHistoricalWeather(apiKey, cityName, date, hourlyInterval, tryCount++);
                throw new Exception();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public CurrentWeatherResponse GetCurrentWeather(string apiKey, string cityName, int tryCount = 0)
        {
            Thread.Sleep(5 * 1000);
            var uri = $"http://api.weatherstack.com/current?access_key={apiKey}& query={cityName}& units=m";
            try
            {
                var jsonResponse = HTTPGet(uri);
                var objectResponse = JsonSerializer.Deserialize<CurrentWeatherResponse>(jsonResponse);

                if (objectResponse?.Current != null)
                    return objectResponse;

                if (tryCount < 3) return GetCurrentWeather(apiKey, cityName, tryCount++);
                throw new Exception();
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