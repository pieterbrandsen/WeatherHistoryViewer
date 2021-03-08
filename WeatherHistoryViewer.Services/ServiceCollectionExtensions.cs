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

        public static IServiceCollection RegisterDataFactoryServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddDbContextFactory<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration["UserSecrets:DefaultConnectionString"]));

            return services;
        }

        public static IServiceCollection RegisterUserSecrets(this IServiceCollection services,
            IConfiguration configuration)
        {
            var secretRevealer = new RevealUserSecrets(configuration);
            UserSecrets.ConnectionString = secretRevealer.ConnectionString();
            UserSecrets.WeatherHistoryApiKey = secretRevealer.WeatherHistoryApiKey();
            UserSecrets.WeatherStackApiKey = secretRevealer.WeatherStackApiKey();

            return services;
        }
    }
}