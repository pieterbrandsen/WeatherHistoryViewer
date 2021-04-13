using System;
using System.Collections.Generic;
using WeatherHistoryViewer.Services.Helpers;
using Xunit;

namespace WeatherHistoryViewer.UnitTests.Helpers
{
    public class Date
    {
        private static readonly DateHelper DateHelper = new();

        [Fact]
        public void GetAllDatesReturnShouldHaveALengthLargerThenZero()
        {
            var dateList = DateHelper.GetAllDates();

            Assert.InRange(dateList.Count, 1, 99999);
        }

        public static IEnumerable<object[]> GetDateRanges()
        {
            for (var i = 1; i < 15; i++)
            {
                var newestDate = DateHelper.GetDateStringOfDaysAgo(i);
                var oldestDate = DateHelper.GetDateStringOfDaysAgo(i * 2);
                var expectedLength = i + 1;
                yield return new object[] {oldestDate, newestDate, expectedLength};
            }

            yield return new object[] {null, null, DateHelper.GetAllDates().Count + 1};
            yield return new object[] {DateHelper.GetDateStringOfDaysAgo(2), null, 3};
            yield return new object[] {DateHelper.GetDateStringOfDaysAgo(), DateHelper.GetDateStringOfDaysAgo(2), 6};
        }

        [Theory]
        [MemberData(nameof(GetDateRanges))]
        public void GetRangeOfRequestableDatesShouldReturnExpectedLength(string oldestDateString,
            string newestDateString, int expectedLength)
        {
            var dateList = DateHelper.GetRangeOfDates(oldestDateString, newestDateString);

            Assert.Equal(expectedLength, dateList.Count);
        }

        [Fact]
        public void ConvertDateStringToDateShouldReturnDatesWhenCorrectlyFormatted()
        {
            var dateString = "1-1-2001";
            var dateTimeObj = DateHelper.ConvertDateStringToDate(dateString);
            Assert.IsType<DateTime>(dateTimeObj);
            Assert.Equal(new DateTime(2001, 1, 1).Ticks, dateTimeObj.Ticks);
        }


        public static IEnumerable<object[]> GetListOfDates()
        {
            for (var i = 1; i < 100; i += 4) yield return new object[] {DateHelper.GetDateStringOfDaysAgo(i)};
        }

        [Theory]
        [MemberData(nameof(GetListOfDates))]
        public void GetWeekDatesFromDateShouldReturn7AsLength(string date)
        {
            var dateList = DateHelper.GetWeekDatesFromDate(date);
            Assert.Equal(7, dateList.Count);
        }
    }
}