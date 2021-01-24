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
    [Migration("20210124111401_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("WeatherHistoryViewer.Core.CurrentWeatherWKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Cloudcover")
                        .HasColumnType("int");

                    b.Property<int>("Feelslike")
                        .HasColumnType("int");

                    b.Property<int>("Humidity")
                        .HasColumnType("int");

                    b.Property<string>("IsDay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ObservationTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Precip")
                        .HasColumnType("float");

                    b.Property<int>("Pressure")
                        .HasColumnType("int");

                    b.Property<int>("Temperature")
                        .HasColumnType("int");

                    b.Property<int>("UvIndex")
                        .HasColumnType("int");

                    b.Property<int>("Visibility")
                        .HasColumnType("int");

                    b.Property<int>("WeatherCode")
                        .HasColumnType("int");

                    b.Property<int>("WeatherModelId")
                        .HasColumnType("int");

                    b.Property<int>("WindDegree")
                        .HasColumnType("int");

                    b.Property<string>("WindDir")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WindSpeed")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WeatherModelId")
                        .IsUnique();

                    b.ToTable("CurrentWeather");
                });

            modelBuilder.Entity("WeatherHistoryViewer.Core.LocationWKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Localtime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocaltimeEpoch")
                        .HasColumnType("int");

                    b.Property<string>("Lon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimezoneId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UtcOffset")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WeatherModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WeatherModelId")
                        .IsUnique();

                    b.ToTable("Location");
                });

            modelBuilder.Entity("WeatherHistoryViewer.Core.WeatherModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.HasKey("Id");

                    b.ToTable("Weather");
                });

            modelBuilder.Entity("WeatherHistoryViewer.Core.CurrentWeatherWKey", b =>
                {
                    b.HasOne("WeatherHistoryViewer.Core.WeatherModel", "WeatherModel")
                        .WithOne("CurrentWeather")
                        .HasForeignKey("WeatherHistoryViewer.Core.CurrentWeatherWKey", "WeatherModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WeatherModel");
                });

            modelBuilder.Entity("WeatherHistoryViewer.Core.LocationWKey", b =>
                {
                    b.HasOne("WeatherHistoryViewer.Core.WeatherModel", "WeatherModel")
                        .WithOne("Location")
                        .HasForeignKey("WeatherHistoryViewer.Core.LocationWKey", "WeatherModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WeatherModel");
                });

            modelBuilder.Entity("WeatherHistoryViewer.Core.WeatherModel", b =>
                {
                    b.Navigation("CurrentWeather");

                    b.Navigation("Location");
                });
#pragma warning restore 612, 618
        }
    }
}
