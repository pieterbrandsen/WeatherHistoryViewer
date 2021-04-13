using System;
using System.Collections.Generic;
using System.Linq;
using WeatherHistoryViewer.Core.Constants;
using WeatherHistoryViewer.Core.Models;
using WeatherHistoryViewer.Core.Models.DataWarehouse;
using WeatherHistoryViewer.Core.Models.Weather;

namespace WeatherHistoryViewer.Services.Helpers
{
    public class LegendHelper
    {
        private WeatherLegend GetLegendValues(WeatherLegend legend, List<double> weatherValues, string propertyName)
        {
            legend.Max.GetType().GetProperty(propertyName)?.SetValue(legend.Max,
                Math.Round(weatherValues.Max(), 2));
            legend.Avg.GetType().GetProperty(propertyName)?.SetValue(legend.Avg,
                Math.Round(weatherValues.Average(), 2));
            legend.Min.GetType().GetProperty(propertyName)?.SetValue(legend.Min,
                Math.Round(weatherValues.Min(), 2));

            return legend;
        }

        public WeatherLegend GetWeatherLegend(List<WeatherOverview> weatherOverviews)
        {
            var weatherLegend = new WeatherLegend();

            var maxTemp = weatherOverviews.Select(w => w.MaxTemp).ToList();
            weatherLegend = GetLegendValues(weatherLegend, maxTemp, "MaxTemp");
            var avgTemp = weatherOverviews.Select(w => w.AvgTemp).ToList();
            weatherLegend = GetLegendValues(weatherLegend, avgTemp, "AvgTemp");
            var minTemp = weatherOverviews.Select(w => w.MinTemp).ToList();
            weatherLegend = GetLegendValues(weatherLegend, minTemp, "MinTemp");
            var sunHour = weatherOverviews.Select(w => w.SunHour).ToList();
            weatherLegend = GetLegendValues(weatherLegend, sunHour, "SunHour");
            return weatherLegend;
        }

        public WeatherLegend GetWeatherLegend(List<HistoricalWeather> historicalWeather)
        {
            var weatherLegend = new WeatherLegend();

            var maxTemp = historicalWeather.Select(w => w.MaxTemp).ToList();
            weatherLegend = GetLegendValues(weatherLegend, maxTemp, "MaxTemp");
            var avgTemp = historicalWeather.Select(w => w.AvgTemp).ToList();
            weatherLegend = GetLegendValues(weatherLegend, avgTemp, "AvgTemp");
            var minTemp = historicalWeather.Select(w => w.MinTemp).ToList();
            weatherLegend = GetLegendValues(weatherLegend, minTemp, "MinTemp");
            var sunHour = historicalWeather.Select(w => w.SunHour).ToList();
            weatherLegend = GetLegendValues(weatherLegend, sunHour, "SunHour");
            return weatherLegend;
        }

        private static double GetCssLegendClassValue(double currentValue, double maxValue, double minValue)
        {
            return Math.Round((currentValue - minValue) / (maxValue - minValue) * 10, 0);
        }

        public List<WeatherOverview> GetWeatherListWithLegendClasses(List<WeatherOverview> weatherOverviews,
            WeatherLegend legend, bool forceAssigningClasses = false)
        {
            foreach (var item in weatherOverviews)
            {
                if (forceAssigningClasses)
                {
                    item.CssClass.MaxTemp =
                        $"legendColor{GetCssLegendClassValue(item.MaxTemp, legend.Max.MaxTemp, legend.Min.MaxTemp)}";
                    item.CssClass.AvgTemp =
                        $"legendColor{GetCssLegendClassValue(item.AvgTemp, legend.Max.AvgTemp, legend.Min.AvgTemp)}";
                    item.CssClass.MinTemp =
                        $"legendColor{GetCssLegendClassValue(item.MinTemp, legend.Max.MinTemp, legend.Min.MinTemp)}";
                    item.CssClass.SunHour =
                        $"legendColor{GetCssLegendClassValue(item.SunHour, legend.Max.SunHour, legend.Min.SunHour)}";
                    continue;
                }

                switch (WeatherConstants.NameOfLegendValue)
                {
                    case PossibleLegendValues.MaxTemp:
                        item.CssClass.MaxTemp =
                            $"legendColor{GetCssLegendClassValue(item.MaxTemp, legend.Max.MaxTemp, legend.Min.MaxTemp)}";
                        break;
                    case PossibleLegendValues.AvgTemp:
                        item.CssClass.AvgTemp =
                            $"legendColor{GetCssLegendClassValue(item.AvgTemp, legend.Max.AvgTemp, legend.Min.AvgTemp)}";
                        break;
                    case PossibleLegendValues.MinTemp:
                        item.CssClass.MinTemp =
                            $"legendColor{GetCssLegendClassValue(item.MinTemp, legend.Max.MinTemp, legend.Min.MinTemp)}";
                        break;
                    case PossibleLegendValues.SunHour:
                        item.CssClass.SunHour =
                            $"legendColor{GetCssLegendClassValue(item.SunHour, legend.Max.SunHour, legend.Min.SunHour)}";
                        break;
                }
            }

            return weatherOverviews;
        }

        public List<HistoricalWeather> GetWeatherListWithLegendClasses(List<HistoricalWeather> historicalWeather,
            WeatherLegend legend, bool forceAssigningClasses = false)
        {
            foreach (var item in historicalWeather)
            {
                if (forceAssigningClasses)
                {
                    item.CssClass.MaxTemp =
                        $"legendColor{GetCssLegendClassValue(item.MaxTemp, legend.Max.MaxTemp, legend.Min.MaxTemp)}";
                    item.CssClass.AvgTemp =
                        $"legendColor{GetCssLegendClassValue(item.AvgTemp, legend.Max.AvgTemp, legend.Min.AvgTemp)}";
                    item.CssClass.MinTemp =
                        $"legendColor{GetCssLegendClassValue(item.MinTemp, legend.Max.MinTemp, legend.Min.MinTemp)}";
                    item.CssClass.SunHour =
                        $"legendColor{GetCssLegendClassValue(item.SunHour, legend.Max.SunHour, legend.Min.SunHour)}";
                    continue;
                }

                switch (WeatherConstants.NameOfLegendValue)
                {
                    case PossibleLegendValues.MaxTemp:
                        item.CssClass.MaxTemp =
                            $"legendColor{GetCssLegendClassValue(item.MaxTemp, legend.Max.MaxTemp, legend.Min.MaxTemp)}";
                        break;
                    case PossibleLegendValues.AvgTemp:
                        item.CssClass.AvgTemp =
                            $"legendColor{GetCssLegendClassValue(item.AvgTemp, legend.Max.AvgTemp, legend.Min.AvgTemp)}";
                        break;
                    case PossibleLegendValues.MinTemp:
                        item.CssClass.MinTemp =
                            $"legendColor{GetCssLegendClassValue(item.MinTemp, legend.Max.MinTemp, legend.Min.MinTemp)}";
                        break;
                    case PossibleLegendValues.SunHour:
                        item.CssClass.SunHour =
                            $"legendColor{GetCssLegendClassValue(item.SunHour, legend.Max.SunHour, legend.Min.SunHour)}";
                        break;
                }
            }

            return historicalWeather;
        }
    }
}