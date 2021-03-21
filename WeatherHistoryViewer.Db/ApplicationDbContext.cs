using Microsoft.EntityFrameworkCore;
using WeatherHistoryViewer.Core.Models.Weather;

namespace WeatherHistoryViewer.Db
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<HistoricalWeather> Weather { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(UserSecrets.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<HistoricalWeather>()
                .HasOne(i => i.Location)
                .WithMany();

            builder.Entity<HistoricalWeather>().HasKey(o => o.Id);
            builder.Entity<Location>().HasKey(o => o.Name);
        }
    }
}