using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherHistoryViewer.Services.Handlers;

namespace WeatherHistoryViewer.Web.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class LocationController : ControllerBase
    {
        private LocationHandler _locationHanlder = new();

        [HttpGet]
        public IActionResult GetLocationNames()
        {
            return Ok(_locationHanlder.GetLocationNames());
        }
    }
}
