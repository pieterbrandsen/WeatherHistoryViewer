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
    [Migration("20210123114134_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("WeatherHistoryViewer.Core.CurrentWeather", b =>
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

                    b.Property<int>("Precip")
                        .HasColumnType("int");

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

                    b.Property<string>("WeatherDescriptions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WeatherIcons")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WindDegree")
                        .HasColumnType("int");

                    b.Property<string>("WindDir")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WindSpeed")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Weather");
                });
#pragma warning restore 612, 618
        }
    }
}
