using System.Timers;
using WeatherHistoryViewer.Services.Handlers;
using WeatherHistoryViewer.Services.Helpers;

namespace WeatherHistoryViewer.Services
{
    public class WeatherTimer
    {
        private static Timer _timer;
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
            _timer = new Timer
            {
                Enabled = true,
                AutoReset = true,
                Interval = 12 * 60 * 60 * 1000
            };
            _timer.Elapsed += OnTimedEvent;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            new WeatherHandler().UpdateAllSavedHistoricalWeather();
        }
    }
}