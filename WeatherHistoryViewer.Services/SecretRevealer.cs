using System;
using Microsoft.Extensions.Options;
using WeatherHistoryViewer.Core.Models;

namespace WeatherHistoryViewer.Services
{
    public interface ISecretRevealer
    {
        public ApiKeys RevealSecretApiKeys();
        public ConnectionStringKeys RevealSecretConnectionStringKeys();
    }

    public class SecretRevealer : ISecretRevealer
    {
        private readonly ApiKeys _apiSecrets;
        private readonly ConnectionStringKeys _connectionStringSecrets;

        // I’ve injected <em>secrets</em> into the constructor as setup in Program.cs
        public SecretRevealer(IOptions<ApiKeys> apiKeys, IOptions<ConnectionStringKeys> connectionStringKeys)
        {
            // We want to know if secrets is null so we throw an exception if it is
            _apiSecrets = apiKeys.Value ?? throw new ArgumentNullException(nameof(apiKeys));
            _connectionStringSecrets = connectionStringKeys.Value ?? throw new ArgumentNullException(nameof(connectionStringKeys));
        }

        public ApiKeys RevealSecretApiKeys()
        {
            //I can now use my mapped secrets below.
            return _apiSecrets;
        }
        public ConnectionStringKeys RevealSecretConnectionStringKeys()
        {
            //I can now use my mapped secrets below.
            return _connectionStringSecrets;
        }
    }
}