﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherHistoryViewer.Db;

namespace WeatherHistoryViewer.Db.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210129185627_addTotalSnowDouble")]
    partial class addTotalSnowDouble
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("WeatherHistoryViewer.Core.Models.Weather.HistoricalWeather", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AvgTemp")
                        .HasColumnType("int");

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DateEpoch")
                        .HasColumnType("int");

                    b.Property<int>("HourlyInterval")
                        .HasColumnType("int");

                    b.Property<string>("LocationName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MaxTemp")
                        .HasColumnType("int");

                    b.Property<int>("MinTemp")
                        .HasColumnType("int");

                    b.Property<double>("SunHour")
                        .HasColumnType("float");

                    b.Property<double>("TotalSnow")
                        .HasColumnType("float");

                    b.Property<int>("UvIndex")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationName");

                    b.ToTable("Weather");
                });

            modelBuilder.Entity("WeatherHistoryViewer.Core.Models.Weather.Location", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimezoneId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UtcOffset")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Name");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("WeatherHistoryViewer.Core.Models.Weather.WeatherSnapshot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Chanceoffog")
                        .HasColumnType("int");

                    b.Property<int>("Chanceoffrost")
                        .HasColumnType("int");

                    b.Property<int>("Chanceofhightemp")
                        .HasColumnType("int");

                    b.Property<int>("Chanceofovercast")
                        .HasColumnType("int");

                    b.Property<int>("Chanceofrain")
                        .HasColumnType("int");

                    b.Property<int>("Chanceofremdry")
                        .HasColumnType("int");

                    b.Property<int>("Chanceofsnow")
                        .HasColumnType("int");

                    b.Property<int>("Chanceofsunshine")
                        .HasColumnType("int");

                    b.Property<int>("Chanceofthunder")
                        .HasColumnType("int");

                    b.Property<int>("Chanceofwindy")
                        .HasColumnType("int");

                    b.Property<int>("Cloudcover")
                        .HasColumnType("int");

                    b.Property<int>("Dewpoint")
                        .HasColumnType("int");

                    b.Property<int>("Feelslike")
                        .HasColumnType("int");

                    b.Property<int>("Heatindex")
                        .HasColumnType("int");

                    b.Property<int>("HistoricalWeatherId")
                        .HasColumnType("int");

                    b.Property<int>("Humidity")
                        .HasColumnType("int");

                    b.Property<double>("Precip")
                        .HasColumnType("float");

                    b.Property<int>("Pressure")
                        .HasColumnType("int");

                    b.Property<int>("Temperature")
                        .HasColumnType("int");

                    b.Property<string>("Time")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UvIndex")
                        .HasColumnType("int");

                    b.Property<int>("Visibility")
                        .HasColumnType("int");

                    b.Property<int>("WeatherCode")
                        .HasColumnType("int");

                    b.Property<int>("WindDegree")
                        .HasColumnType("int");

                    b.Property<string>("WindDir")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WindSpeed")
                        .HasColumnType("int");

                    b.Property<int>("Windchill")
                        .HasColumnType("int");

                    b.Property<int>("Windgust")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HistoricalWeatherId");

                    b.ToTable("WeatherHourly");
                });

            modelBuilder.Entity("WeatherHistoryViewer.Core.Models.Weather.HistoricalWeather", b =>
                {
                    b.HasOne("WeatherHistoryViewer.Core.Models.Weather.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationName");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("WeatherHistoryViewer.Core.Models.Weather.WeatherSnapshot", b =>
                {
                    b.HasOne("WeatherHistoryViewer.Core.Models.Weather.HistoricalWeather", "HistoricalWeather")
                        .WithMany("SnapshotsOfDay")
                        .HasForeignKey("HistoricalWeatherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HistoricalWeather");
                });

            modelBuilder.Entity("WeatherHistoryViewer.Core.Models.Weather.HistoricalWeather", b =>
                {
                    b.Navigation("SnapshotsOfDay");
                });
#pragma warning restore 612, 618
        }
    }
}
