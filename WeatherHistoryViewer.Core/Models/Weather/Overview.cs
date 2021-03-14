namespace WeatherHistoryViewer.Core.Models.Weather
{
    public class WeatherOverview
    {
        public string LocationName { get; set; }
        public double MaxTemp { get; set; }
        public string DateOfMaxTemp { get; set; }
        public double MinTemp { get; set; }
        public string DateOfMinTemp { get; set; }
        public double AverageTemp { get; set; }
        public double AverageSunHours { get; set; }
    }
}