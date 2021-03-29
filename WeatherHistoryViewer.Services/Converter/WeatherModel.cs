using WeatherHistoryViewer.Core.Models;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Services.Handlers;
using WeatherHistoryViewer.Services.Helpers;

namespace WeatherHistoryViewer.Services.Converter
{
    public class WeatherModelConverter
    {
        private readonly DateHelper _dateHelper;
        private readonly LocationHandler _locationData;

        public WeatherModelConverter()
        {
            _locationData = new LocationHandler();
            _dateHelper = new DateHelper();
        }

        public HistoricalWeather ToHistoricalWeatherModelConverter(HistoricalWeatherResponse historicalWeatherResponse,
            string date)
        {
            var day = historicalWeatherResponse.Historical.Day;
            var weather = new HistoricalWeather
            {
                Location = historicalWeatherResponse.Location,
                AvgTemp = day.Avgtemp,
                Date = date,
                DateEpoch = day.DateEpoch,
                MaxTemp = day.Maxtemp,
                MinTemp = day.Mintemp,
                SunHour = day.Sunhour,
                TotalSnow = day.Totalsnow,
                UvIndex = day.UvIndex,
            };

            return weather;
        }
    }
}