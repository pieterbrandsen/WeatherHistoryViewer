using Microsoft.AspNetCore.Mvc;
using WeatherHistoryViewer.Services.Handlers;

namespace WeatherHistoryViewer.Web.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class LocationController : ControllerBase
    {
        private readonly LocationHandler _locationHandler = new();

        [HttpGet]
        public IActionResult GetLocationNames()
        {
            return Ok(_locationHandler.GetLocationNames());
        }
    }
}