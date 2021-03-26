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
    [Route("api/[controller]")]
    public class OverviewController : ControllerBase
    {
        private WeatherHelper _weatherHelper = new();
        private WebsiteHelper _websiteHelper = new();
        [HttpGet]
        public IActionResult Get()
        {
            var weatherOverviews = _weatherHelper.GetWeatherOverview(); ;
            var weatherLegenda = _websiteHelper.GetWeatherLegenda(weatherOverviews);
            weatherOverviews = _websiteHelper.GetWeatherCssLegendaClasses(weatherOverviews, weatherLegenda);
            var weatherOverviewViewModel = new WeatherOverviewViewModel()
            {
                WeatherOverviews = weatherOverviews,
                WeatherLegenda = weatherLegenda
            };

            return Ok(weatherOverviewViewModel);
        }
    }
}
