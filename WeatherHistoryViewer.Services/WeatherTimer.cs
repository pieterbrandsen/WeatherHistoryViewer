using System.Threading.Tasks;
using System.Timers;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Services.Handlers;

namespace WeatherHistoryViewer.Services
{
    public interface IWeatherTimer
    {
        public void StartTimer();
    }

    public class WeatherTimer : IWeatherTimer
    {
        private static Timer timer;
        private static IWeatherData _weatherData;
        private static IDateData _dateData;
        private static ILocationData _locationData;

        public WeatherTimer(IWeatherData weatherData, IDateData dateData, ILocationData locationData)
        {
            _weatherData = weatherData;
            _dateData = dateData;
            _locationData = locationData;
        }

        public void StartTimer()
        {
            timer = new Timer
            {
                Enabled = true,
                AutoReset = true,
                Interval = 60 * 60 * 1000
            };
            timer.Elapsed += OnTimedEvent;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            var oldestDate = _dateData.GetDateStringOfDaysAgo();
            var yesterdayDate = _dateData.GetDateStringOfDaysAgo(1);
            var locations = _locationData.GetAllLocationNames();
            Task.Run(() =>
            {
                foreach (var locationName in locations)
                {
                    _weatherData.UpdateHistoricalWeatherRangeToDb(locationName, HourlyInterval.Hours1, oldestDate,
                        yesterdayDate);
                }
            });
        }
    }
}