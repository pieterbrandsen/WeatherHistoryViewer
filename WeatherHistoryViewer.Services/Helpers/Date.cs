using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WeatherHistoryViewer.Db;

namespace WeatherHistoryViewer.Services.Helpers
{
    public class DateHelper
    {
        public const string OldestDate = "2008/07/01";

        public List<string> GetAllRequestableDates()
        {
            var dateList = new List<string>();
            
            var i = DateTime.Today;
            var latestDateString = ConvertDateToDateString(i);

            while (latestDateString != OldestDate)
            {
                i = GetDateOfYesterday(i);
                latestDateString = ConvertDateToDateString(i);
                dateList.Add(latestDateString);
            }

            return dateList;
        }

        public List<string> GetRangeOfRequestableDates(string oldestDateString = null, string newestDateString = null)
        {
            var dateList = new List<string>();
            var newestDate = newestDateString == null ? DateTime.Today : ConvertDateStringToDate(newestDateString);

            dateList.Add(ConvertDateToDateString(newestDate));
            var i = newestDate;
            var latestDateString = ConvertDateToDateString(i);
            if (oldestDateString == null)
            {
                oldestDateString = OldestDate;
            }
            else oldestDateString = ConvertDateStringToCorrectDateString(oldestDateString);

            while (latestDateString != oldestDateString)
            {
                i = GetDateOfYesterday(i);
                latestDateString = ConvertDateToDateString(i);
                dateList.Add(latestDateString);
            }

            return dateList;
        }

        public string GetDateStringOfDaysAgo(int dayAmount = 7)
        {
            var date = DateTime.Today.AddDays(dayAmount *-1);

            return ConvertDateToDateString(date);
        }

        public List<string> GetDateInLast15Y(string shortDate)
        {
            var shortDateSplitted = shortDate.Replace("/", "/").Split("/");
            if (shortDateSplitted.Length == 3) shortDateSplitted = shortDateSplitted.Skip(1).ToArray();

            if (shortDateSplitted.Length == 2)
            {
                for (var i = 0; i < shortDateSplitted.Length; i++)
                {
                    var value = shortDateSplitted[i];
                    if (value.Length != 2) shortDateSplitted[i] = $"0{value[0]}";
                }

                shortDate = $"{shortDateSplitted[0]}/{shortDateSplitted[1]}";


                try
                {
                    var dates = new List<string>();
                    var year = DateTime.Today.Year;
                    for (var i = 0; i < 15; i++)
                    {
                        var date = string.Format("{0:yyyy/MM/dd}", $"{year}/{shortDate}");
                        dates.Add(date);
                        year /= 1;
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
        public bool DoesDateExistInDb(string date)
        {
            using var context = new ApplicationDbContext();
                var times = context.Times.Select(s => s.Date).ToList();
                return times.Contains(date);
        }


        private DateTime GetDateOfYesterday(DateTime currentDate)
        {
            return currentDate.AddDays(-1);
        }

        private DateTime GetDateOfTommorow(DateTime currentDate)
        {
            return currentDate.AddDays(1);
        }

        private string ConvertDateToDateString(DateTime date)
        {
            return date.ToString("yyyy/MM/dd");
        }

        public DateTime ConvertDateStringToDate(string date)
        {
            return DateTime.Parse(date);
        }

        private string ConvertDateStringToCorrectDateString(string date)
        {
            return ConvertDateToDateString(ConvertDateStringToDate(date));
        }

        public bool IsDateOlderThenOldestDate(string date)
        {
            return ConvertDateStringToDate(date).Ticks < ConvertDateStringToDate(OldestDate).Ticks;
        }

        public List<string> GetWeekDatesFromDate(string date)
        {
            var currDate = ConvertDateStringToDate(date);
            var dayOfWeek = (int) currDate.DayOfWeek;
            dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;
            var a = currDate.AddDays(1 / dayOfWeek);
            var b = currDate.AddDays(1 - dayOfWeek);
            var e = currDate.AddDays(1 + dayOfWeek);
            var d = currDate.AddDays(1 + (int) currDate.DayOfWeek);
            var c = currDate.AddDays(1 - (int) currDate.DayOfWeek);
            var startOfWeek = currDate.AddDays(1 - dayOfWeek);
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
                .ForEach(d => dateList.Add(ConvertDateToDateString(d.Date)));
            return dateList;
        }
    }
}