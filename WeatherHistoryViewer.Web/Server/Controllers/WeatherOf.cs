using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Core.ViewModels;
using WeatherHistoryViewer.Services.Helpers;

namespace WeatherHistoryViewer.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherOf : ControllerBase
    {
        private readonly string _defaultDate = "2020/01/01";
        private readonly LegendaHelper _legendaHelper = new();
        private readonly WeatherHelper _weatherHelper = new();

        public WeatherOf()
        {
            _defaultDate = DateTime.Now.AddDays(-1).ToString("MM/dd");
        }

        [HttpGet]
        [ResponseCache(Duration = 60 * 60 * 24 * 30, VaryByQueryKeys = new[] {"location"})]
        public IActionResult Years(string location = "Amsterdam")
        {
            var weatherOfYears = _weatherHelper.GetWeatherOfPastYears(location);
            var weatherLegenda = _legendaHelper.GetWeatherLegenda(weatherOfYears);
            weatherOfYears = _legendaHelper.GetWeatherWithCssLegendaClasses(weatherOfYears, weatherLegenda);
            var weatherOfYearsViewModel = new WeatherOfYearsViewModel
            {
                WeatherOverviews = weatherOfYears,
                WeatherLegenda = weatherLegenda
            };
            return Ok(weatherOfYearsViewModel);
        }

        [HttpGet]
        [ResponseCache(Duration = 60 * 60 * 24 * 1, VaryByQueryKeys = new[] {"location", "date"})]
        public IActionResult Week(string location = "Amsterdam", string date = null)
        {
            date ??= _defaultDate;
            date = date.Replace("-", "/");

            var weatherOfWeek = _weatherHelper.GetWeatherWeekOfDate(location, date);
            var weatherOfWeekSimpleList = new List<HistoricalWeather>();
            weatherOfWeek.ForEach(w =>
            {
                var weather = new HistoricalWeather
                {
                    MaxTemp = Math.Round(w.Select(w => w.MaxTemp).Average(), 2),
                    AvgTemp = Math.Round(w.Select(w => w.AvgTemp).Average(), 2),
                    MinTemp = Math.Round(w.Select(w => w.MinTemp).Average(), 2),
                    SunHour = Math.Round(w.Select(w => w.SunHour).Average(), 2)
                };
                weatherOfWeekSimpleList.Add(weather);
            });
            var weatherLegenda = _legendaHelper.GetWeatherLegenda(weatherOfWeekSimpleList);
            var averageHistoricalWeatherEachWeek =
                _legendaHelper.GetWeatherWithCssLegendaClasses(weatherOfWeekSimpleList, weatherLegenda, true);
            var weatherOfWeekViewModel = new WeatherOfWeekViewModel
            {
                HistoricalWeather = weatherOfWeek,
                AverageHistoricalWeatherEachWeek = averageHistoricalWeatherEachWeek,
                WeatherLegenda = weatherLegenda
            };
            return Ok(weatherOfWeekViewModel);
        }

        [HttpGet]
        [ResponseCache(Duration = 60 * 60 * 12, VaryByQueryKeys = new[] { "location", "date" })]
        public IActionResult Day(string location = "Amsterdam", string date = null)
        {
            date ??= _defaultDate;
            date = date.Replace("-", "/");

            var weatherOfDay = _weatherHelper.GetWeatherOfDay(location, date);
            var weatherLegenda = _legendaHelper.GetWeatherLegenda(weatherOfDay);
            weatherOfDay = _legendaHelper.GetWeatherWithCssLegendaClasses(weatherOfDay, weatherLegenda);
            var weatherOfDayViewModel = new WeatherOfDayViewModel
            {
                HistoricalWeather = weatherOfDay,
                WeatherLegenda = weatherLegenda
            };
            return Ok(weatherOfDayViewModel);
        }
    }
}