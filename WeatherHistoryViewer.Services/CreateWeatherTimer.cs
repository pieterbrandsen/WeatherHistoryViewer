using System;
using System.Timers;
using WeatherHistoryViewer.Core.Models.Weather;

namespace WeatherHistoryViewer.Services
{
    public interface ICreateWeatherTimer
    {
        public void InitTimer();
    }

    public class CreateWeatherTimer : ICreateWeatherTimer
    {
        private static Timer timer;
        private static IWeatherDataHandler _weatherDataHandler;

        public CreateWeatherTimer(IWeatherDataHandler weatherDataHandler)
        {
            _weatherDataHandler = weatherDataHandler;
        }

        public void InitTimer()
        {
            // Create a timer with a 60 minute interval.
            timer = new Timer(60 * 60 * 1000);
            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("A new data entry was added at {0:HH:mm:ss.fff}",
                e.SignalTime);
            _weatherDataHandler.AddHistoricalWeatherToDb("Weather", "2015-01-21", HourlyInterval.Hours1);
        }
    }
}