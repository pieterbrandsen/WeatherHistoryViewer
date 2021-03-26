using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHistoryViewer.Core.Models.Weather;

namespace WeatherHistoryViewer.Core.ViewModels
{
    public sealed class WeatherOverviewViewModel : WeatherViewModel
    {
        public List<WeatherOverview> WeatherOverviews { get; set; }
    }
}
