namespace WeatherHistoryViewer.Core.Models
{
    public class WeatherLocationWKey : Location
    {
        public int Id { get; set; }
        public int WeatherModelId { get; set; }
        public WeatherModel WeatherModel { get; set; }
    }

    public class WeatherHistoryWeatherWKey : CurrentWeather
    {
        public int Id { get; set; }
        public int WeatherModelId { get; set; }
        public WeatherModel WeatherModel { get; set; }
    }

    public class WeatherModel
    {
        public int Id { get; set; }
        public WeatherLocationWKey Location { get; set; }
        public WeatherHistoryWeatherWKey CurrentWeather { get; set; }
    }
}