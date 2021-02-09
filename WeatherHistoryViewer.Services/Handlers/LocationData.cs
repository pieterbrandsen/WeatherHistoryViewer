﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;

namespace WeatherHistoryViewer.Services.Handlers
{
    public interface ILocationData
    {
        public Location GetLocationBasedOnCityName(string cityName, Location knownLocation);
        public List<string> GetAllLocationNames();
    }

    public class LocationData : ILocationData
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public LocationData(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public Location GetLocationBasedOnCityName(string cityName, Location knownLocation)
        {
            using var context = _contextFactory.CreateDbContext();
            var foundLocation = context.Locations.FirstOrDefault(l => l.Name == cityName);
            context.Dispose();
            return foundLocation ?? knownLocation;
        }
        public List<string> GetAllLocationNames()
        {
            using var context = _contextFactory.CreateDbContext();
            var locations = context.Locations.Select(l => l.Name).ToList();
            context.Dispose();
            return locations;
        }
    }
}