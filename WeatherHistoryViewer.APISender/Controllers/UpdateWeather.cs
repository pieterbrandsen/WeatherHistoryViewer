using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Services;
using WeatherHistoryViewer.Services.Handlers;

namespace WeatherHistoryViewer.APISender.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UpdateWeather : ControllerBase
    {
        private readonly ISecretRevealer _secretRevealer;
        private readonly IWeatherData _weatherData;
        private readonly IDateData _dateData;
        private readonly ILocationData _locationData;
        private readonly IHttpStatus _httpStatus;

        public UpdateWeather(ISecretRevealer secretRevealer, IWeatherData weatherData, IDateData dateData, ILocationData locationData, IHttpStatus httpStatus)
        {
            _secretRevealer = secretRevealer;
            _weatherData = weatherData;
            _dateData = dateData;
            _locationData = locationData;
            _httpStatus = httpStatus;
        }

        [HttpPost]
        public IActionResult AddLatestWeather(string access_key)
        {
            try
            {
                var weatherHistoryApiKey = _secretRevealer.RevealWeatherHistoryApiKey();
                if (weatherHistoryApiKey != access_key) return StatusCode(StatusCodes.Status400BadRequest, _httpStatus.GetErrorModel(HttpStatusTypes.invalid_acces_key));

                var oldestDate = _dateData.GetDateStringOfDaysAgo();
                var yesterdayDate = _dateData.GetDateStringOfDaysAgo(1);
                var locations = _locationData.GetAllLocationNames();
                Task.Run(() =>
                {
                    foreach (var locationName in locations)
                    {
                        _weatherData.UpdateHistoricalWeatherRangeToDb(locationName, HourlyInterval.Hours1, oldestDate,
                            yesterdayDate);
                    }
                });
                return Ok(new {message = "Updated all locations", locations});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, _httpStatus.GetErrorModel(HttpStatusTypes.request_failed));
            }
        }
    }
}
