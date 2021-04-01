using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherHistoryViewer.Core.Models.Weather
{
    public class HistoricalWeather
    {
        public HistoricalWeather()
        {
            CssBackgroundClass = new CssBackgroundClass();
        }

        public int Id { get; set; }
        public Location Location { get; set; }

        public string Date { get; set; }
        public int DateEpoch { get; set; }
        public double MinTemp { get; set; }
        public double MaxTemp { get; set; }
        public double AvgTemp { get; set; }
        public double TotalSnow { get; set; }
        public double SunHour { get; set; }
        public int UvIndex { get; set; }

        [NotMapped] public CssBackgroundClass CssBackgroundClass { get; set; }
    }
}