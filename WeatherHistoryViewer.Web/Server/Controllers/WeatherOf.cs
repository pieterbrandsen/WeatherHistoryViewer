using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Services.Helpers;

namespace WeatherHistoryViewer.Web.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherOf : ControllerBase
    {
        private WeatherHelper _weatherHelper = new();

        private string _defaultDate = "2020-01-01";

        public WeatherOf()
        {
            _defaultDate = DateTime.Now.AddDays(-1).ToString("MM/dd");
        }
        [HttpGet]
        public IActionResult Years(string location= "Amsterdam")
        {
            var weatherOfYears = _weatherHelper.GetWeatherOfPastYears(location);
            return Ok(weatherOfYears);
        }
        [HttpGet]
        public IActionResult Weeks(string location = "Amsterdam", string date = null)
        {
            if (date == null) date = _defaultDate;

            var weatherOfWeeks = _weatherHelper.GetWeatherWeekOfDateInThePastYears(location, date);
            return Ok(weatherOfWeeks);
        }

        [HttpGet]
        public IActionResult Days(string location = "Amsterdam", string date = null)
        {
            if (date == null) date = _defaultDate;

            var weatherOfDays = _weatherHelper.GetWeatherOfDayInThePastYears(location, date);
            return Ok(weatherOfDays);
        }
    }
}
