using Microsoft.EntityFrameworkCore;
using WeatherHistoryViewer.Core.Models.DataWarehouse;
using WeatherHistoryViewer.Core.Models.Weather;

namespace WeatherHistoryViewer.Db
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UpdateTime> LastUpdateTimes { get; set; }
        public DbSet<WeatherWarehouse> WeatherWarehouse { get; set; }
        public DbSet<LocationWarehouse> LocationsWarehouse { get; set; }

        public DbSet<Location> Locations { get; set; }
        public DbSet<HistoricalWeather> Weather { get; set; }

        public DbSet<Time> Times { get; set; }
        public DbSet<WeatherMeasurment> WeatherMeasurments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(UserSecrets.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UpdateTime>().HasData(new UpdateTime {Name = "Weather", Date = "0001/01/01"});

            builder.Entity<HistoricalWeather>()
                .HasOne(i => i.Location)
                .WithMany();

            builder.Entity<WeatherWarehouse>()
                .HasOne(i => i.Location)
                .WithMany();
            builder.Entity<WeatherWarehouse>()
                .HasOne(i => i.Time)
                .WithMany();
            builder.Entity<WeatherWarehouse>().Navigation(i => i.WeatherMeasurment);
//.HasOne(i => i.WeatherMeasurment)
//.WithOne();

            builder.Entity<HistoricalWeather>().HasKey(o => o.Id);
            builder.Entity<Location>().HasKey(o => o.Name);

            builder.Entity<LocationWarehouse>().HasKey(o => o.LocationName);
            builder.Entity<WeatherWarehouse>().HasKey(o => o.Id);
            builder.Entity<Time>().HasKey(o => o.Id);
            builder.Entity<WeatherMeasurment>().HasKey(o => o.Id);
            builder.Entity<UpdateTime>().HasKey(o => o.Name);
        }
    }
}