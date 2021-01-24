using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherHistoryViewer.Core;
using WeatherHistoryViewer.Core.Models;
using WeatherHistoryViewer.Db;

namespace WeatherHistoryViewer.APISender.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrentWeatherController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CurrentWeatherController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public WeatherModel Get()
        {
            if (_context.Weather.ToList().Count() == 0) return new WeatherModel();

            WeatherModel weatherModel = _context.Weather.Include(i => i.CurrentWeather).Include(i => i.Location).OrderBy(i => i).Last();
            return weatherModel;
        }
    }
}
