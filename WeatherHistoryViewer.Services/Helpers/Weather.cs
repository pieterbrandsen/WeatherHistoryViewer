using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WeatherHistoryViewer.Core.Models;
using WeatherHistoryViewer.Core.Models.DataWarehouse;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;
using WeatherHistoryViewer.Services.Handlers;

//using System.Data.Entity;

namespace WeatherHistoryViewer.Services.Helpers
{
    public class WeatherHelper
    {
        private readonly DateHelper _dateHelper = new();
        private readonly LocationHandler _locationHandler = new();

        public bool DoesWeatherWarehouseNeedToBeUpdated(MinDaysBeforeUpdatingWeather type)
        {
            var result = true;
            using var context = new ApplicationDbContext();
            var lastUpdateTime = context.LastUpdateTimes.Find("Weather");
            if (lastUpdateTime == null)
            {
            }
            else if ((DateTime.Now - DateTime.Parse(lastUpdateTime.Date)).Days + (int) type <= 0)
            {
                lastUpdateTime.Date = DateTime.Now.ToShortDateString();
                context.SaveChanges();
            }
            else
            {
                result = false;
            }

            return result;
        }

        public List<WeatherOverview> GetWeatherOverview()
        {
            if (DoesWeatherWarehouseNeedToBeUpdated(MinDaysBeforeUpdatingWeather.Overview))
                new DataWarehouseHandlers().UpdateWeatherWarehouse();

            var overviewList = new List<WeatherOverview>();
            using var context = new ApplicationDbContext();
            try
            {
                var weatherLocations = context.WeatherWarehouse.Include(w => w.Location).Include(w => w.Time)
                    .Include(w => w.WeatherMeasurment).ToList().GroupBy(w => w.Location.LocationName)
                    .OrderBy(s => s.Key).ToList();
                foreach (var weatherList in weatherLocations)
                {
                    var highestMaxTemp = new TempModel {Temp = double.NegativeInfinity};
                    var lowestMinTemp = new TempModel {Temp = double.PositiveInfinity};
                    foreach (var weather in weatherList)
                        if (weather.WeatherMeasurment.MaxTemp > highestMaxTemp.Temp)
                        {
                            highestMaxTemp.Temp = weather.WeatherMeasurment.MaxTemp;
                            highestMaxTemp.Date = weather.Time.Date;
                        }
                        else if (weather.WeatherMeasurment.MinTemp < lowestMinTemp.Temp)
                        {
                            lowestMinTemp.Temp = weather.WeatherMeasurment.MinTemp;
                            lowestMinTemp.Date = weather.Time.Date;
                        }

                    var weatherMeasurementList = weatherList.Select(w => w.WeatherMeasurment).ToList();
                    var overview = new WeatherOverview
                    {
                        LocationName = weatherList.Key,
                        AvgTemp = Math.Round(weatherMeasurementList.Select(w => w.AvgTemp).Average(), 2),
                        SunHour = Math.Round(weatherMeasurementList.Select(w => w.SunHour).Average(), 2),
                        MaxTemp = highestMaxTemp.Temp,
                        DateOfMaxTemp = highestMaxTemp.Date,
                        MinTemp = lowestMinTemp.Temp,
                        DateOfMinTemp = lowestMinTemp.Date
                    };
                    overviewList.Add(overview);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return overviewList;
        }

        public List<WeatherOverview> GetWeatherOfPastYears(string cityName)
        {
            using var context = new ApplicationDbContext();
            var overviewList = new List<WeatherOverview>();

            if (DoesWeatherWarehouseNeedToBeUpdated(MinDaysBeforeUpdatingWeather.Years))
                new DataWarehouseHandlers().UpdateWeatherWarehouse();

            try
            {
                var weatherYears = context.WeatherWarehouse.Include(w => w.Location).Include(w => w.Time)
                    .Include(w => w.WeatherMeasurment).Where(w => w.Location.LocationName == cityName).ToList()
                    .GroupBy(s => s.Time.Year).OrderByDescending(s => s.Key).ToList();
                foreach (var weatherList in weatherYears)
                {
                    var highestMaxTemp = new TempModel {Temp = -99};
                    var lowestMinTemp = new TempModel {Temp = 99};
                    foreach (var weather in weatherList)
                    {
                        if (weather.WeatherMeasurment.MaxTemp > highestMaxTemp.Temp)
                        {
                            highestMaxTemp.Temp = weather.WeatherMeasurment.MaxTemp;
                            highestMaxTemp.Date = weather.Time.Date;
                        }

                        if (weather.WeatherMeasurment.MinTemp < lowestMinTemp.Temp)
                        {
                            lowestMinTemp.Temp = weather.WeatherMeasurment.MinTemp;
                            lowestMinTemp.Date = weather.Time.Date;
                        }
                    }

                    var weatherMeasurementList = weatherList.Select(w => w.WeatherMeasurment).ToList();
                    var shortDateOfMaxTemp = highestMaxTemp.Date.Substring(highestMaxTemp.Date.IndexOf('/') + 1);
                    var shortDateOfLowestTemp = lowestMinTemp.Date.Substring(lowestMinTemp.Date.IndexOf('/') + 1);
                    var overview = new WeatherOverview
                    {
                        LocationName = weatherList.First().Location.LocationName,
                        Year = weatherList.Key.ToString(),
                        AvgTemp = Math.Round(weatherMeasurementList.Select(w => w.AvgTemp).Average(), 2),
                        SunHour = Math.Round(weatherMeasurementList.Select(w => w.SunHour).Average(), 2),
                        MaxTemp = highestMaxTemp.Temp,
                        DateOfMaxTemp = shortDateOfMaxTemp,
                        MinTemp = lowestMinTemp.Temp,
                        DateOfMinTemp = shortDateOfLowestTemp
                    };

                    overviewList.Add(overview);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return overviewList;
        }

        public List<List<HistoricalWeather>> GetWeatherWeekOfDate(string cityName, string date)
        {
            if (DoesWeatherWarehouseNeedToBeUpdated(MinDaysBeforeUpdatingWeather.Week))
                new DataWarehouseHandlers().UpdateWeatherWarehouse();

            using var context = new ApplicationDbContext();
            var weatherListList = new List<List<HistoricalWeather>>();
            try
            {
                var weatherYears = context.WeatherWarehouse.Include(w => w.Location).Include(w => w.Time)
                    .Include(w => w.WeatherMeasurment).Where(w => w.Location.LocationName == cityName).ToList()
                    .GroupBy(s => s.Time.Year).OrderByDescending(s => s.Key).ToList();
                foreach (var weatherList in weatherYears)
                {
                    var shortDate = date.Split("/").Length > 1 ? date.Substring(date.IndexOf('/') + 1) : date;
                    var weatherOfDate = weatherList.FirstOrDefault(s => s.Time.Date.Contains(shortDate));
                    if (weatherOfDate == null)
                    {
                        continue;
                    }

                    var currentDates = _dateHelper.GetWeekDatesFromDate(weatherOfDate.Time.Date);
                        var newWeatherList = new List<HistoricalWeather>();
                        currentDates.ForEach(currentDate=> {
                            var weatherMeasurement = weatherList.FirstOrDefault(w => w.Time.Date == currentDate);
                            if (weatherMeasurement != null)
                            {
                                var historicalWeather = new HistoricalWeather
                                {
                                    AvgTemp = weatherMeasurement.WeatherMeasurment.AvgTemp,
                                    Date = weatherMeasurement.Time.Date,
                                    Location = _locationHandler.GetLocation(weatherMeasurement.Location.LocationName),
                                    MaxTemp = weatherMeasurement.WeatherMeasurment.MaxTemp,
                                    MinTemp = weatherMeasurement.WeatherMeasurment.MinTemp,
                                    SunHour = weatherMeasurement.WeatherMeasurment.SunHour
                                };
                                newWeatherList.Add(historicalWeather);
                            }
                        });

                        weatherListList.Add(newWeatherList);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return weatherListList;
        }

        public List<HistoricalWeather> GetWeatherOfDay(string cityName, string date)
        {
            using var context = new ApplicationDbContext();
            var historicalWeatherList = new List<HistoricalWeather>();
            try
            {
                var weatherYears = context.WeatherWarehouse.Include(w => w.Location).Include(w => w.Time)
                    .Include(w => w.WeatherMeasurment).Where(w => w.Location.LocationName == cityName).ToList()
                    .GroupBy(s => s.Time.Year).OrderByDescending(s => s.Key).ToList();
                foreach (var weatherList in weatherYears)
                {
                    var shortDate = date.Split("/").Length > 1 ? date.Substring(date.IndexOf('/') + 1) : date;
                    var weatherOfDate = weatherList.FirstOrDefault(s => s.Time.Date.Contains(shortDate));
                    if (weatherOfDate == null)
                    {
                        continue;
                    }

                    var historicalWeather = new HistoricalWeather
                        {
                            AvgTemp = weatherOfDate.WeatherMeasurment.AvgTemp,
                            Date = weatherOfDate.Time.Year.ToString(),
                            Location = _locationHandler.GetLocation(weatherOfDate.Location.LocationName),
                            MaxTemp = weatherOfDate.WeatherMeasurment.MaxTemp,
                            MinTemp = weatherOfDate.WeatherMeasurment.MinTemp,
                            SunHour = weatherOfDate.WeatherMeasurment.SunHour
                        };
                        historicalWeatherList.Add(historicalWeather);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return historicalWeatherList;
        }

        private class TempModel
        {
            public double Temp { get; set; }
            public string Date { get; set; }
        }
    }
}