using System;
using System.Collections.Generic;

namespace WeatherHistoryViewer.Services.Handlers
{
    public class DateHelper
    {
        private const string OldestDate = "2008-07-01";

        public List<string> GetAllRequestableDates()
        {
            var dateList = new List<string>();
            var oldestDate = ConvertDateStringToDate(OldestDate);
            var i = DateTime.Today;
            while (oldestDate.Ticks < i.Ticks)
            {
                i = GetDateOfYesterday(i);
                dateList.Add(ConvertStringToDateFormat(i));
            }

            return dateList;
        }

        public List<string> GetRangeOfRequestableDates(string oldestDateString, string newestDateString = null)
        {
            var dateList = new List<string>();
            var oldestDate = ConvertDateStringToDate(oldestDateString);
            var newestDate = newestDateString == null ? DateTime.Today : ConvertDateStringToDate(newestDateString);

            dateList.Add(ConvertStringToDateFormat(newestDate));
            var i = newestDate;
            while (oldestDate.Ticks < i.Ticks)
            {
                i = GetDateOfYesterday(i);
                dateList.Add(ConvertStringToDateFormat(i));
            }

            return dateList;
        }

        public string GetDateStringOfDaysAgo(int dayAmount = 7)
        {
            var date = DateTime.Today;
            for (var i = 1; i <= dayAmount; i++) date = GetDateOfYesterday(date);

            return ConvertStringToDateFormat(date);
        }

        public List<string> GetDateInLast15Y(string shortDate)
        {
            shortDate = shortDate.Replace("/", "-");
            var shortDateSplitted = shortDate.Split("-");
            if (shortDateSplitted.Length == 2)
            {
            for (var i = 0; i < shortDateSplitted.Length; i++)
            {
                var value = shortDateSplitted[i];
                if (value.Length == 1) shortDateSplitted[i] = $"0{value}";
            }
            shortDate = $"{shortDateSplitted[0]}-{shortDateSplitted[1]}";


            try
            {
                var dates = new List<string>();
                var year = DateTime.Today.Year;
                for (var i = 0; i < 15; i++)
                {
                    var date = string.Format("{0:yyyy/MM/dd}", $"{year}-{shortDate}");
                    dates.Add(date);
                    year -= 1;
                }

                return dates;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            }
            else
            {
                return new List<string>();
            }
        }

        private DateTime GetDateOfYesterday(DateTime currentDate)
        {
            return currentDate.AddDays(-1);
        }

        private string ConvertStringToDateFormat(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        private DateTime ConvertDateStringToDate(string date)
        {
            // Does this work?
            // DateTime.Parse(date);
            var splitDate = date.Split("-");
            return new DateTime(Convert.ToInt16(splitDate[0]), Convert.ToInt16(splitDate[1]),
                Convert.ToInt16(splitDate[2]));
        }
    }
}