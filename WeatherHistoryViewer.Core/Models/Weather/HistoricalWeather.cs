using System.Collections.Generic;

namespace WeatherHistoryViewer.Core.Models.Weather
{
    public enum HourlyInterval
    {
        Hours1 = 1,
        Hours3 = 3,
        Hours6 = 6,
        Hours12 = 12,
        Hours24 = 24
    }

    public class HistoricalWeather
    {
        public int Id { get; set; }
        public Location Location { get; set; }

        public string Date { get; set; }
        public int DateEpoch { get; set; }
        public int MinTemp { get; set; }
        public int MaxTemp { get; set; }
        public int AvgTemp { get; set; }
        public double TotalSnow { get; set; }
        public double SunHour { get; set; }
        public int UvIndex { get; set; }
    }
}