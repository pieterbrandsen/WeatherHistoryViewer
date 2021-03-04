using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;

namespace WeatherHistoryViewer.Services.Handlers
{
    public interface ILocationHandler
    {
        public Location GetLocationBasedOnCityName(string cityName, Location knownLocation);
        public List<string> GetAllLocationNames();
        public bool DoesLocationExistInDb(string query);
    }

    public class LocationHandler : ILocationHandler
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public LocationHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public Location GetLocationBasedOnCityName(string cityName, Location knownLocation)
        {
            using var context = _contextFactory.CreateDbContext();
            var foundLocation = context.Locations.FirstOrDefault(l => l.Name.ToLower() == cityName.ToLower());
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

        public bool DoesLocationExistInDb(string query)
        {
            return GetAllLocationNames().Contains(query);
        }
    }
}