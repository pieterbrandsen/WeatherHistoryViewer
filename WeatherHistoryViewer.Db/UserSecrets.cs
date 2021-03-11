using Microsoft.Extensions.Configuration;

namespace WeatherHistoryViewer.Db
{
    public static class UserSecrets
    {
        public static readonly string ConnectionString;
        public static readonly string WeatherHistoryApiKey;
        public static readonly string WeatherStackApiKey;
    }

    public class RevealUserSecrets
    {
        private readonly IConfiguration _configuration;

        public RevealUserSecrets(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ConnectionString()
        {
            return _configuration["UserSecrets:DefaultConnectionString"];
        }

        public string WeatherHistoryApiKey()
        {
            return _configuration["UserSecrets:ApiKeys:WeatherHistoryViewer"];
        }

        public string WeatherStackApiKey()
        {
            return _configuration["UserSecrets:ApiKeys:WeatherStack"];
        }
    }
}