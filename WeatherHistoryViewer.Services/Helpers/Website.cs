using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHistoryViewer.Config;
using WeatherHistoryViewer.Core.Models.Weather;

namespace WeatherHistoryViewer.Services.Helpers
{
    public class WebsiteHelper
    {
        private WeatherLegenda GetLegendaValues(WeatherLegenda legenda, List<double> weatherValues, string propertyName)
        {
            legenda.Max.GetType().GetProperty(propertyName).SetValue(legenda.Max,
    Math.Round(weatherValues.Max(),2));
            legenda.Avg.GetType().GetProperty(propertyName).SetValue(legenda.Avg,
                Math.Round(weatherValues.Average(),2));
            legenda.Min.GetType().GetProperty(propertyName).SetValue(legenda.Min,
    Math.Round(weatherValues.Min(),2));
            
            return legenda;
        }
        public WeatherLegenda GetWeatherLegenda(List<WeatherOverview> weatherOverviews)
        {
            var weatherLegenda = new WeatherLegenda();
            
            var maxTemp = weatherOverviews.Select(w => w.MaxTemp).ToList();
            weatherLegenda = GetLegendaValues(weatherLegenda, maxTemp, "MaxTemp");
            var avgTemp = weatherOverviews.Select(w => w.AvgTemp).ToList();
            weatherLegenda = GetLegendaValues(weatherLegenda, avgTemp, "AvgTemp");
            var minTemp = weatherOverviews.Select(w => w.MinTemp).ToList();
            weatherLegenda = GetLegendaValues(weatherLegenda, minTemp, "MinTemp");
            var sunHour = weatherOverviews.Select(w => w.SunHour).ToList();
            weatherLegenda = GetLegendaValues(weatherLegenda, sunHour, "SunHour");
            return weatherLegenda;
        }
        public WeatherLegenda GetWeatherLegenda(List<HistoricalWeather> historicalWeathers)
        {
            var weatherLegenda = new WeatherLegenda();

            var maxTemp = historicalWeathers.Select(w => w.MaxTemp).ToList();
            weatherLegenda = GetLegendaValues(weatherLegenda, maxTemp, "MaxTemp");
            var avgTemp = historicalWeathers.Select(w => w.AvgTemp).ToList();
            weatherLegenda = GetLegendaValues(weatherLegenda, avgTemp, "AvgTemp");
            var minTemp = historicalWeathers.Select(w => w.MinTemp).ToList();
            weatherLegenda = GetLegendaValues(weatherLegenda, minTemp, "MinTemp");
            var sunHour = historicalWeathers.Select(w => w.SunHour).ToList();
            weatherLegenda = GetLegendaValues(weatherLegenda, sunHour, "SunHour");
            return weatherLegenda;
        }
        private string GetCssLegendaClass(double currValue, double maxValue, double minValue)
        {
            return $"legendaColor{Math.Round((currValue - minValue) / (maxValue - minValue) * 10,0)}";
        }
        public List<WeatherOverview> GetWeatherCssLegendaClasses(List<WeatherOverview> weatherOverviews, WeatherLegenda legenda, bool forceAssigningClasses = false)
        {
            foreach (var item in weatherOverviews)
            {
                switch (WebConfig.NameOfLegendaValue)
                {
                    case "MaxTemp":
                item.CssBackgroundClass.MaxTemp = GetCssLegendaClass(item.MaxTemp, legenda.Max.MaxTemp, legenda.Min.MaxTemp);
                        break;
                    case "AvgTemp":
                item.CssBackgroundClass.AvgTemp = GetCssLegendaClass(item.AvgTemp, legenda.Max.AvgTemp, legenda.Min.AvgTemp);
                        break;
                    case "MinTemp":
                item.CssBackgroundClass.MinTemp = GetCssLegendaClass(item.MinTemp, legenda.Max.MinTemp, legenda.Min.MinTemp);
                        break;
                    case "SunHour":
                item.CssBackgroundClass.SunHour = GetCssLegendaClass(item.SunHour, legenda.Max.SunHour, legenda.Min.SunHour);
                        break;
                    default:
                        break;
                }

                if (forceAssigningClasses)
                {
                item.CssBackgroundClass.MaxTemp = GetCssLegendaClass(item.MaxTemp, legenda.Max.MaxTemp, legenda.Min.MaxTemp);
                    item.CssBackgroundClass.AvgTemp = GetCssLegendaClass(item.AvgTemp, legenda.Max.AvgTemp, legenda.Min.AvgTemp);
                item.CssBackgroundClass.MinTemp = GetCssLegendaClass(item.MinTemp, legenda.Max.MinTemp, legenda.Min.MinTemp);
                    item.CssBackgroundClass.SunHour = GetCssLegendaClass(item.SunHour, legenda.Max.SunHour, legenda.Min.SunHour);
                }
            }
            return weatherOverviews;
        }

        public List<HistoricalWeather> GetWeatherCssLegendaClasses(List<HistoricalWeather> historicalWeathers, WeatherLegenda legenda, bool forceAssigningClasses = false)
        {
            foreach (var item in historicalWeathers)
            {
                switch (WebConfig.NameOfLegendaValue)
                {
                    case "MaxTemp":
                        item.CssBackgroundClass.MaxTemp = GetCssLegendaClass(item.MaxTemp, legenda.Max.MaxTemp, legenda.Min.MaxTemp);
                        break;
                    case "AvgTemp":
                        item.CssBackgroundClass.AvgTemp = GetCssLegendaClass(item.AvgTemp, legenda.Max.AvgTemp, legenda.Min.AvgTemp);
                        break;
                    case "MinTemp":
                        item.CssBackgroundClass.MinTemp = GetCssLegendaClass(item.MinTemp, legenda.Max.MinTemp, legenda.Min.MinTemp);
                        break;
                    case "SunHour":
                        item.CssBackgroundClass.SunHour = GetCssLegendaClass(item.SunHour, legenda.Max.SunHour, legenda.Min.SunHour);
                        break;
                    default:
                        break;
                }

                if (forceAssigningClasses)
                {
                    item.CssBackgroundClass.MaxTemp = GetCssLegendaClass(item.MaxTemp, legenda.Max.MaxTemp, legenda.Min.MaxTemp);
                    item.CssBackgroundClass.AvgTemp = GetCssLegendaClass(item.AvgTemp, legenda.Max.AvgTemp, legenda.Min.AvgTemp);
                    item.CssBackgroundClass.MinTemp = GetCssLegendaClass(item.MinTemp, legenda.Max.MinTemp, legenda.Min.MinTemp);
                    item.CssBackgroundClass.SunHour = GetCssLegendaClass(item.SunHour, legenda.Max.SunHour, legenda.Min.SunHour);
                }
            }
            return historicalWeathers;
        }
    }
}
