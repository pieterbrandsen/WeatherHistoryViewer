﻿// <auto/generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherHistoryViewer.Db;

namespace WeatherHistoryViewer.Db.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210327154327_dataWarehouse")]
    partial class dataWarehouse
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WeatherHistoryViewer.Core.Models.DataWarehouse.LocationWarehouse", b =>
                {
                    b.Property<string>("LocationName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LocationName");

                    b.ToTable("LocationsWarehouse");
                });

            modelBuilder.Entity("WeatherHistoryViewer.Core.Models.DataWarehouse.Time", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Times");
                });

            modelBuilder.Entity("WeatherHistoryViewer.Core.Models.DataWarehouse.UpdateTime", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Name");

                    b.ToTable("LastUpdateTimes");

                    b.HasData(
                        new
                        {
                            Name = "Weather",
                            Date = "0000/01/01"
                        });
                });

            modelBuilder.Entity("WeatherHistoryViewer.Core.Models.DataWarehouse.WeatherMeasurment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AvgTemp")
                        .HasColumnType("float");

                    b.Property<double>("MaxTemp")
                        .HasColumnType("float");

                    b.Property<double>("MinTemp")
                        .HasColumnType("float");

                    b.Property<double>("SunHour")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("WeatherMeasurments");
                });

            modelBuilder.Entity("WeatherHistoryViewer.Core.Models.DataWarehouse.WeatherWarehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LocationName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("TimeId")
                        .HasColumnType("int");

                    b.Property<int?>("WeatherMeasurmentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationName");

                    b.HasIndex("TimeId");

                    b.HasIndex("WeatherMeasurmentId");

                    b.ToTable("WeatherWarehouse");
                });

            modelBuilder.Entity("WeatherHistoryViewer.Core.Models.Weather.HistoricalWeather", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AvgTemp")
                        .HasColumnType("float");

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DateEpoch")
                        .HasColumnType("int");

                    b.Property<string>("LocationName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("MaxTemp")
                        .HasColumnType("float");

                    b.Property<double>("MinTemp")
                        .HasColumnType("float");

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

            modelBuilder.Entity("WeatherHistoryViewer.Core.Models.DataWarehouse.WeatherWarehouse", b =>
                {
                    b.HasOne("WeatherHistoryViewer.Core.Models.DataWarehouse.LocationWarehouse", "Location")
                        .WithMany()
                        .HasForeignKey("LocationName");

                    b.HasOne("WeatherHistoryViewer.Core.Models.DataWarehouse.Time", "Time")
                        .WithMany()
                        .HasForeignKey("TimeId");

                    b.HasOne("WeatherHistoryViewer.Core.Models.DataWarehouse.WeatherMeasurment", "WeatherMeasurment")
                        .WithMany()
                        .HasForeignKey("WeatherMeasurmentId");

                    b.Navigation("Location");

                    b.Navigation("Time");

                    b.Navigation("WeatherMeasurment");
                });

            modelBuilder.Entity("WeatherHistoryViewer.Core.Models.Weather.HistoricalWeather", b =>
                {
                    b.HasOne("WeatherHistoryViewer.Core.Models.Weather.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationName");

                    b.Navigation("Location");
                });
#pragma warning restore 612, 618
        }
    }
}
