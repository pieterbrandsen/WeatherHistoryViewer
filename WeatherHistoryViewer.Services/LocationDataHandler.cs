using System.Linq;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;

namespace WeatherHistoryViewer.Services
{
    public interface ILocationDataHandler
    {
        public Location GetLocationBasedOnCity(string cityName, Location knownLocation);
    }

    public class LocationDataHandler : ILocationDataHandler
    {
        private readonly ApplicationDbContext _context;

        public LocationDataHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public Location GetLocationBasedOnCity(string cityName, Location knownLocation)
        {
            var foundLocation = _context.Locations.FirstOrDefault(l => l.Name == cityName);
            if (foundLocation != null) return foundLocation;

            return knownLocation;
        }
    }
}