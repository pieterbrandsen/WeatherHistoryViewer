using System;
using System.Timers;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherHistoryViewer.Services
{
    public interface ICreateTimer
    {
        public void InitTimer();
    }
    public class CreateTimer : ICreateTimer
    {
        private static Timer timer;
        private static IWeatherDataHandler _weatherDataHandler;

        public CreateTimer(IWeatherDataHandler weatherDataHandler)
        {
            _weatherDataHandler = weatherDataHandler;
        }

        public void InitTimer()
        {
            // Create a timer with a ... second interval.
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
            _weatherDataHandler.AddCurrentWeatherToDB();
        }
    }
}