﻿using System;
using Microsoft.EntityFrameworkCore;
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
        private readonly ApplicationDbContext _context;

        public Database(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            using var context = contextFactory.CreateDbContext();
            _context = context;
        }

        public void AddHistoricalWeather(HistoricalWeather weather)
        {
            try
            {
                BeginTransaction();
                _context.Weather.Add(weather);
                SaveChanges();
                CommitTransaction();
            }
            catch (Exception e)
            {
                RollbackTransaction();
                CloseConnection();
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                DisposeConnection();
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

        private void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        private void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        private void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        private void DisposeConnection()
        {
            _context.Dispose();
        }
    }
}