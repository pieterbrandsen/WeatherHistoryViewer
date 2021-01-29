using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHistoryViewer.Core.Models.Weather;

namespace WeatherHistoryViewer.Services
{
    public interface ICustomWeatherClassConverter
    {
        public HistoricalWeather ToHistoricalWeatherModelConverter(HistoricalWeatherResponse historicalWeatherResponse, string date, HourlyInterval hourlyInterval);
    }
    public class CustomWeatherClassConverter : ICustomWeatherClassConverter
    {
        private readonly ILocationDataHandler _locationDataHandler;
        public CustomWeatherClassConverter(ILocationDataHandler locationDataHandler)
        {
            _locationDataHandler = locationDataHandler;
        }
        public HistoricalWeather ToHistoricalWeatherModelConverter(HistoricalWeatherResponse historicalWeatherResponse, string date, HourlyInterval hourlyInterval)
        {
            var historicalWeather = new HistoricalWeather()
            {
                Location = _locationDataHandler.GetLocationBasedOnCity(historicalWeatherResponse.Location.Name, historicalWeatherResponse.Location),
                AvgTemp = historicalWeatherResponse.Historical.Day.Avgtemp,
                Date = date,
                DateEpoch = historicalWeatherResponse.Historical.Day.DateEpoch,
                SnapshotsOfDay = historicalWeatherResponse.Historical.Day.HourlyModels,
                MaxTemp = historicalWeatherResponse.Historical.Day.Maxtemp,
                MinTemp = historicalWeatherResponse.Historical.Day.Mintemp,
                SunHour = historicalWeatherResponse.Historical.Day.Sunhour,
                TotalSnow = historicalWeatherResponse.Historical.Day.Totalsnow,
                UvIndex = historicalWeatherResponse.Historical.Day.UvIndex,
                HourlyInterval = hourlyInterval
            };

            return historicalWeather;
        }
    }
}
