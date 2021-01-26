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
        public DbSet<WeatherHistoryWeatherWKey> WeatherHistory { get; set; }
        public DbSet<WeatherLocationWKey> WeatherLocation { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.EnableSensitiveDataLogging();
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("");
        //    }
        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<WeatherModel>()
                .HasOne(s => s.Location)
                .WithOne(ad => ad.WeatherModel)
                .HasForeignKey<WeatherLocationWKey>(ad => ad.WeatherModelId);
            builder.Entity<WeatherModel>()
                .HasOne(s => s.CurrentWeather)
                .WithOne(ad => ad.WeatherModel)
                .HasForeignKey<WeatherHistoryWeatherWKey>(ad => ad.WeatherModelId);
        }
    }
}