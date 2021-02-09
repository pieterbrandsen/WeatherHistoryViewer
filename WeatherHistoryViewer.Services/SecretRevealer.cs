using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using WeatherHistoryViewer.Core.Models;

namespace WeatherHistoryViewer.Services
{
    public interface ISecretRevealer
    {
        public string RevealWeatherStackApiKey();
        public string RevealWeatherHistoryApiKey();
        public string RevealConnectionString();
    }

    public class SecretRevealer : ISecretRevealer
    {
        private readonly IConfiguration _configuration;

        public SecretRevealer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string RevealConnectionString()
        {
            return _configuration["UserSecrets:DefaultConnectionString"];
        }

        public string RevealWeatherHistoryApiKey()
        {
            return _configuration["UserSecrets:ApiKeys:WeatherHistoryViewer"];
        }

        public string RevealWeatherStackApiKey()
        {
            return _configuration["UserSecrets:ApiKeys:WeatherStack"];
        }
    }
}