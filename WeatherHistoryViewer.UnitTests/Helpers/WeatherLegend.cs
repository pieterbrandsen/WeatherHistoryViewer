using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHistoryViewer.Core.Constants;
using WeatherHistoryViewer.Core.Models;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Services.Helpers;
using Xunit;

namespace WeatherHistoryViewer.UnitTests.Helpers
{
    public class WeatherLegend
    {
        private readonly LegendHelper _legendHelper = new();

        private static List<WeatherOverview> WeatherOverviewListGenerator(double length)
        {
            var weatherOverviewList = new List<WeatherOverview>();

            for (int i = 1; i <= length; i++)
            {
                weatherOverviewList.Add(new()
                {
                    SunHour = i,
                    AvgTemp = i,
                    MaxTemp = i,
                    MinTemp = i,
                });
            }

            weatherOverviewList = weatherOverviewList.OrderBy(a => Guid.NewGuid()).ToList();
            return weatherOverviewList;
        }

        private static List<HistoricalWeather> HistoricalWeatherListGenerator(double length)
        {
            var weatherOverviewList = new List<HistoricalWeather>();

            for (int i = 1; i <= length; i++)
            {
                weatherOverviewList.Add(new()
                {
                    SunHour = i,
                    AvgTemp = i,
                    MaxTemp = i,
                    MinTemp = i,
                });
            }

            weatherOverviewList = weatherOverviewList.OrderBy(a => Guid.NewGuid()).ToList();
            return weatherOverviewList;
        }

        public static IEnumerable<object[]> GetWeatherData()
        {
            const double length = 100;
            yield return new object[] {WeatherOverviewListGenerator(length)};
            yield return new object[] {null, HistoricalWeatherListGenerator(length)};

        }

        [Theory]
        [MemberData(nameof(GetWeatherData))]
        public void GetWeatherLegendShouldReturnNonNullValues(List<WeatherOverview> weatherOverviewList = null,
            List<HistoricalWeather> historicalWeatherList = null)
        {
            var length = 100;
            var weatherLegend = weatherOverviewList != null
                ? _legendHelper.GetWeatherLegend(weatherOverviewList)
                : _legendHelper.GetWeatherLegend(historicalWeatherList);
            Assert.Equal(length, weatherLegend.Max.MaxTemp);
            Assert.Equal(length, weatherLegend.Max.AvgTemp);
            Assert.Equal(length, weatherLegend.Max.MinTemp);
            Assert.Equal(length, weatherLegend.Max.SunHour);

            Assert.Equal(length * 0.505, weatherLegend.Avg.MaxTemp);
            Assert.Equal(length * 0.505, weatherLegend.Avg.AvgTemp);
            Assert.Equal(length * 0.505, weatherLegend.Avg.MinTemp);
            Assert.Equal(length * 0.505, weatherLegend.Avg.SunHour);

            Assert.Equal(1, weatherLegend.Min.MaxTemp);
            Assert.Equal(1, weatherLegend.Min.AvgTemp);
            Assert.Equal(1, weatherLegend.Min.MinTemp);
            Assert.Equal(1, weatherLegend.Min.SunHour);
        }

        [Theory]
        [MemberData(nameof(GetWeatherData))]
        public void GetWeatherListWithLegendClassesShouldListWithCssClasses(
            List<WeatherOverview> weatherOverviewList = null, List<HistoricalWeather> historicalWeatherList = null)
        {
            var weatherLegend = weatherOverviewList != null
                ? _legendHelper.GetWeatherLegend(weatherOverviewList)
                : _legendHelper.GetWeatherLegend(historicalWeatherList);

            if (weatherOverviewList != null)
            {
                var filledWeatherList =
                    _legendHelper.GetWeatherListWithLegendClasses(weatherOverviewList, weatherLegend);
                foreach (var item in filledWeatherList)
                {
                    Assert.NotNull(item.CssClass.GetType().GetProperty(WeatherConstants.NameOfLegendValue.ToString())?.GetValue(item.CssClass));
                }

                var filledForceAssignedWeatherList =
                    _legendHelper.GetWeatherListWithLegendClasses(weatherOverviewList, weatherLegend, true);
                foreach (var item in filledForceAssignedWeatherList)
                {
                    Assert.NotNull(item.CssClass.MaxTemp);
                    Assert.NotNull(item.CssClass.AvgTemp);
                    Assert.NotNull(item.CssClass.MinTemp);
                    Assert.NotNull(item.CssClass.SunHour);
                }

            }
            else
            {
                var filledWeatherList =
                    _legendHelper.GetWeatherListWithLegendClasses(historicalWeatherList, weatherLegend);
                foreach (var item in filledWeatherList)
                {
                    Assert.NotNull(item.CssClass.GetType().GetProperty(WeatherConstants.NameOfLegendValue.ToString())?.GetValue(item.CssClass));
                }

                var filledForceAssignedWeatherList =
                    _legendHelper.GetWeatherListWithLegendClasses(historicalWeatherList, weatherLegend, true);
                foreach (var item in filledForceAssignedWeatherList)
                {
                    Assert.NotNull(item.CssClass.MaxTemp);
                    Assert.NotNull(item.CssClass.AvgTemp);
                    Assert.NotNull(item.CssClass.MinTemp);
                    Assert.NotNull(item.CssClass.SunHour);
                }
            }
        }
    }
}
