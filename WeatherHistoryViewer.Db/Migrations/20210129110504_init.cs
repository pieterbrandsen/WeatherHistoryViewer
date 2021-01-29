using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherHistoryViewer.Db.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Locations",
                table => new
                {
                    Name = table.Column<string>("nvarchar(450)", nullable: false),
                    Country = table.Column<string>("nvarchar(max)", nullable: true),
                    Region = table.Column<string>("nvarchar(max)", nullable: true),
                    Lat = table.Column<string>("nvarchar(max)", nullable: true),
                    Lon = table.Column<string>("nvarchar(max)", nullable: true),
                    TimezoneId = table.Column<string>("nvarchar(max)", nullable: true),
                    UtcOffset = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Locations", x => x.Name); });

            migrationBuilder.CreateTable(
                "Weather",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>("nvarchar(450)", nullable: true),
                    Date = table.Column<string>("nvarchar(max)", nullable: true),
                    DateEpoch = table.Column<int>("int", nullable: false),
                    MinTemp = table.Column<int>("int", nullable: false),
                    MaxTemp = table.Column<int>("int", nullable: false),
                    AvgTemp = table.Column<int>("int", nullable: false),
                    TotalSnow = table.Column<int>("int", nullable: false),
                    SunHour = table.Column<double>("float", nullable: false),
                    UvIndex = table.Column<int>("int", nullable: false),
                    HourlyInterval = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.Id);
                    table.ForeignKey(
                        "FK_Weather_Locations_LocationName",
                        x => x.LocationName,
                        "Locations",
                        "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "WeatherHourly",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoricalWeatherId = table.Column<int>("int", nullable: false),
                    Time = table.Column<string>("nvarchar(max)", nullable: true),
                    Temperature = table.Column<int>("int", nullable: false),
                    WindSpeed = table.Column<int>("int", nullable: false),
                    WindDegree = table.Column<int>("int", nullable: false),
                    WindDir = table.Column<string>("nvarchar(max)", nullable: true),
                    WeatherCode = table.Column<int>("int", nullable: false),
                    Precip = table.Column<double>("float", nullable: false),
                    Humidity = table.Column<int>("int", nullable: false),
                    Visibility = table.Column<int>("int", nullable: false),
                    Pressure = table.Column<int>("int", nullable: false),
                    Cloudcover = table.Column<int>("int", nullable: false),
                    Heatindex = table.Column<int>("int", nullable: false),
                    Dewpoint = table.Column<int>("int", nullable: false),
                    Windchill = table.Column<int>("int", nullable: false),
                    Windgust = table.Column<int>("int", nullable: false),
                    Feelslike = table.Column<int>("int", nullable: false),
                    Chanceofrain = table.Column<int>("int", nullable: false),
                    Chanceofremdry = table.Column<int>("int", nullable: false),
                    Chanceofwindy = table.Column<int>("int", nullable: false),
                    Chanceofovercast = table.Column<int>("int", nullable: false),
                    Chanceofsunshine = table.Column<int>("int", nullable: false),
                    Chanceoffrost = table.Column<int>("int", nullable: false),
                    Chanceofhightemp = table.Column<int>("int", nullable: false),
                    Chanceoffog = table.Column<int>("int", nullable: false),
                    Chanceofsnow = table.Column<int>("int", nullable: false),
                    Chanceofthunder = table.Column<int>("int", nullable: false),
                    UvIndex = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherHourly", x => x.Id);
                    table.ForeignKey(
                        "FK_WeatherHourly_Weather_HistoricalWeatherId",
                        x => x.HistoricalWeatherId,
                        "Weather",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Weather_LocationName",
                "Weather",
                "LocationName");

            migrationBuilder.CreateIndex(
                "IX_WeatherHourly_HistoricalWeatherId",
                "WeatherHourly",
                "HistoricalWeatherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "WeatherHourly");

            migrationBuilder.DropTable(
                "Weather");

            migrationBuilder.DropTable(
                "Locations");
        }
    }
}