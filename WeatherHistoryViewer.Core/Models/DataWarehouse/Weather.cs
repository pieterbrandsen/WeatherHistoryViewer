using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherHistoryViewer.Core.Models.DataWarehouse
{
    public enum MinDaysBeforeUpdatingWeather
    {
        Overview = 60,
        Years = 30,
        Week = 7,
        Day = 1,
    }
    public class WeatherMeasurment
    {
        public int Id { get; set; }
        public double AvgTemp { get; set; }
        public double MaxTemp { get; set; }
        [NotMapped]
        public Time TimeOfHighestMaxTemp { get; set; }
        public double MinTemp { get; set; }
        [NotMapped]
        public Time TimeOfLowestMinTemp { get; set; }
        public double SunHour { get; set; }
    }
    public class WeatherWarehouse {
        public int Id { get; set; }
    public LocationWarehouse Location { get; set; }
        public Time Time { get; set; }
    public WeatherMeasurment WeatherMeasurment { get; set; }
    [NotMapped]
        public CssClasses CssClasses { get; set; }
}
}
