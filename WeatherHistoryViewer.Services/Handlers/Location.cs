using System.Collections.Generic;
using System.Linq;
using WeatherHistoryViewer.Core.Models.DataWarehouse;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;

namespace WeatherHistoryViewer.Services.Handlers
{
    public class LocationHandler
    {
        public Location GetLocation(string cityName, Location knownLocation = null)
        {
            using var context = new ApplicationDbContext();
            var foundLocation = context.Locations.FirstOrDefault(l => l.Name.ToLower() == cityName.ToLower());
            return foundLocation ?? knownLocation;
        }

        public LocationWarehouse GetLocation(string cityName, LocationWarehouse knownLocation)
        {
            using var context = new ApplicationDbContext();
            var foundLocation = context.LocationsWarehouse.FirstOrDefault(l => l.LocationName == cityName);
            return foundLocation ?? knownLocation;
        }

        public List<string> GetLocationNames()
        {
            using var context = new ApplicationDbContext();
            //var locations = context.Locations.Select(s => s.Name).ToList();
            var locations = context.LocationsWarehouse.Select(s => s.LocationName).ToList();
            return locations;
        }

        public bool DoesLocationExistInDb(string query)
        {
            return GetLocationNames().Contains(query);
        }
    }
}