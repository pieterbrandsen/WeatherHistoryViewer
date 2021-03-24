using System;
using System.ComponentModel.DataAnnotations;

namespace WeatherHistoryViewer.Core.Models.Weather
{
    public class WeatherOverview
    {
        public string Year { get; set; }
        public string LocationName { get; set; }
        public double MaxTemp { get; set; }
        public string DateOfMaxTemp { get; set; }
        public double MinTemp { get; set; }
        public string DateOfMinTemp { get; set; }
        public double AvgTemp { get; set; }
        public double AvgSunHours { get; set; }
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
}