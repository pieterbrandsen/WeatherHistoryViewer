using Microsoft.AspNetCore.Mvc;
using WeatherHistoryViewer.Core.ViewModels;
using WeatherHistoryViewer.Services.Helpers;

namespace WeatherHistoryViewer.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OverviewController : ControllerBase
    {
        private readonly LegendaHelper _legendaHelper = new();
        private readonly WeatherHelper _weatherHelper = new();

        [HttpGet]
        [ResponseCache(Duration = 60 * 60 * 24 * 30)]
        public IActionResult Get()
        {
            var weatherOverviews = _weatherHelper.GetWeatherOverview();
            var weatherLegenda = _legendaHelper.GetWeatherLegenda(weatherOverviews);
            weatherOverviews = _legendaHelper.GetWeatherWithCssLegendaClasses(weatherOverviews, weatherLegenda);
            var weatherOverviewViewModel = new WeatherOverviewViewModel
            {
                WeatherOverviews = weatherOverviews,
                WeatherLegenda = weatherLegenda
            };

            return Ok(weatherOverviewViewModel);
        }
    }
}