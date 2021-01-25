using System;
using Microsoft.Extensions.Options;
using WeatherHistoryViewer.Core.Models;

namespace WeatherHistoryViewer.Services
{
    public interface ISecretRevealer
    {
        public UserSecrets RevealUserSecrets();
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

        public UserSecrets RevealUserSecrets()
        {
            //I can now use my mapped secrets below.
            return _userSecrets;
        }
    }
}