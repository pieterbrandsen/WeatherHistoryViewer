using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;
using WeatherHistoryViewer.Services.Handlers;

namespace WeatherHistoryViewer.Services.Helper
{
    public class WeatherHelper
    {
        private readonly DateHelper dateHelper = new DateHelper();
        private readonly LocationHandler locationHandler = new LocationHandler();
        public List<HistoricalWeather> GetWeatherOfDateInThePastYears(string cityName, string date)
        {
            using var context = new ApplicationDbContext();
            try
            {
                var dates = dateHelper.GetDateInLast15Y(date);
                var weather = context.Weather.Include(w => w.Location).Include(w => w.SnapshotsOfDay)
                    .Where(w => w.Location.Name == cityName && dates.Contains(w.Date))
                    .OrderByDescending(o => o.DateEpoch).ToList();
                return weather;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public (string date, double temp) GetMaxOrMinTempOfLocation(bool returnMax, string locationName)
    {
            using var context = new ApplicationDbContext();
            double temp = double.NaN;
            string date = null;

            if (returnMax) {
                temp = context.Weather.Include(w=>w.Location).Where(w=>w.Location.Name == locationName).Max(w => w.MaxTemp);
            }
            else {
                temp = context.Weather.Include(w => w.Location).Where(w => w.Location.Name == locationName).Min(w => w.MinTemp);
            };

            date = context.Weather.FirstOrDefault(w => (returnMax == true ? w.MaxTemp : w.MinTemp) == temp).Date;
            return (date, temp);
        }

        public List<WeatherOverview> GetWeatherOverview()
        {
            using var context = new ApplicationDbContext();

            var overviewList = new List<WeatherOverview>();
            var locations = locationHandler.GetAllLocationNames();
            foreach (var location in locations)
            {
                (string dateOfMaxTemp, double maxTemp) = GetMaxOrMinTempOfLocation(true,location);
                (string dateOfMinTemp, double minTemp) = GetMaxOrMinTempOfLocation(false,location);
                var overviewObj = new WeatherOverview()
                {
                    LocationName = location,
                    MaxTemp = Math.Round(maxTemp,2),
                    DateOfMaxTemp = dateOfMaxTemp,
                    MinTemp = Math.Round(minTemp, 2),
                    DateOfMinTemp = dateOfMinTemp,
                    AverageSunHours = Math.Round(context.Weather.Include(w => w.Location).Where(w => w.Location.Name == location).Select(w => w.SunHour).Average(),2),
                    AverageTemp = Math.Round(context.Weather.Include(w=>w.Location).Where(w=>w.Location.Name == location).Select(w => w.AvgTemp).Average(),2)
                };
                overviewList.Add(overviewObj);
            }

            return overviewList;
        }
    }
}
