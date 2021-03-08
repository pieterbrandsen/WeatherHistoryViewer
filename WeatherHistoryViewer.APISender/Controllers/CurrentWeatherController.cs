using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;
using WeatherHistoryViewer.Services;
using WeatherHistoryViewer.Services.Requester;

namespace WeatherHistoryViewer.APISender.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrentWeatherController : ControllerBase
    {
        private readonly WeatherStackAPI _weatherApiRequester;
        private readonly HttpStatus _httpStatus;
        private readonly UserSecrets _secrets;

        public CurrentWeatherController()
        {
            _weatherApiRequester = new WeatherStackAPI();
            _secrets = new UserSecrets();
            _httpStatus = new HttpStatus();
        }

        [HttpGet]
        public IActionResult GetCurrentWeather(string access_key, string query, string units = "m")
        {
            try
            {
                var weatherStackApiKey = _secrets.WeatherStackApiKey;
                var weatherHistoryApiKey = _secrets.WeatherHistoryApiKey;

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