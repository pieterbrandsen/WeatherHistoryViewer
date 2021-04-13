using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherHistoryViewer.Core.Models;
using WeatherHistoryViewer.Services.Handlers;

namespace WeatherHistoryViewer.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddWeatherDataController : ControllerBase
    {
        private readonly WeatherHandler _weatherHandler = new();

        [HttpPost]
        public IActionResult Index(AddWeatherData form)
        {
            Task.Run(() =>
            {
                _weatherHandler.UpdateHistoricalWeatherRangeToDb(form.Location, form.OldestDateString,
                    form.NewestDateString);
                new DataWarehouseHandlers().UpdateWeatherWarehouse();
            });
            return Ok();
        }
    }
}