using System;
using Microsoft.EntityFrameworkCore;
using WeatherHistoryViewer.Core.Models.DataWarehouse;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;
using WeatherHistoryViewer.Services.Helpers;

namespace WeatherHistoryViewer.Services.Handlers
{
    public class DatabaseHandler
    {
        private readonly LocationHandler _locationHandler;

        public DatabaseHandler()
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
            context.SaveChanges();
                context.Database.CommitTransaction();
            }
            catch (Exception e)
            {
                context.Database.RollbackTransaction();
                Console.WriteLine(e);
            }
            finally
            {
                context.Database.CloseConnection();
            }
        }

        public void AddLocationInWarehouse(LocationWarehouse locationWarehouse)
        {
            using var context = new ApplicationDbContext();
            try
            {
                context.LocationsWarehouse.Add(locationWarehouse);
            context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void AddWeatherInWarehouse(WeatherWarehouse weatherWarehouse)
        {
            using var context = new ApplicationDbContext();
            try
            {
                context.Database.BeginTransaction();
                if (_locationHandler.DoesLocationExistInDb(weatherWarehouse.Location.LocationName))
                    context.LocationsWarehouse.Attach(weatherWarehouse.Location);
                if (new DateHelper().DoesDateExistInDb(weatherWarehouse.Time.Date))
                    context.Times.Attach(weatherWarehouse.Time);
                context.WeatherWarehouse.Add(weatherWarehouse);
            context.SaveChanges();
                context.Database.CommitTransaction();
            }
            catch (Exception e)
            {
                context.Database.RollbackTransaction();
                Console.WriteLine(e);
            }
            finally
            {
                context.Database.CloseConnection();
            }
        }
    }
}