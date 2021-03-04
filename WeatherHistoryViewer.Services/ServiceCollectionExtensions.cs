using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherHistoryViewer.Db;
using WeatherHistoryViewer.Services.Handlers;

namespace WeatherHistoryViewer.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration["UserSecrets:DefaultConnectionString"]));

            return services;
        }

        public static IServiceCollection RegisterDataFactoryServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddDbContextFactory<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration["UserSecrets:DefaultConnectionString"]));

            return services;
        }

        public static IServiceCollection RegisterInterfaceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddScoped<ISecretRevealer, SecretRevealer>()
                .AddScoped<IApiRequester, ApiRequester>()
                .AddScoped<IWeatherHandler, WeatherHandler>()
                .AddScoped<IWeatherTimer, WeatherTimer>()
                .AddScoped<ICustomWeatherClassConverter, CustomWeatherClassConverter>()
                .AddScoped<ILocationHandler, LocationHandler>()
                .AddScoped<IDatabase, Database>();
            return services;
        }
    }
}