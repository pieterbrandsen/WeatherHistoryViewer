using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WeatherHistoryViewer.Services;

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
            IWeatherData weatherData = _serviceProvider.GetService<IWeatherData>();
            weatherData.AddCurrentWeatherToDB();
        }
    }
}
