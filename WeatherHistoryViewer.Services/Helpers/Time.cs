using System.Linq;
using WeatherHistoryViewer.Core.Models.DataWarehouse;
using WeatherHistoryViewer.Db;

namespace WeatherHistoryViewer.Services.Helpers
{
    public class TimeHelper
    {
        public Time GetTime(string date, Time knownTime)
        {
            using var context = new ApplicationDbContext();
            var foundLocation = context.Times.FirstOrDefault(l => l.Date == date);

            return foundLocation ?? knownTime;
        }
    }
}