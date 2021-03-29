using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherHistoryViewer.Core.Models.DataWarehouse
{
    public class UpdateTime
    {
        public string Name { get; set; }
        public string Date { get; set; }
    }
    public class CssClasses
    {
        public string BackgroundColor { get; set; }
    }
    public class Time
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public DateTime DateTime => DateTime.Parse(Date);
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
