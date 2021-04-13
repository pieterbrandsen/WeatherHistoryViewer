using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherHistoryViewer.Core.Models
{
    public class CssClass
    {
        public string AvgTemp { get; set; }
        public string MinTemp { get; set; }
        public string MaxTemp { get; set; }
        public string SunHour { get; set; }
    }

    public class WeatherOverview
    {
        public WeatherOverview()
        {
            CssClass = new CssClass();
        }

        public string Year { get; set; }
        public string LocationName { get; set; }
        public double MaxTemp { get; set; }
        public string DateOfMaxTemp { get; set; }
        public double MinTemp { get; set; }
        public string DateOfMinTemp { get; set; }
        public double AvgTemp { get; set; }
        public double SunHour { get; set; }

        [NotMapped] public CssClass CssClass { get; set; }
    }

    public class WeatherData
    {
        [Required] public string Location { get; set; }

        [Required] public DateTime Date { get; set; }
        public string DateString => Date.ToString("yyyy/MM/dd");
    }

    public class YearData
    {
        [Required] public string Location { get; set; }
    }

    public class AddWeatherData
    {
        [Required] public string Location { get; set; }

        [Required] public DateTime OldestDate { get; set; }
        public string OldestDateString => OldestDate.ToString("yyyy/MM/dd");
        [Required] public DateTime NewestDate { get; set; }
        public string NewestDateString => NewestDate.ToString("yyyy/MM/dd");
    }

    public class LegendValues
    {
        public double AvgTemp { get; set; }
        public double MinTemp { get; set; }
        public double MaxTemp { get; set; }
        public double SunHour { get; set; }
    }

    public class WeatherLegend
    {
        public WeatherLegend()
        {
            Max = new LegendValues();
            Avg = new LegendValues();
            Min = new LegendValues();
        }

        public LegendValues Max { get; set; }
        public LegendValues Avg { get; set; }
        public LegendValues Min { get; set; }
    }
}