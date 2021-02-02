using System;
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
        private readonly UserSecrets _userSecrets;

        // I’ve injected <em>secrets</em> into the constructor as setup in Program.cs
        public SecretRevealer(IOptions<UserSecrets> userSecrets)
        {
            // We want to know if secrets is null so we throw an exception if it is
            _userSecrets = userSecrets.Value ?? throw new ArgumentNullException(nameof(_userSecrets));
        }

        public string RevealConnectionString()
        {
            return _userSecrets.DefaultConnectionString;
        }

        public string RevealWeatherHistoryApiKey()
        {
            return _userSecrets.ApiKeys.WeatherHistoryViewer;
        }

        public string RevealWeatherStackApiKey()
        {
            return _userSecrets.ApiKeys.WeatherStack;
        }
    }
}