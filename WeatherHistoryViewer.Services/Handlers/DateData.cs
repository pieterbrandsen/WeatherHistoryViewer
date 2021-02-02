using System;
using System.Collections.Generic;

namespace WeatherHistoryViewer.Services.Handlers
{
    public interface IDateData
    {
        public List<string> GetAllRequestableDates();
        public List<string> GetRangeOfRequestableDates(string oldestDateString, string newestDateString = null);
        public string GetDateStringOfDaysAgo(int dayCount = 7);
    }

    public class DateData : IDateData
    {
        private const string OldestDateString = "2008-07-01";

        public List<string> GetAllRequestableDates()
        {
            var dateList = new List<string>();
            var oldestDate = ConvertDateStringToDate(OldestDateString);
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
            var splitDate = date.Split("-");
            return new DateTime(Convert.ToInt16(splitDate[0]), Convert.ToInt16(splitDate[1]),
                Convert.ToInt16(splitDate[2]));
        }
    }
}