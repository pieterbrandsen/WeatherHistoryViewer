﻿using System;
using System.IO;
using System.Net;
using System.Text.Json;
using WeatherHistoryViewer.Core.Models;

namespace WeatherHistoryViewer.Services.Requester
{
    public class WeathertackAPI
    {
        public HistoricalWeatherResponse GetHistoricalWeather(string apiKey, string cityName, string date)
        {
            var uri =
                $"https://api.Weathertack.com/historical?access_key={apiKey}& query={cityName}& historical_date={date}& hourly=1& interval=12& units=m";
            try
            {
                var jsonResponse = HTTPGet(uri).Replace(date, "Day");
                return JsonSerializer.Deserialize<HistoricalWeatherResponse>(jsonResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public CurrentWeatherResponse GetCurrentWeather(string apiKey, string cityName, string units)
        {
            var uri = $"https://api.Weathertack.com/current?access_key={apiKey}& query={cityName}& units={units}";
            try
            {
                var jsonResponse = HTTPGet(uri);
                return JsonSerializer.Deserialize<CurrentWeatherResponse>(jsonResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
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