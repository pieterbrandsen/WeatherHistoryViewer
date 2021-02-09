using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Update;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;

namespace WeatherHistoryViewer.Services.Handlers
{
    public interface IDatabase
    {
        public void AddHistoricalWeather(HistoricalWeather weather);
    }

    public class Database : IDatabase
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly ILocationData _locationData;

        public Database(IDbContextFactory<ApplicationDbContext> contextFactory, ILocationData locationData)
        {
            _contextFactory = contextFactory;
            _locationData = locationData;
        }

        public void AddHistoricalWeather(HistoricalWeather weather)
        {
            using var context = _contextFactory.CreateDbContext();
            try
            {
                context.Database.BeginTransaction();
                if (_locationData.DoesLocationExistInDb(weather.Location.Name))
                {
                    context.Locations.Attach(weather.Location);
                }
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
            finally
            {
                context.Dispose();
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