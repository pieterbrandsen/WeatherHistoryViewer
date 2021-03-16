using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;
using WeatherHistoryViewer.Services.Handlers; //using System.Data.Entity;

namespace WeatherHistoryViewer.Services.Helper
{
    public class WeatherHelper
    {
        private readonly DateHelper dateHelper = new();
        private readonly LocationHandler locationHandler = new();

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

        public (string date, double temp) GetMaxOrMinTempOfLocation(bool returnMax, string locationName, string year  = null)
        {
            using var context = new ApplicationDbContext();
            var weatherList = new List<HistoricalWeather>();
            var tempIsAssigned = false;
            var temp = double.NaN;


                // Sequence  contains no elements
                if (returnMax)
                {
                    weatherList = context.Weather.Include(w => w.Location).Where(w =>
                            w.Location.Name == locationName && (year != null && w.Date.Contains(year) || year == null))
                        .ToList();
                    if (weatherList.Count > 0)
                    {
                        tempIsAssigned = true;
                       temp = weatherList.Max(w => w.MaxTemp);
                    }
                }
            else {
                    weatherList = context.Weather.Include(w => w.Location).Where(w =>
                            w.Location.Name == locationName && (year != null && w.Date.Contains(year) || year == null))
                        .ToList();
                if (weatherList.Count > 0)
                {
                    tempIsAssigned = true;
                    temp = weatherList.Min(w => w.MinTemp);
                }
            }

            if (tempIsAssigned) {
            string date = weatherList.FirstOrDefault(w => (returnMax ? w.MaxTemp : w.MinTemp) == temp).Date;
            return (date, temp);
            }

            return (null, temp);
        }

        public List<WeatherOverview> GetWeatherOverview()
        {
            using var context = new ApplicationDbContext();
            var overviewList = new List<WeatherOverview>();

            try
            {
                var locations = locationHandler.GetAllLocationNames();
                foreach (var location in locations)
                {
                    (var dateOfMaxTemp, var maxTemp) = GetMaxOrMinTempOfLocation(true, location);
                    (var dateOfMinTemp, var minTemp) = GetMaxOrMinTempOfLocation(false, location);
                    var overviewObj = new WeatherOverview
                    {
                        LocationName = location,
                        MaxTemp = Math.Round(maxTemp, 2),
                        DateOfMaxTemp = dateOfMaxTemp,
                        MinTemp = Math.Round(minTemp, 2),
                        DateOfMinTemp = dateOfMinTemp,
                        AverageSunHours =
                            Math.Round(
                                context.Weather.Include(w => w.Location).Where(w => w.Location.Name == location)
                                    .Select(w => w.SunHour).Average(), 2),
                        AverageTemp =
                            Math.Round(
                                context.Weather.Include(w => w.Location).Where(w => w.Location.Name == location)
                                    .Select(w => w.AvgTemp).Average(), 2)
                    };
                    overviewList.Add(overviewObj);
                }

                context.Dispose();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return overviewList;
        }

        public List<List<HistoricalWeather>> GetWeatherWeekOfDateInThePastYears(string cityName, string date)
        {
            using var context = new ApplicationDbContext();
            var weatherList = new List<List<HistoricalWeather>>();
            try
            {
                var dates = dateHelper.GetDateInLast15Y(date);
                foreach (var currDate in dates)
                {
                    var currDates = dateHelper.GetWeekDatesFromDate(currDate);
                    var weather = context.Weather.Include(w => w.Location).Include(w => w.SnapshotsOfDay)
                   .Where(w => w.Location.Name == cityName && currDates.Contains(w.Date))
                   .OrderByDescending(o => o.DateEpoch).ToList();
                    weatherList.Add(weather);
                }

               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return weatherList.FindAll(wl => wl.Count > 0);
        }

        public List<WeatherOverview> GetWeatherOfPastYears(string cityName)
        {
            using var context = new ApplicationDbContext();
            var overviewList = new List<WeatherOverview>();

            try
            {
                var dates = dateHelper.GetDateInLast15Y(DateTime.Now.AddDays(-1).ToString("MM/dd"));
                foreach (var date in dates)
                {
                    var year = date.Split("-")[0];
                    (var dateOfMaxTemp, var maxTemp) = GetMaxOrMinTempOfLocation(true, cityName, year);
                    (var dateOfMinTemp, var minTemp) = GetMaxOrMinTempOfLocation(false, cityName, year);
                    if (dateOfMaxTemp == null && dateOfMinTemp == null) continue;
                    var overviewObj = new WeatherOverview
                    {
                        Year = year,
                        LocationName = cityName,
                        MaxTemp = Math.Round(maxTemp, 2),
                        DateOfMaxTemp = dateOfMaxTemp.Split("-")[1]+"/"+ dateOfMaxTemp.Split("-")[2],
                        MinTemp = Math.Round(minTemp, 2),
                        DateOfMinTemp = dateOfMinTemp.Split("-")[1] + "/" + dateOfMinTemp.Split("-")[2],
                        AverageSunHours =
                            Math.Round(
                                context.Weather.Include(w => w.Location).Where(w => w.Location.Name == cityName && w.Date.Contains(year))
                                    .Select(w => w.SunHour).Average(), 2),
                        AverageTemp =
                            Math.Round(
                                context.Weather.Include(w => w.Location).Where(w => w.Location.Name == cityName && w.Date.Contains(year))
                                    .Select(w => w.AvgTemp).Average(), 2)
                    };

                    overviewList.Add(overviewObj);
                }

                context.Dispose();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return overviewList;
        }
    }
}