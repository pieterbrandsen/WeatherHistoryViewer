using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherHistoryViewer.Core;

namespace WeatherHistoryViewer.Services.API
{
    public interface IRequester
    {
        CurrentWeatherHTTPResponse GetCurrentWeather(string APIKey);
    }
    public class Requester : IRequester
    {
        public Requester()
        {

        }
        private static string HTTPGet(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using Stream stream = response.GetResponseStream();
            using StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public CurrentWeatherHTTPResponse GetCurrentWeather(string APIKey)
        {
            string uri = $"http://api.weatherstack.com/current?access_key={APIKey}& query=Baarn& units = m& language = en";
            string jsonResponse = HTTPGet(uri);
            return JsonSerializer.Deserialize<CurrentWeatherHTTPResponse>(jsonResponse);
        }
    }
}
