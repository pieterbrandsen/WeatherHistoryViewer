using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherHistoryViewer.Db.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Weather",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table => { table.PrimaryKey("PK_Weather", x => x.Id); });

            migrationBuilder.CreateTable(
                "CurrentWeather",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeatherModelId = table.Column<int>("int", nullable: false),
                    ObservationTime = table.Column<string>("nvarchar(max)", nullable: true),
                    Temperature = table.Column<int>("int", nullable: false),
                    WeatherCode = table.Column<int>("int", nullable: false),
                    WindSpeed = table.Column<int>("int", nullable: false),
                    WindDegree = table.Column<int>("int", nullable: false),
                    WindDir = table.Column<string>("nvarchar(max)", nullable: true),
                    Pressure = table.Column<int>("int", nullable: false),
                    Precip = table.Column<double>("float", nullable: false),
                    Humidity = table.Column<int>("int", nullable: false),
                    Cloudcover = table.Column<int>("int", nullable: false),
                    Feelslike = table.Column<int>("int", nullable: false),
                    UvIndex = table.Column<int>("int", nullable: false),
                    Visibility = table.Column<int>("int", nullable: false),
                    IsDay = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentWeather", x => x.Id);
                    table.ForeignKey(
                        "FK_CurrentWeather_Weather_WeatherModelId",
                        x => x.WeatherModelId,
                        "Weather",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Location",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeatherModelId = table.Column<int>("int", nullable: false),
                    Name = table.Column<string>("nvarchar(max)", nullable: true),
                    Country = table.Column<string>("nvarchar(max)", nullable: true),
                    Region = table.Column<string>("nvarchar(max)", nullable: true),
                    Lat = table.Column<string>("nvarchar(max)", nullable: true),
                    Lon = table.Column<string>("nvarchar(max)", nullable: true),
                    TimezoneId = table.Column<string>("nvarchar(max)", nullable: true),
                    Localtime = table.Column<string>("nvarchar(max)", nullable: true),
                    LocaltimeEpoch = table.Column<int>("int", nullable: false),
                    UtcOffset = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        "FK_Location_Weather_WeatherModelId",
                        x => x.WeatherModelId,
                        "Weather",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_CurrentWeather_WeatherModelId",
                "CurrentWeather",
                "WeatherModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Location_WeatherModelId",
                "Location",
                "WeatherModelId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "CurrentWeather");

            migrationBuilder.DropTable(
                "Location");

            migrationBuilder.DropTable(
                "Weather");
        }
    }
}