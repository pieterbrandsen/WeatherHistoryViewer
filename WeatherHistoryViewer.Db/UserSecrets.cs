using Microsoft.Extensions.Configuration;

namespace WeatherHistoryViewer.Db
{
    public static class UserSecrets
    {
        public static string ConnectionString;
        public static string WeatherHistoryApiKey;
        public static string WeatherStackApiKey;
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
            return "";
            return _configuration["UserSecrets:DefaultConnectionString"];
        }

        public string WeatherHistoryApiKey()
        {
            return "";
            return _configuration["UserSecrets:ApiKeys:WeatherHistoryViewer"];
        }

        public string WeatherStackApiKey()
        {
            return "";
            return _configuration["UserSecrets:ApiKeys:WeatherStack"];
        }
    }
}