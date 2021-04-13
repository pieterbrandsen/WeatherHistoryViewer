namespace WeatherHistoryViewer.Core.Models.DataWarehouse
{
    public enum MinCachingDaysBeforeUpdatingWeatherDb
    {
        OverviewPage = 60,
        YearsPage = 30,
        WeekPage = 7,
        DayPage = 1
    }

    public enum PossibleLegendValues
    {
        AvgTemp,
        MaxTemp,
        MinTemp,
        SunHour
    }

    public class WeatherMeasurement
    {
        public int Id { get; set; }
        public double AvgTemp { get; set; }
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
        public double SunHour { get; set; }
    }

    public class WeatherWarehouse
    {
        public int Id { get; set; }
        public LocationWarehouse Location { get; set; }
        public Time Time { get; set; }
        public WeatherMeasurement WeatherMeasurement { get; set; }
    }
}