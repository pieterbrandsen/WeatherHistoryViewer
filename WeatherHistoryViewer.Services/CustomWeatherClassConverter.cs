using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Services.Handlers;

namespace WeatherHistoryViewer.Services
{
    public interface ICustomWeatherClassConverter
    {
        public HistoricalWeather ToHistoricalWeatherModelConverter(HistoricalWeatherResponse historicalWeatherResponse,
            string date, HourlyInterval hourlyInterval);
    }

    public class CustomWeatherClassConverter : ICustomWeatherClassConverter
    {
        private readonly IDateData _dateData;
        private readonly ILocationData _locationData;

        public CustomWeatherClassConverter(ILocationData locationData, IDateData dateData)
        {
            _locationData = locationData;
            _dateData = dateData;
        }

        public HistoricalWeather ToHistoricalWeatherModelConverter(HistoricalWeatherResponse historicalWeatherResponse,
            string date, HourlyInterval hourlyInterval)
        {
            var day = historicalWeatherResponse.Historical.Day;
            var weather = new HistoricalWeather
            {
                Location = historicalWeatherResponse.Location,
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

            foreach (var weatherSnapshot in weather.SnapshotsOfDay)
            {
                var hour = weatherSnapshot.Time != "0" ? weatherSnapshot.Time.Split("0")[0] : "0";
                var formattedHour = hour.Length == 1 ? $"0{hour}:00" : $"{hour}:00";
                weatherSnapshot.FullDate = $"{date.Replace("-", "-")}T{formattedHour}:01";
            }

            return weather;
        }
    }
}