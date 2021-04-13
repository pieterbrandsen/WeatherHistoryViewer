using WeatherHistoryViewer.Core.Models.DataWarehouse;

namespace WeatherHistoryViewer.Core.Constants
{
    public class WeatherConstants
    {
        public const short OldestDaysAgo = 10000;
        public const short NewestDaysAgo = 10;

        public const PossibleLegendValues NameOfLegendValue = PossibleLegendValues.AvgTemp;
        public const string OldestWeatherDatePossible = "2008/07/01";

        public const string DefaultLocationName = "Amsterdam";
    }
}