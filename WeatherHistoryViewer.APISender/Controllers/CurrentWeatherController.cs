using Microsoft.AspNetCore.Mvc;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Services;

namespace WeatherHistoryViewer.APISender.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrentWeatherController : ControllerBase
    {
        private readonly IApiRequester _apiRequester;
        private readonly ISecretRevealer _secretRevealer;

        public CurrentWeatherController(IApiRequester apiRequester,
            ISecretRevealer secretRevealer)
        {
            _apiRequester = apiRequester;
            _secretRevealer = secretRevealer;
        }

        [HttpGet]
        public CurrentWeatherResponse Get(string city_name = "Baarn")
        {
            var userSecrets = _secretRevealer.RevealUserSecrets();
            var currentWeather = _apiRequester.GetCurrentWeather(userSecrets.ApiKeys.WeatherStack, city_name);
            return currentWeather;
        }
    }
}