using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHistoryViewer.Core.Models.DataWarehouse;
using WeatherHistoryViewer.Db;
using WeatherHistoryViewer.Services.Helpers;

namespace WeatherHistoryViewer.Services.Handlers
{
    public class DataWarehouseHandlers
    {
        private DatabaseHandler _databaseHandler = new();
        public void UpdateLocationWarehouse()
        {
            using var context = new ApplicationDbContext();
            var alreadyIncludedNames = new HashSet<string>(context.LocationsWarehouse.Select(w => w.LocationName));
            var missingNames = context.Locations.Select(l => l.Name).Where(p => !alreadyIncludedNames.Contains(p)).ToList();
            foreach (var name in missingNames)
            {
                _databaseHandler.AddLocationInWarehouse(new LocationWarehouse() { LocationName = name });
            }
        }

        private class NameAndLocation
        {
            public string Date { get; set; }
            public string City { get; set; }
            public string CombinedValue => Date+=City;
        }
        public void UpdateWeatherWarehouse()
        {
            Task.Run(() =>
            {
                UpdateLocationWarehouse();

                using var context = new ApplicationDbContext();
                var alreadyIncludedInWarehouse = new HashSet<string>(context.WeatherWarehouse.Include(w => w.Location).Include(w => w.Time).Select(w => w.Location.LocationName + w.Time.Date));
                var missingWeather = context.Weather.Include(w => w.Location).Where(w => !alreadyIncludedInWarehouse.Contains(w.Date + w.Location.Name)).ToList();
                foreach (var weather in missingWeather)
                {
            Task.Run(() =>
            {
                var time = new TimeHelper().GetTime(weather.Date, new() { Day = Convert.ToInt16(weather.Date.Split("/")[2]), Month = Convert.ToInt16(weather.Date.Split("/")[1]), Year = Convert.ToInt16(weather.Date.Split("/")[0]), Date = weather.Date });
                    var weatherWarehouse = new WeatherWarehouse()
                    {
                        Location = new() { LocationName = weather.Location.Name },
                        Time = time,
                        WeatherMeasurment = new() { AvgTemp = weather.AvgTemp, MaxTemp = weather.MaxTemp, MinTemp = weather.MinTemp, SunHour = weather.SunHour }
                    };
                    _databaseHandler.AddWeatherInWarehouse(weatherWarehouse);
            });
                }
            });
            }
    }
}
