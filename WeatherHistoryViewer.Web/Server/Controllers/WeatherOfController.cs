using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherHistoryViewer.Core.Constants;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Core.ViewModels;
using WeatherHistoryViewer.Services.Helpers;

namespace WeatherHistoryViewer.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherOfController : ControllerBase
    {
        private readonly string _defaultDate = "2020/01/01";
        private readonly LegendHelper _legendHelper = new();
        private readonly WeatherHelper _weatherHelper = new();
        private readonly ILogger _logger;

        public WeatherOfController(ILogger<WeatherOfController> logger)
        {
            _defaultDate = DateTime.Now.AddDays(-1).ToString("MM/dd");
            _logger = logger;
        }

        [HttpGet]
        [ResponseCache(Duration = 60 * 60 * 24 * 30, VaryByQueryKeys = new[] {"location"})]
        public IActionResult Years(string location = WeatherConstants.DefaultLocationName)
        {
            _logger.LogInformation("Started loading YearsPage");
            var weatherOfYears = _weatherHelper.GetWeatherOfPastYears(location);
            var weatherLegend = _legendHelper.GetWeatherLegend(weatherOfYears);
            weatherOfYears = _legendHelper.GetWeatherWithLegendClasses(weatherOfYears, weatherLegend);
            var weatherOfYearsViewModel = new WeatherOfYearsViewModel
            {
                WeatherOverviews = weatherOfYears,
                WeatherLegend = weatherLegend
            };
            _logger.LogInformation("Finished loading YearsPage");
            return Ok(weatherOfYearsViewModel);
        }

        [HttpGet]
        [ResponseCache(Duration = 60 * 60 * 24 * 1, VaryByQueryKeys = new[] {"location", "date"})]
        public IActionResult Week(string location = WeatherConstants.DefaultLocationName, string date = null)
        {
            _logger.LogInformation("Started loading WeekPage");

            date ??= _defaultDate;
            date = date.Replace("-", "/");

            var weatherOfWeek = _weatherHelper.GetWeatherWeekOfDate(location, date);
            var weatherOfWeekSimpleList = new List<HistoricalWeather>();
            weatherOfWeek.ForEach(wl =>
            {
                var weather = new HistoricalWeather
                {
                    MaxTemp = Math.Round(wl.Select(h => h.MaxTemp).Average(), 2),
                    AvgTemp = Math.Round(wl.Select(h => h.AvgTemp).Average(), 2),
                    MinTemp = Math.Round(wl.Select(h => h.MinTemp).Average(), 2),
                    SunHour = Math.Round(wl.Select(h => h.SunHour).Average(), 2)
                };
                weatherOfWeekSimpleList.Add(weather);
            });

            var weatherLegend = _legendHelper.GetWeatherLegend(weatherOfWeekSimpleList);
            var averageHistoricalWeatherEachWeek =
                _legendHelper.GetWeatherWithLegendClasses(weatherOfWeekSimpleList, weatherLegend, true);
            var weatherOfWeekViewModel = new WeatherOfWeekViewModel
            {
                HistoricalWeather = weatherOfWeek,
                AverageHistoricalWeatherEachWeek = averageHistoricalWeatherEachWeek,
                WeatherLegend = weatherLegend
            };
            _logger.LogInformation("Finished loading WeekPage");
            return Ok(weatherOfWeekViewModel);
        }

        [HttpGet]
        [ResponseCache(Duration = 60 * 60 * 12, VaryByQueryKeys = new[] { "location", "date" })]
        public IActionResult Day(string location = WeatherConstants.DefaultLocationName, string date = null)
        {
            _logger.LogInformation("Started loading DayPage");

            date ??= _defaultDate;
            date = date.Replace("-", "/");

            var weatherOfDay = _weatherHelper.GetWeatherOfDay(location, date);
            var weatherLegend = _legendHelper.GetWeatherLegend(weatherOfDay);
            weatherOfDay = _legendHelper.GetWeatherWithLegendClasses(weatherOfDay, weatherLegend);
            var weatherOfDayViewModel = new WeatherOfDayViewModel
            {
                HistoricalWeather = weatherOfDay,
                WeatherLegend = weatherLegend
            };
            _logger.LogInformation("Finished loading DayPage");
            return Ok(weatherOfDayViewModel);
        }
    }
}