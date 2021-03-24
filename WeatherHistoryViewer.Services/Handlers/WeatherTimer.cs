using System.Threading.Tasks;
using System.Timers;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Services.Handlers;
using WeatherHistoryViewer.Services.Helpers;

namespace WeatherHistoryViewer.Services
{
    public class WeatherTimer
    {
        private static Timer timer;
        private static WeatherHandler _weatherHandler;
        private static DateHelper _dateHelper;
        private static LocationHandler _locationHandler;

        public WeatherTimer()
        {
            _weatherHandler = new WeatherHandler();
            _dateHelper = new DateHelper();
            _locationHandler = new LocationHandler();
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
            var locations = _locationHandler.GetLocationNames();
            Task.Run(() =>
            {
                foreach (var locationName in locations)
                    _weatherHandler.UpdateHistoricalWeatherRangeToDb(locationName, oldestDate,
                        yesterdayDate);
            });
        }
    }
}