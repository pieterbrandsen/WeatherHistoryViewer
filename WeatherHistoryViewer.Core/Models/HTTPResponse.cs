using System.Text.Json.Serialization;
using WeatherHistoryViewer.Core.Models.Weather;

namespace WeatherHistoryViewer.Core.Models
{
    public class HistoricalWeatherResponse
    {
        [JsonPropertyName("request")] public RequestJSON Request { get; set; }

        [JsonPropertyName("location")] public Location Location { get; set; }

        [JsonPropertyName("current")] public CurrentJSON Current { get; set; }

        [JsonPropertyName("historical")] public HistoricalJSON Historical { get; set; }
    }

    public class CurrentWeatherResponse
    {
        [JsonPropertyName("request")] public RequestJSON Request { get; set; }

        [JsonPropertyName("location")] public Location Location { get; set; }

        [JsonPropertyName("current")] public CurrentJSON Current { get; set; }
    }

    public class HttpStatusResponse
    {
        public bool Result { get; set; } = false;
        public HttpStatusModel StatusModel { get; set; }
    }
}