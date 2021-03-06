﻿using System.Collections.Generic;
using WeatherHistoryViewer.Core.Models;
using WeatherHistoryViewer.Core.Models.Weather;

namespace WeatherHistoryViewer.Core.ViewModels
{
    public abstract class WeatherViewModel
    {
        public WeatherLegend WeatherLegend { get; set; }
    }

    public sealed class WeatherOfYearsViewModel : WeatherViewModel
    {
        public List<WeatherOverview> WeatherOverviews { get; set; }
    }

    public sealed class WeatherOfWeekViewModel : WeatherViewModel
    {
        public List<List<HistoricalWeather>> HistoricalWeather { get; set; }
        public List<HistoricalWeather> AverageHistoricalWeatherEachWeek { get; set; }
    }

    public sealed class WeatherOfDayViewModel : WeatherViewModel
    {
        public List<HistoricalWeather> HistoricalWeather { get; set; }
    }
}