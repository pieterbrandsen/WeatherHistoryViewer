using System.Linq;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;

namespace WeatherHistoryViewer.Services.Handlers
{
    public interface ILocationData
    {
        public Location GetLocationBasedOnCity(string cityName, Location knownLocation);
    }

    public class LocationData : ILocationData
    {
        private readonly ApplicationDbContext _context;

        public LocationData(ApplicationDbContext context)
        {
            _context = context;
        }

        public Location GetLocationBasedOnCity(string cityName, Location knownLocation)
        {
            var foundLocation = _context.Locations.FirstOrDefault(l => l.Name == cityName);
            return foundLocation ?? knownLocation;
        }
    }
}