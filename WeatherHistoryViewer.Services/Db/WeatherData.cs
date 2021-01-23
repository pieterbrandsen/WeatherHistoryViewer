using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHistoryViewer.Core;
using WeatherHistoryViewer.Db;
using WeatherHistoryViewer.Services.API;

namespace WeatherHistoryViewer.Services.Db
{
    public interface IWeatherData
    {
        public void AddCurrentWeatherToDB();
    }
    public class WeatherData : IWeatherData
    {
        private readonly ApplicationDbContext _context;
        private readonly ISecretRevealer _secretRevealer;
        private readonly IRequester _requester;

        public WeatherData(ApplicationDbContext context, ISecretRevealer secretRevealer, IRequester requester)
        {
            _context = context;
            _secretRevealer = secretRevealer;
            _requester = requester;
        }
        public void AddCurrentWeatherToDB()
        {
            SecretKeys secrets = _secretRevealer.RevealSecretKeys();
            CurrentWeatherHTTPResponse response = _requester.GetCurrentWeather(secrets.WeatherStack);
            _context.Weather.Add(response.Current);
            _context.SaveChanges();
        }
    }
}
