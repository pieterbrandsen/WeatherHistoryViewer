using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Core.ViewModels;
using WeatherHistoryViewer.Services.Helpers;

namespace WeatherHistoryViewer.Web.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherOf : ControllerBase
    {
        private WeatherHelper _weatherHelper = new();
        private WebsiteHelper _websiteHelper = new();

        private string _defaultDate = "2020-01-01";

        public WeatherOf()
        {
            _defaultDate = DateTime.Now.AddDays(-1).ToString("MM/dd");
        }
        [HttpGet]
        public IActionResult Years(string location= "Amsterdam")
        {
            var weatherOfYears = _weatherHelper.GetWeatherOfPastYears(location);
            var weatherLegenda = _websiteHelper.GetWeatherLegenda(weatherOfYears);
            weatherOfYears = _websiteHelper.GetWeatherCssLegendaClasses(weatherOfYears, weatherLegenda);
            var weatherOfYearsViewModel = new WeatherOfYearsViewModel()
            {
                WeatherOverviews = weatherOfYears,
                WeatherLegenda = weatherLegenda
            };
            return Ok(weatherOfYearsViewModel);
        }
        [HttpGet]
        public IActionResult Week(string location = "Amsterdam", string date = null)
        {
            if (date == null) date = _defaultDate;

            var weatherOfWeek = _weatherHelper.GetWeatherWeekOfDateInThePastYears(location, date);
            var weatherOfWeekSimpleList = new List<HistoricalWeather>();
            weatherOfWeek.ForEach(w =>
            {
                var weather = new HistoricalWeather()
                {
                    MaxTemp = Math.Round(w.Select(w => w.MaxTemp).Average(), 2),
                    AvgTemp = Math.Round(w.Select(w => w.AvgTemp).Average(), 2),
                    MinTemp = Math.Round(w.Select(w => w.MinTemp).Average(), 2),
                    SunHour = Math.Round(w.Select(w => w.SunHour).Average(), 2),
                };
                weatherOfWeekSimpleList.Add(weather);
            });
            var weatherLegenda = _websiteHelper.GetWeatherLegenda(weatherOfWeekSimpleList);
            var averageHistoricalWeatherEachWeek = _websiteHelper.GetWeatherCssLegendaClasses(weatherOfWeekSimpleList, weatherLegenda,true);
            var weatherOfWeekViewModel = new WeatherOfWeekViewModel()
            {
                HistoricalWeathers = weatherOfWeek,
                AverageHistoricalWeatherEachWeek =  averageHistoricalWeatherEachWeek,
                WeatherLegenda = weatherLegenda
            };
            return Ok(weatherOfWeekViewModel);
        }

        [HttpGet]
        public IActionResult Day(string location = "Amsterdam", string date = null)
        {
            if (date == null) date = _defaultDate;

            var weatherOfDay = _weatherHelper.GetWeatherOfDayInThePastYears(location,date);
            var weatherLegenda = _websiteHelper.GetWeatherLegenda(weatherOfDay);
            weatherOfDay = _websiteHelper.GetWeatherCssLegendaClasses(weatherOfDay, weatherLegenda);
            var weatherOfDayViewModel = new WeatherOfDayViewModel()
            {
                HistoricalWeathers = weatherOfDay,
                WeatherLegenda = weatherLegenda
            };
            return Ok(weatherOfDayViewModel);
        }
    }
}
