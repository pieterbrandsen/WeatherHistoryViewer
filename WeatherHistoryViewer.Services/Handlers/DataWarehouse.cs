using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherHistoryViewer.Core.Models.DataWarehouse;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;
using WeatherHistoryViewer.Services.Helpers;

namespace WeatherHistoryViewer.Services.Handlers
{
    public class DataWarehouseHandlers
    {
        private readonly DatabaseHandler _databaseHandler = new();

        public void UpdateLocationWarehouse()
        {
            using var context = new ApplicationDbContext();
            var alreadyIncludedNames = new HashSet<string>(context.LocationsWarehouse.Select(w => w.LocationName));
            var missingNames = context.Locations.Select(l => l.Name).Where(p => !alreadyIncludedNames.Contains(p))
                .ToList();
            foreach (var name in missingNames)
                _databaseHandler.AddLocationInWarehouse(new LocationWarehouse {LocationName = name});
        }

        public void UpdateWeatherWarehouse()
        {
            Task.Run(() =>
            {
                UpdateLocationWarehouse();

                using var context = new ApplicationDbContext();
                var alreadyIncludedInWarehouse = new HashSet<string>(context.WeatherWarehouse.Include(w => w.Location)
                    .Include(w => w.Time).Select(w => w.Location.LocationName + w.Time.Date));
                var missingWeather = context.Weather.Include(w => w.Location)
                    .Where(w => !alreadyIncludedInWarehouse.Contains(w.Date + w.Location.Name)).ToList();
                foreach (var weather in missingWeather)
                    Task.Run(() =>
                    {
                        var time = new TimeHelper().GetTime(weather.Date,
                            new Time
                            {
                                Day = Convert.ToInt16(weather.Date.Split("/")[2]),
                                Month = Convert.ToInt16(weather.Date.Split("/")[1]),
                                Year = Convert.ToInt16(weather.Date.Split("/")[0]), Date = weather.Date
                            });
                        var weatherWarehouse = new WeatherWarehouse
                        {
                            Location = new LocationWarehouse {LocationName = weather.Location.Name},
                            Time = time,
                            WeatherMeasurement = new WeatherMeasurement
                            {
                                AvgTemp = weather.AvgTemp, MaxTemp = weather.MaxTemp, MinTemp = weather.MinTemp,
                                SunHour = weather.SunHour
                            }
                        };
                        _databaseHandler.AddWeatherInWarehouse(weatherWarehouse);
                    });
            });
        }

        private class NameAndLocation
        {
            private string Date { get; set; }
            private string City { get; set; }
            public string CombinedValue => Date += City;
        }
    }
}