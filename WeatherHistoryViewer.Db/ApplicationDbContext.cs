using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WeatherHistoryViewer.Core;
using WeatherHistoryViewer.Core.Models;

namespace WeatherHistoryViewer.Db
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=weatherDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
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
