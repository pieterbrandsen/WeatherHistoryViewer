using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherHistoryViewer.Core.Models;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Services.Handlers;

namespace WeatherHistoryViewer.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddWeatherDataController : ControllerBase
    {
        private readonly WeatherHandler _weatherHandler = new();
       
        [HttpPost]
        public IActionResult Index(AddWeatherDataForm form)
        {
            Task.Run(() => { _weatherHandler.UpdateHistoricalWeatherRangeToDb(form.Location, form.OldestDate.ToString("yyyy/MM/dd"), form.NewestDate.ToString("yyyy/MM/dd")); new DataWarehouseHandlers().UpdateWeatherWarehouse(); });
            return Ok();
        }
    }
}
