using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHistoryViewer.Core;

namespace WeatherHistoryViewer.Services
{
    public interface ISecretRevealer
    {
        public SecretKeys RevealSecretKeys();
    }
    public class SecretRevealer : ISecretRevealer
    {
        private readonly SecretKeys _secrets;
        // I’ve injected <em>secrets</em> into the constructor as setup in Program.cs
        public SecretRevealer(IOptions<SecretKeys> secrets)
        {
            // We want to know if secrets is null so we throw an exception if it is
            _secrets = secrets.Value ?? throw new ArgumentNullException(nameof(secrets));
        }

        public SecretKeys RevealSecretKeys()
        {
            //I can now use my mapped secrets below.
            return _secrets;
        }
    }
}
