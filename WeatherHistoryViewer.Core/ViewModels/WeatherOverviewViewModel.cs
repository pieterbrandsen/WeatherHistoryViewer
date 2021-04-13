using System.Collections.Generic;
using WeatherHistoryViewer.Core.Models;

namespace WeatherHistoryViewer.Core.ViewModels
{
    public sealed class WeatherOverviewViewModel : WeatherViewModel
    {
        public List<WeatherOverview> WeatherOverviews { get; set; }
    }
}