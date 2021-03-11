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

        public (string date, double temp) GetMaxOrMinTempOfLocation(bool returnMax)
    {
            using var context = new ApplicationDbContext();
            double temp = double.NaN;
            string date = null;

            if (returnMax) {
                temp = context.Weather.Max(w => w.MaxTemp);
            }
            else {
                temp = context.Weather.Min(w => w.MinTemp);
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
                (string dateOfMaxTemp, double maxTemp) = GetMaxOrMinTempOfLocation(true);
                (string dateOfMinTemp, double minTemp) = GetMaxOrMinTempOfLocation(false);
                var overviewObj = new WeatherOverview()
                {
                    LocationName = location,
                    MaxTemp = maxTemp,
                    DateOfMaxTemp = dateOfMaxTemp,
                    MinTemp = minTemp,
                    DateOfMinTemp = dateOfMinTemp,
                    AverageSunHours = context.Weather.Select(w => w.SunHour).Average(),
                    AverageTemp = context.Weather.Select(w => w.AvgTemp).Average(),
                };
                overviewList.Add(overviewObj);
            }

            return overviewList;
        }
    }
}
