using Microsoft.AspNetCore.Mvc;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Db;
using WeatherHistoryViewer.Services;

namespace WeatherHistoryViewer.APISender.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrentWeatherController : ControllerBase
    {
        private readonly IApiRequester _apiRequester;
        private readonly ApplicationDbContext _context;
        private readonly ISecretRevealer _secretRevealer;

        public CurrentWeatherController(ApplicationDbContext context, IApiRequester apiRequester,
            ISecretRevealer secretRevealer)
        {
            _context = context;
            _apiRequester = apiRequester;
            _secretRevealer = secretRevealer;
        }

        [HttpGet]
        public CurrentWeatherResponse Get()
        {
            var userSecrets = _secretRevealer.RevealUserSecrets();
            var apiKey = userSecrets.ApiKeys.WeatherStack;
            var currentWeather = _apiRequester.GetCurrentWeather(apiKey, "Baarn");
            return currentWeather;
        }
    }
}