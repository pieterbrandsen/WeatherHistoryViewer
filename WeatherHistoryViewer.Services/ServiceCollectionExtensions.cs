using Microsoft.EntityFrameworkCore;
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
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration["UserSecrets:DefaultConnectionString"]));


            return services;
        }

        public static IServiceCollection RegisterInterfaceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddScoped<ISecretRevealer, SecretRevealer>()
                .AddScoped<IApiRequester, ApiRequester>()
                .AddScoped<IWeatherDataHandler, WeatherDataHandlerHandler>()
                .AddScoped<ICreateWeatherTimer, CreateWeatherTimer>()
                .AddScoped<ICustomWeatherClassConverter, CustomWeatherClassConverter>()
                .AddScoped<ILocationDataHandler, LocationDataHandler>();
            return services;
        }
    }
}