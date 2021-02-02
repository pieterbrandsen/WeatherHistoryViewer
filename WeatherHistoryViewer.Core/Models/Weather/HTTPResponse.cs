using System.Text.Json.Serialization;

namespace WeatherHistoryViewer.Core.Models.Weather
{
    public class HistoricalWeatherResponse
    {
        [JsonPropertyName("request")] public Request Request { get; set; }

        [JsonPropertyName("location")] public Location Location { get; set; }

        [JsonPropertyName("current")] public Current Current { get; set; }

        [JsonPropertyName("historical")] public Historical Historical { get; set; }
    }

    public class CurrentWeatherResponse
    {
        [JsonPropertyName("request")] public Request Request { get; set; }

        [JsonPropertyName("location")] public Location Location { get; set; }

        [JsonPropertyName("current")] public Current Current { get; set; }
    }

    public class HttpStatusResponse
    {
        public bool Result { get; set; } = false;
        public HttpStatusModel StatusModel { get; set; }
    }
}