using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Services;
using WeatherHistoryViewer.Services.Handlers;

namespace WeatherHistoryViewer.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.RegisterDataFactoryServices(Configuration);
            services.RegisterInterfaceServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILocationHandler locationData, IWeatherHandler weatherData)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            var dateHelper = new DateHelper();
            var oldestDate = dateHelper.GetDateStringOfDaysAgo(90);
            var yesterdayDate = dateHelper.GetDateStringOfDaysAgo(1);
            var locations = locationData.GetAllLocationNames();
            Task.Run(() =>
            {
            weatherData.UpdateHistoricalWeatherRangeToDb("Grachen", HourlyInterval.Hours1);
                foreach (var locationName in locations)
                    weatherData.UpdateHistoricalWeatherRangeToDb(locationName, HourlyInterval.Hours1, oldestDate,
                        yesterdayDate);
            });
        }
    }
}
