using System.Collections.Generic;
using System.Linq;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;

namespace WeatherHistoryViewer.Services.Handlers
{
    public class LocationHandler
    {
        public Location GetLocationBasedOnCityName(string cityName, Location knownLocation)
        {
            using var context = new ApplicationDbContext();
            var foundLocation = context.Locations.FirstOrDefault(l => l.Name.ToLower() == cityName.ToLower());
            return foundLocation ?? knownLocation;
        }

        public List<string> GetAllLocationNames()
        {
            using var context = new ApplicationDbContext();
            var locations = context.Locations.Select(l => l.Name).ToList();
            return locations;
        }

        public bool DoesLocationExistInDb(string query)
        {
            return GetAllLocationNames().Contains(query);
        }
    }
}