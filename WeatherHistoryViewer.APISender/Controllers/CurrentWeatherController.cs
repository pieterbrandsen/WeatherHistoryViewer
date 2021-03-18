using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;
using WeatherHistoryViewer.Services.Helpers;
using WeatherHistoryViewer.Services.Requester;

namespace WeatherHistoryViewer.APISender.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrentWeatherController : ControllerBase
    {
        private readonly HttpStatus _httpStatus;
        private readonly WeatherStackAPI _weatherApiRequester;

        public CurrentWeatherController()
        {
            _weatherApiRequester = new WeatherStackAPI();
            _httpStatus = new HttpStatus();
        }

        [HttpGet]
        public IActionResult GetCurrentWeather(string access_key, string query, string units = "m")
        {
            try
            {
                var weatherStackApiKey = UserSecrets.WeatherStackApiKey;
                var weatherHistoryApiKey = UserSecrets.WeatherHistoryApiKey;

                if (weatherHistoryApiKey != access_key)
                    return StatusCode(StatusCodes.Status400BadRequest,
                        _httpStatus.GetErrorModel(HttpStatusTypes.invalid_acces_key));
                if (query == null)
                    return StatusCode(StatusCodes.Status400BadRequest,
                        _httpStatus.GetErrorModel(HttpStatusTypes.missing_query));
                var weather = _weatherApiRequester.GetCurrentWeather(weatherStackApiKey, query, units);
                if (weather?.Request == null)
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        _httpStatus.GetErrorModel(HttpStatusTypes.no_results));
                return Ok(weather);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    _httpStatus.GetErrorModel(HttpStatusTypes.request_failed));
            }
        }
    }
}