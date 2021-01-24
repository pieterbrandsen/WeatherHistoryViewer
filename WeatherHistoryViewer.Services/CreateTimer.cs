using System;
using System.Timers;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherHistoryViewer.Services
{
    public class CreateTimer
    {
        private static Timer aTimer;
        private static IServiceProvider _serviceProvider;

        public CreateTimer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void InitTimer()
        {
            // Create a timer with a ... second interval.
            aTimer = new Timer(30 * 60 * 1000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("A new data entry was added at {0:HH:mm:ss.fff}",
                e.SignalTime);
            var weatherData = _serviceProvider.GetService<IWeatherData>();
            weatherData.AddCurrentWeatherToDB();
        }
    }
}