using System;
using Microsoft.EntityFrameworkCore;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;

namespace WeatherHistoryViewer.Services.Handlers
{
    public class Database 
    {
        private readonly LocationHandler _locationHandler;
        public Database()
        {
            _locationHandler = new LocationHandler();
        }
        public void AddHistoricalWeather(HistoricalWeather weather)
        {
            using var context = new ApplicationDbContext();
            try
            {
                context.Database.BeginTransaction();
                if (_locationHandler.DoesLocationExistInDb(weather.Location.Name))
                    context.Locations.Attach(weather.Location);
                context.Weather.Add(weather);
                SaveChanges(context);
                context.Database.CommitTransaction();
            }
            catch (Exception e)
            {
                context.Database.RollbackTransaction();
                context.Database.CloseConnection();
                Console.WriteLine(e);
                throw;
            }
        }

        private void SaveChanges(ApplicationDbContext context)
        {
            context.Database.OpenConnection();
            context.SaveChanges();
            context.Database.CloseConnection();
        }
    }
}