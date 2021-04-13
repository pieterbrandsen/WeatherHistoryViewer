using WeatherHistoryViewer.Db;
using Xunit;

namespace WeatherHistoryViewer.UnitTests.Db
{
    public class UserSecretsTests
    {
        private readonly string FakeConnectionString = "abc";
        private readonly string FakeWeatherHistoryViewerApiKey = "abc";
        private readonly string FakeWeatherStackApiKey = "abc";

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