using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherHistoryViewer.Core.Models
{
    public enum DisplayedPropertys
    {
        AvgTemp,
        MaxTemp,
        MinTemp,
        SunHour
    }
    public class CssBackgroundClass
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
            CssBackgroundClass = new CssBackgroundClass();
        }
        public string Year { get; set; }
        public string LocationName { get; set; }
        public double MaxTemp { get; set; }
        public string DateOfMaxTemp { get; set; }
        public double MinTemp { get; set; }
        public string DateOfMinTemp { get; set; }
        public double AvgTemp { get; set; }
        public double SunHour { get; set; }
        [NotMapped]
        public CssBackgroundClass CssBackgroundClass { get; set; }
    }

    public class FormResponse
    {
        [Required]
        public string Location { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
    public class YearForm
    {
        [Required]
        public string Location { get; set; }
    }

    public class AddWeatherDataForm
    {
        [Required]
        public string Location { get; set; }

        [Required]
        public DateTime OldestDate { get; set; }

        [Required]
        public DateTime NewestDate { get; set; }
    }

    public class LegendaValues
    {
        public double AvgTemp { get; set; }
        public double MinTemp { get; set; }
        public double MaxTemp { get; set; }
        public double SunHour { get; set; }
    }

    public class WeatherLegenda {
        public WeatherLegenda()
        {
            Max = new LegendaValues();
            Avg = new LegendaValues();
            Min = new LegendaValues();
        }
        public LegendaValues Max { get; set; }
        public LegendaValues Avg { get; set; }
        public LegendaValues Min { get; set; }
    }
}