using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherHistoryViewer.Core.Models;
using WeatherHistoryViewer.Services;
using WeatherHistoryViewer.Services.Handlers;

namespace WeatherHistoryViewer.APISender
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<UserSecrets>(Configuration.GetSection(nameof(UserSecrets)))
                .RegisterDataServices(Configuration)
                .RegisterInterfaceServices(Configuration)
                .AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IWeatherData weatherData,
            IWeatherTimer weatherTimer)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            weatherTimer.StartTimer();
            //weatherData.AddWeatherToDb("Baarn", "2018-01-04", HourlyInterval.Hours1);
            //weatherData.AddHistoricalWeatherRangeToDb("Baarn", HourlyInterval.Hours1, "2018-01-01");
        }
    }
}