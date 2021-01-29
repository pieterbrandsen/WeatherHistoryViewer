using WeatherHistoryViewer.Core.Models.Weather;

namespace WeatherHistoryViewer.Services
{
    public interface ICustomWeatherClassConverter
    {
        public HistoricalWeather ToHistoricalWeatherModelConverter(HistoricalWeatherResponse historicalWeatherResponse,
            string date, HourlyInterval hourlyInterval);
    }

    public class CustomWeatherClassConverter : ICustomWeatherClassConverter
    {
        private readonly ILocationDataHandler _locationDataHandler;

        public CustomWeatherClassConverter(ILocationDataHandler locationDataHandler)
        {
            _locationDataHandler = locationDataHandler;
        }

        public HistoricalWeather ToHistoricalWeatherModelConverter(HistoricalWeatherResponse historicalWeatherResponse,
            string date, HourlyInterval hourlyInterval)
        {
            var day = historicalWeatherResponse.Historical.Day;
            return new HistoricalWeather
            {
                Location = _locationDataHandler.GetLocationBasedOnCity(historicalWeatherResponse.Location.Name,
                    historicalWeatherResponse.Location),
                AvgTemp = day.Avgtemp,
                Date = date,
                DateEpoch = day.DateEpoch,
                SnapshotsOfDay = day.HourlyModels,
                MaxTemp = day.Maxtemp,
                MinTemp = day.Mintemp,
                SunHour = day.Sunhour,
                TotalSnow = day.Totalsnow,
                UvIndex = day.UvIndex,
                HourlyInterval = hourlyInterval
            };
        }
    }
}