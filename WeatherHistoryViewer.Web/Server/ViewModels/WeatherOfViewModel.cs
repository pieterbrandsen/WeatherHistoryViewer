﻿using System.Collections.Generic;
using WeatherHistoryViewer.Core.Models;
using WeatherHistoryViewer.Core.Models.Weather;

namespace WeatherHistoryViewer.Web.Server.ViewModels
{
    public abstract class WeatherViewModel
    {
        public WeatherLegend WeatherLegend { get; set; }
    }

    public sealed class WeatherOverviewViewModel : WeatherViewModel
    {
        public List<WeatherOverview> WeatherOverviews { get; set; }
    }

    public sealed class WeatherOfYearsViewModel : WeatherViewModel
    {
        public List<WeatherOverview> WeatherOverviews { get; set; }
    }

    public sealed class WeatherOfWeeksViewModel : WeatherViewModel
    {
        public List<List<HistoricalWeather>> HistoricalWeather { get; set; }
    }

    public sealed class WeatherOfDaysViewModel : WeatherViewModel
    {
        public List<HistoricalWeather> HistoricalWeather { get; set; }
    }
}