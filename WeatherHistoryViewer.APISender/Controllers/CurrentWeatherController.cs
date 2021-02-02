using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Services;

namespace WeatherHistoryViewer.APISender.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrentWeatherController : ControllerBase
    {
        private readonly IApiRequester _apiRequester;
        private readonly ISecretRevealer _secretRevealer;
        private readonly IHttpStatus _httpStatus;

        public CurrentWeatherController(IApiRequester apiRequester,
            ISecretRevealer secretRevealer, IHttpStatus httpStatus)
        {
            _apiRequester = apiRequester;
            _secretRevealer = secretRevealer;
            _httpStatus = httpStatus;
        }

        [HttpGet]
        public IActionResult GetCurrentWeather(string access_key, string query, string units= "m")
        {
            try
            {
                var weatherStackApiKey = _secretRevealer.RevealWeatherStackApiKey();
                var weatherHistoryApiKey = _secretRevealer.RevealWeatherHistoryApiKey();

                if (weatherHistoryApiKey != access_key) return StatusCode(StatusCodes.Status400BadRequest, _httpStatus.GetErrorModel(HttpStatusTypes.invalid_acces_key));
                if (query == null) return StatusCode(StatusCodes.Status400BadRequest, _httpStatus.GetErrorModel(HttpStatusTypes.missing_query));
                var weather = _apiRequester.GetCurrentWeather(weatherStackApiKey, query, units);
                if (weather?.Request == null)
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        _httpStatus.GetErrorModel(HttpStatusTypes.no_results));
                return Ok(weather);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, _httpStatus.GetErrorModel(HttpStatusTypes.request_failed));
            }

        }
    }
}