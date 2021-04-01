using System;
using System.Collections.Generic;
using System.Linq;
using WeatherHistoryViewer.Db;

namespace WeatherHistoryViewer.Services.Helpers
{
    public class DateHelper
    {
        public const string OldestDate = "2008/07/01";

        public List<string> GetAllDates()
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

        public List<string> GetRangeOfDates(string oldestDateString = null, string newestDateString = null)
        {
            var dateList = new List<string>();
            var newestDate = newestDateString == null ? DateTime.Today : ConvertDateStringToDate(newestDateString);

            dateList.Add(ConvertDateToDateString(newestDate));
            var i = newestDate;
            var latestDateString = ConvertDateToDateString(i);
            if (oldestDateString == null)
                oldestDateString = OldestDate;
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
            var date = DateTime.Today.AddDays(dayAmount * -1);

            return ConvertDateToDateString(date);
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

        private DateTime GetDateOfTomorrow(DateTime currentDate)
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
            var currentDate = ConvertDateStringToDate(date);
            var dayOfWeek = (int) currentDate.DayOfWeek;
            dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;
            var startOfWeek = currentDate.AddDays(1 - dayOfWeek);
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
                var dateOfTomorrow = GetDateOfTomorrow(j);
                j = dateOfTomorrow;
                dateTimeList.Add(j);

                if (j.DayOfWeek == DayOfWeek.Saturday) dateTimeList.Add(GetDateOfTomorrow(j));
            }

            var dateList = new List<string>();
            dateTimeList.OrderByDescending(d => d.Ticks).ToList()
                .ForEach(d => dateList.Add(ConvertDateToDateString(d.Date)));
            return dateTimeList.Select(t=> ConvertDateToDateString(t.Date)).OrderByDescending(s=>s).ToList();
        }
    }
}