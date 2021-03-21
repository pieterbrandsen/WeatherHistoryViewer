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
    [Route("api/[controller]")]
    public class OverviewController : ControllerBase
    {
        private WeatherHelper _weatherHelper = new();

        [HttpGet]
        public IActionResult Get()
        {
            var weatherOverview = _weatherHelper.GetWeatherOverview();
            return Ok(weatherOverview);
        }
    }
}
