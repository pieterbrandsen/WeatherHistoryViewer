using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;

namespace WeatherHistoryViewer.Services.Handlers
{
    public interface IDatabase
    {
        public void AddHistoricalWeather(HistoricalWeather weather, bool saveData);
    }

    public class Database : IDatabase
    {
        private readonly ApplicationDbContext _context;

        public Database(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddHistoricalWeather(HistoricalWeather weather, bool saveData)
        {
            try
            {
                _context.Weather.Add(weather);
                if (!saveData) SaveChanges();
            }
            catch (Exception e)
            {
                CloseConnection();
                Console.WriteLine(e);
                throw;
            }
        }

        private void OpenConnection()
        {
            _context.Database.OpenConnection();
        }

        private void CloseConnection()
        {
            _context.Database.CloseConnection();
        }

        private void SaveChanges()
        {
            OpenConnection();
            _context.SaveChanges();
            CloseConnection();
        }

        public void AddHistoricalWeatherList(List<HistoricalWeather> weatherList)
        {
            try
            {
                _context.Weather.AddRange(weatherList);
                SaveChanges();
            }
            catch (Exception e)
            {
                CloseConnection();
                Console.WriteLine(e);
                throw;
            }
        }
    }
}