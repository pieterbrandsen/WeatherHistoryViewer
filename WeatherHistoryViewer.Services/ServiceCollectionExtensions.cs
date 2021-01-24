using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHistoryViewer.Core;
using WeatherHistoryViewer.Db;
using WeatherHistoryViewer.Services;

namespace WeatherHistoryViewer.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection RegisterInterfaceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<ISecretRevealer, SecretRevealer>()
                .AddScoped<IRequester, APIRequester>()
                .AddScoped<IWeatherData, WeatherDataHandler>();
            
            return services;
        }

        public static IServiceCollection RegisterSecrets(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
