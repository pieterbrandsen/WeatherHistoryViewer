using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherHistoryViewer.Core.Models;
using WeatherHistoryViewer.Core.Models.Weather;
using WeatherHistoryViewer.Services;

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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IWeatherDataHandler weatherDataHandler)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            weatherDataHandler.AddHistoricalWeatherToDb("Baarn", "2015-01-22", HourlyInterval.Hours1);
        }
    }
}