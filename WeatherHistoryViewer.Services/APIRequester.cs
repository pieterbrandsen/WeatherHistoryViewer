using System.IO;
using System.Net;
using System.Text.Json;
using WeatherHistoryViewer.Core.Models;

namespace WeatherHistoryViewer.Services
{
    public interface IRequester
    {
        CurrentWeatherHTTPResponse GetCurrentWeather(string APIKey);
    }

    public class APIRequester : IRequester
    {
        public CurrentWeatherHTTPResponse GetCurrentWeather(string APIKey)
        {
            var uri = $"http://api.weatherstack.com/current?access_key={APIKey}& query=Baarn& units = m& language = en";
            var jsonResponse = HTTPGet(uri);
            var currentWeatherHTTPResponse = JsonSerializer.Deserialize<CurrentWeatherHTTPResponse>(jsonResponse);
            return currentWeatherHTTPResponse;
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