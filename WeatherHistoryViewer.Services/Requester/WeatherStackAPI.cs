using System;
using System.IO;
using System.Net;
using System.Text.Json;
using WeatherHistoryViewer.Core.Models;

namespace WeatherHistoryViewer.Services.Requester
{
    public class WeatherStackApi
    {
        public HistoricalWeatherResponse GetHistoricalWeather(string apiKey, string cityName, string date)
        {
            date = date.Replace("/", "-");
            var uri =
                $"https://api.Weatherstack.com/historical?access_key={apiKey}& query={cityName}& historical_date={date}& hourly=1& interval=12& units=m";
            try
            {
                var jsonResponse = HttpGet(uri).Replace(date, "Day");
                return JsonSerializer.Deserialize<HistoricalWeatherResponse>(jsonResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new HistoricalWeatherResponse();
            }
        }

        private static string HttpGet(string uri)
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