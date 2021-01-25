using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherHistoryViewer.Db;

namespace WeatherHistoryViewer.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddDbContext<ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection RegisterInterfaceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddScoped<ISecretRevealer, SecretRevealer>()
                .AddScoped<IRequester, APIRequester>()
                .AddScoped<IWeatherDataHandler, WeatherDataHandlerHandler>()
                .AddScoped<ICreateTimer, CreateTimer>();

            return services;
        }

        public static IServiceCollection RegisterSecrets(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}