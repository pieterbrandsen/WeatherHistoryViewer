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
        private static IWeatherHandler _weatherData;
        private static DateHelper _dateHelper;
        private static ILocationHandler _locationData;

        public WeatherTimer(IWeatherHandler weatherData, ILocationHandler locationData)
        {
            _weatherData = weatherData;
            _dateHelper = new DateHelper();
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
            var oldestDate = _dateHelper.GetDateStringOfDaysAgo();
            var yesterdayDate = _dateHelper.GetDateStringOfDaysAgo(1);
            var locations = _locationData.GetAllLocationNames();
            Task.Run(() =>
            {
                foreach (var locationName in locations)
                    _weatherData.UpdateHistoricalWeatherRangeToDb(locationName, HourlyInterval.Hours1, oldestDate,
                        yesterdayDate);
            });
        }
    }
}