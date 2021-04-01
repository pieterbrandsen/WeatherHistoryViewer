using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHistoryViewer.Services.Helpers;
using Xunit;

namespace WeatherHistoryViewer.UnitTests.Services
{
    public class Date
    {
        private static DateHelper _dateHelper = new();

        [Fact]
        public void GetAllRequestableDatesReturnShouldHaveALenghtLargerThenZero()
        {
            var dateList = _dateHelper.GetAllRequestableDates();

            Assert.InRange(dateList.Count, 1, 99999);
        }

        public static IEnumerable<object[]> GetDateRanges()
        {
            for (int i = 1; i < 15; i++)
            {
                var newestDate = _dateHelper.GetDateStringOfDaysAgo(i);
                var oldestDate = _dateHelper.GetDateStringOfDaysAgo(i * 2);
                var expectedLength = i + 1;
                yield return new object[] { oldestDate, newestDate, expectedLength };
            }

            yield return new object[] { null, null, _dateHelper.GetAllRequestableDates().Count + 1 };
            yield return new object[] { _dateHelper.GetDateStringOfDaysAgo(2), null, 3 };
            yield return new object[] { _dateHelper.GetDateStringOfDaysAgo(), _dateHelper.GetDateStringOfDaysAgo(2), 6 };

        }

        [Theory]
        [MemberData(nameof(GetDateRanges))]
        public void GetRangeOfRequestableDatesShouldReturnExpectedLength(string oldestDateString, string newestDateString, int expectedLength)
        {
            var dateList = _dateHelper.GetRangeOfRequestableDates(oldestDateString, newestDateString);

            Assert.Equal(expectedLength, dateList.Count);
        }

        [Theory]
        [InlineData("02-7")]
        [InlineData("2-07")]
        [InlineData("25-7")]
        [InlineData("25-7-2001")]
        [InlineData("")]
        [InlineData("abc")]
        public void GetDateInLast15YShouldReturnEmptyOrListWith15Dates(string shortDate)
        {
            var dateList = _dateHelper.GetDateInLast15Y(shortDate);
            if (shortDate.Split("-").Length == 2) Assert.Equal(15, dateList.Count);
            else Assert.Empty(dateList);
        }

        [Fact]
        public void ConvertDateStringToDateShouldReturnDatesWhenCorrectlyFormatted()
        {
            var dateString = "1-1-2001";
            var dateTimeObj = _dateHelper.ConvertDateStringToDate(dateString);
            Assert.IsType<DateTime>(dateTimeObj);
            Assert.Equal(new DateTime(2001, 1, 1).Ticks, dateTimeObj.Ticks);
        }


        public static IEnumerable<object[]> GetListOfDates()
        {
            for (int i = 1; i < 100; i += 4)
            {
                yield return new object[] { _dateHelper.GetDateStringOfDaysAgo(i) };
            }
        }

        [Theory]
        [MemberData(nameof(GetListOfDates))]
        public void GetWeekDatesFromDateShouldReturn7AsLength(string date)
        {
            var dateList = _dateHelper.GetWeekDatesFromDate(date);
            Assert.Equal(7, dateList.Count);
        }
    }
}
