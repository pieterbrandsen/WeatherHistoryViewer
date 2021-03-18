using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Services.Helpers;
using Xunit;

namespace WeatherHistoryViewer.UnitTests.Services
{
    public class HttpStatusTests
    {
        private readonly HttpStatus _httpStatus = new();

        public static IEnumerable<object[]> GetHttpStatusTypes()
        {
            foreach (var type in Enum.GetValues(typeof(HttpStatusTypes)))
            {
                yield return new object[] { type };
            }
        }

        [Theory]
        [MemberData(nameof(GetHttpStatusTypes))]
        public void ExpectStatusModelToHaveSameCodeAsInput(HttpStatusTypes type)
        {
            var statusModel = _httpStatus.GetErrorModel(type).StatusModel;
            Assert.Equal(type.ToString(), statusModel.Type);
        }
    }
}
