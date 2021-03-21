using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHistoryViewer.Core.Models.Weather;

namespace WeatherHistoryViewer.Services.Helpers
{
    public class ColorScalerHelper
    {
        public void Scale3Way(string scaleProp, List<WeatherOverview> overviews = null, List<HistoricalWeather> historicalWeathers = null)
        {
            if (overviews != null)
            {
                var a = overviews.Select(s => s.GetType().Name == scaleProp).ToList();
            }
            else if (historicalWeathers != null)
            {

            }
        }
    }
}
