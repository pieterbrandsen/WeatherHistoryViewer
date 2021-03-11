using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WeatherHistoryViewer.Db;
using WeatherHistoryViewer.Services;
using WeatherHistoryViewer.Services.Handlers;

namespace WeatherHistoryViewer.UnitTest
{
    public class HandlerNames
    {
        public const string Database = "Database";
        public const string Date = "Date";
        public const string Location = "Location";
        public const string Weather = "Weather";
    }
}
