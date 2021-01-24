using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherHistoryViewer.Core.Models;
using WeatherHistoryViewer.Services;

namespace TestConsole
{
    internal class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static IServiceProvider ServiceProvider { get; set; }


        private static void Main()
        {
            Startup();

            var createTimer = new CreateTimer(ServiceProvider);
            createTimer.InitTimer();
            Console.ReadKey();
        }

        private static void Startup()
        {
            var devEnvironmentVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

            var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) ||
                                devEnvironmentVariable.ToLower() == "development";
            //Determines the working environment as IHostingEnvironment is unavailable in a console app

            var builder = new ConfigurationBuilder();
            // tell the builder to look for the appsettings.json file
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();

            //only add secrets in development
            if (isDevelopment) builder.AddUserSecrets<Program>();

            Configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();

            //Map the implementations of your classes here ready for DI
            services.Configure<SecretKeys>(Configuration.GetSection(nameof(SecretKeys)))
                .RegisterDataServices(Configuration)
                .RegisterInterfaceServices(Configuration);

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}