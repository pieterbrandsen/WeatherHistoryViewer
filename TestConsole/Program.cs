using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherHistoryViewer.Core;
using WeatherHistoryViewer.Db;
using WeatherHistoryViewer.Services;
using WeatherHistoryViewer.Services.API;
using WeatherHistoryViewer.Services.Db;
using System.Timers;

namespace TestConsole
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static IServiceProvider ServiceProvider { get; set; }
        private static System.Timers.Timer aTimer;

        private static void Main()
        {
            var devEnvironmentVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

            var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) ||
                                devEnvironmentVariable.ToLower() == "development";
            //Determines the working environment as IHostingEnvironment is unavailable in a console app

            var builder = new ConfigurationBuilder();
            // tell the builder to look for the appsettings.json file
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            //only add secrets in development
            if (isDevelopment)
            {
                builder.AddUserSecrets<Program>();
            }

            Configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();

            //Map the implementations of your classes here ready for DI
            services.Configure<SecretKeys>(Configuration.GetSection(nameof(SecretKeys)))
                .RegisterDataServices(Configuration)
                    .AddScoped<ISecretRevealer, SecretRevealer>()
    .AddScoped<IRequester, Requester>()
    .AddScoped<IWeatherData, WeatherData>();

            ServiceProvider = services.BuildServiceProvider();

            SetTimer();
            Console.ReadKey();
        }

        private static void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
            IWeatherData weatherData = ServiceProvider.GetService<IWeatherData>();
            //weatherData.AddCurrentWeatherToDB();
        }
    }
}
