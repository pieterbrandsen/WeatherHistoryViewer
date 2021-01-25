namespace WeatherHistoryViewer.Core.Models
{
    public class ApiKeys
    {
        public string WeatherStack { get; set; }
    }
    public class UserSecrets
    {
        public ApiKeys ApiKeys { get; set; }
        public string DefaultConnectionString { get; set; }
    }

}