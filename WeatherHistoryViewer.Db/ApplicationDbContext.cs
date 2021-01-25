using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WeatherHistoryViewer.Core.Models;

namespace WeatherHistoryViewer.Db
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WeatherModel> Weather { get; set; }
        public DbSet<CurrentWeatherWKey> CurrentWeather { get; set; }
        public DbSet<LocationWKey> Location { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<WeatherModel>()
                .HasOne(s => s.Location)
                .WithOne(ad => ad.WeatherModel)
                .HasForeignKey<LocationWKey>(ad => ad.WeatherModelId);
            builder.Entity<WeatherModel>()
                .HasOne(s => s.CurrentWeather)
                .WithOne(ad => ad.WeatherModel)
                .HasForeignKey<CurrentWeatherWKey>(ad => ad.WeatherModelId);
        }
    }
}