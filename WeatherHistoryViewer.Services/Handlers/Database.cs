using Microsoft.EntityFrameworkCore;
using System;
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

        public Database(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddHistoricalWeather(HistoricalWeather weather)
        {
            using var context = _contextFactory.CreateDbContext();
            try
            {
                context.Database.BeginTransaction();
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