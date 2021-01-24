namespace WeatherHistoryViewer.Core.Models
{
    public class LocationWKey : Location
    {
        public int Id { get; set; }
        public int WeatherModelId { get; set; }
        public WeatherModel WeatherModel { get; set; }
    }

    public class CurrentWeatherWKey : CurrentWeather
    {
        public int Id { get; set; }
        public int WeatherModelId { get; set; }
        public WeatherModel WeatherModel { get; set; }
    }

    public class WeatherModel
    {
        public int Id { get; set; }
        public LocationWKey Location { get; set; }
        public CurrentWeatherWKey CurrentWeather { get; set; }
    }
}