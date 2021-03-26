namespace WeatherHistoryViewer.Config
{
    public class WebConfig
    {
        // * Weather Handler
        // Keep up to date with this amount of days 
        public static short OldestDaysAgo = 365;
        public static short NewestDaysAgo = 10;

        public static string NameOfLegendaValue = "AvgTemp";

    }
}
