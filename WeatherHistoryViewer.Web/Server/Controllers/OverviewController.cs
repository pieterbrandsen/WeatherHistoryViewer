using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherHistoryViewer.Core.ViewModels;
using WeatherHistoryViewer.Services.Helpers;

namespace WeatherHistoryViewer.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OverviewController : ControllerBase
    {
        private readonly LegendHelper _legendHelper = new();
        private readonly WeatherHelper _weatherHelper = new();
        private readonly ILogger _logger;

        public OverviewController(ILogger<OverviewController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ResponseCache(Duration = 60 * 60 * 24 * 30)]
        public IActionResult Get()
        {
            _logger.LogInformation("Started loading OverviewPage");

            var weatherOverviews = _weatherHelper.GetWeatherOverview();
            var weatherLegend = _legendHelper.GetWeatherLegend(weatherOverviews);
            weatherOverviews = _legendHelper.GetWeatherListWithLegendClasses(weatherOverviews, weatherLegend);
            var weatherOverviewViewModel = new WeatherOverviewViewModel
            {
                WeatherOverviews = weatherOverviews,
                WeatherLegend = weatherLegend
            };

            _logger.LogInformation("Finished loading OverviewPage");
            return Ok(weatherOverviewViewModel);
        }
    }
}