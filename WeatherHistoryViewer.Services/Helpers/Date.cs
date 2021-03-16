using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherHistoryViewer.Services.Helper
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

            return new List<string>();
        }

        private DateTime GetDateOfYesterday(DateTime currentDate)
        {
            return currentDate.AddDays(-1);
        }

        private DateTime GetDateOfTommorow(DateTime currentDate)
        {
            return currentDate.AddDays(1);
        }

        private string ConvertStringToDateFormat(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        public DateTime ConvertDateStringToDate(string date)
        {
            // Does this work?
            // DateTime.Parse(date);
            var splitDate = date.Split("-");
            return new DateTime(Convert.ToInt16(splitDate[0]), Convert.ToInt16(splitDate[1]),
                Convert.ToInt16(splitDate[2]));
        }

        public List<string> GetWeekDatesFromDate(string date)
        {
            var currDate = ConvertDateStringToDate(date);
            var dayOfWeek = (int) currDate.DayOfWeek;
            dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;
            var startOfWeek = currDate.AddDays(1 - (int) currDate.DayOfWeek);
            var dateTimeList = new List<DateTime> {startOfWeek};
            var i = startOfWeek;
            var j = startOfWeek;
            while ((int) i.DayOfWeek > 1)
            {
                var dateOfYesterday = GetDateOfYesterday(i);
                i = dateOfYesterday;
                dateTimeList.Add(i);
            }

            while ((int) j.DayOfWeek < 6)
            {
                var dateOfTomorrow = GetDateOfTommorow(j);
                j = dateOfTomorrow;
                dateTimeList.Add(j);

                if (j.DayOfWeek == DayOfWeek.Saturday) dateTimeList.Add(GetDateOfTommorow(j));
            }

            var dateList = new List<string>();
            dateTimeList.OrderByDescending(d => d.Ticks).ToList()
                .ForEach(d => dateList.Add(ConvertStringToDateFormat(d.Date)));
            return dateList;
        }
    }
}