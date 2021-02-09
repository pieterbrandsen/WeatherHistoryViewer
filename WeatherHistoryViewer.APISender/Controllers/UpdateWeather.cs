using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Services;
using WeatherHistoryViewer.Services.Handlers;

namespace WeatherHistoryViewer.APISender.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UpdateWeather : ControllerBase
    {
        private readonly IDateData _dateData;
        private readonly IHttpStatus _httpStatus;
        private readonly ILocationData _locationData;
        private readonly ISecretRevealer _secretRevealer;
        private readonly IWeatherData _weatherData;

        public UpdateWeather(ISecretRevealer secretRevealer, IWeatherData weatherData, IDateData dateData,
            ILocationData locationData, IHttpStatus httpStatus)
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
                if (weatherHistoryApiKey != access_key)
                    return StatusCode(StatusCodes.Status400BadRequest,
                        _httpStatus.GetErrorModel(HttpStatusTypes.invalid_acces_key));

                var oldestDate = _dateData.GetDateStringOfDaysAgo();
                var yesterdayDate = _dateData.GetDateStringOfDaysAgo(1);
                var locations = _locationData.GetAllLocationNames();
                Task.Run(() =>
                {
                    foreach (var locationName in locations)
                        _weatherData.UpdateHistoricalWeatherRangeToDb(locationName, HourlyInterval.Hours1, oldestDate,
                            yesterdayDate);
                });
                return Ok(new {message = "Updated all locations", locations});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    _httpStatus.GetErrorModel(HttpStatusTypes.request_failed));
            }
        }

        [HttpPost]
        public IActionResult AddWeatherForLocation(string access_key, string query, string oldest_date,
            string newest_date)
        {
            try
            {
                var weatherHistoryApiKey = _secretRevealer.RevealWeatherHistoryApiKey();
                if (weatherHistoryApiKey != access_key)
                    return StatusCode(StatusCodes.Status400BadRequest,
                        _httpStatus.GetErrorModel(HttpStatusTypes.invalid_acces_key));
                if (query == null)
                    return StatusCode(StatusCodes.Status400BadRequest,
                        _httpStatus.GetErrorModel(HttpStatusTypes.missing_query));

                Task.Run(() =>
                {
                    _weatherData.UpdateHistoricalWeatherRangeToDb(query, HourlyInterval.Hours3, oldest_date,
                        newest_date);
                });
                return Ok(new {message = "Updating weather from request"});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}