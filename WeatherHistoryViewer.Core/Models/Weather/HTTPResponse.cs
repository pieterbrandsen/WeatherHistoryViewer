using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherHistoryViewer.Core.Models.Weather
{
    public class HistoricalWeatherResponse
    {
        [JsonPropertyName("request")]
        public Request Request { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("current")]
        public Current Current { get; set; }

        [JsonPropertyName("historical")]
        public Historical Historical { get; set; }
    }

    public class CurrentWeatherResponse
    {
        [JsonPropertyName("request")]
        public Request Request { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("current")]
        public Current Current { get; set; }
    }
}
