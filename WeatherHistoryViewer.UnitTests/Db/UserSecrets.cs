using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHistoryViewer.Db;
using Xunit;

namespace WeatherHistoryViewer.UnitTests.Db
{
    public class UserSecretsTests
    {
        private string FakeConnectionString = "abc";
        private string FakeWeatherHistoryViewerApiKey = "abc";
        private string FakeWeatherStackApiKey = "abc";
        [Fact]
        public void SetUserSecrets()
        {
            UserSecrets.ConnectionString = FakeConnectionString;
            UserSecrets.WeatherHistoryApiKey = FakeWeatherHistoryViewerApiKey;
            UserSecrets.WeatherStackApiKey = FakeWeatherStackApiKey;

            Assert.Equal(UserSecrets.ConnectionString, FakeConnectionString);
            Assert.Equal(UserSecrets.WeatherHistoryApiKey, FakeWeatherHistoryViewerApiKey);
            Assert.Equal(UserSecrets.WeatherStackApiKey, FakeWeatherStackApiKey);
        }
    }
}
