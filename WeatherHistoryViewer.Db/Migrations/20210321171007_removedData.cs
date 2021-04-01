using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherHistoryViewer.Db.Migrations
{
    public partial class removedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "WeatherHourly");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "WeatherHourly",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chanceoffog = table.Column<int>("int", nullable: false),
                    Chanceoffrost = table.Column<int>("int", nullable: false),
                    Chanceofhightemp = table.Column<int>("int", nullable: false),
                    Chanceofovercast = table.Column<int>("int", nullable: false),
                    Chanceofrain = table.Column<int>("int", nullable: false),
                    Chanceofremdry = table.Column<int>("int", nullable: false),
                    Chanceofsnow = table.Column<int>("int", nullable: false),
                    Chanceofsunshine = table.Column<int>("int", nullable: false),
                    Chanceofthunder = table.Column<int>("int", nullable: false),
                    Chanceofwindy = table.Column<int>("int", nullable: false),
                    Cloudcover = table.Column<int>("int", nullable: false),
                    Dewpoint = table.Column<int>("int", nullable: false),
                    Feelslike = table.Column<int>("int", nullable: false),
                    FullDate = table.Column<string>("nvarchar(max)", nullable: true),
                    Heatindex = table.Column<int>("int", nullable: false),
                    HistoricalWeatherId = table.Column<int>("int", nullable: false),
                    Humidity = table.Column<int>("int", nullable: false),
                    Precip = table.Column<double>("float", nullable: false),
                    Pressure = table.Column<int>("int", nullable: false),
                    Temperature = table.Column<int>("int", nullable: false),
                    Ticks = table.Column<long>("bigint", nullable: false),
                    Time = table.Column<string>("nvarchar(max)", nullable: true),
                    UvIndex = table.Column<int>("int", nullable: false),
                    Visibility = table.Column<int>("int", nullable: false),
                    WeatherCode = table.Column<int>("int", nullable: false),
                    WindDegree = table.Column<int>("int", nullable: false),
                    WindDir = table.Column<string>("nvarchar(max)", nullable: true),
                    WindSpeed = table.Column<int>("int", nullable: false),
                    Windchill = table.Column<int>("int", nullable: false),
                    Windgust = table.Column<int>("int", nullable: false)
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
                "IX_WeatherHourly_HistoricalWeatherId",
                "WeatherHourly",
                "HistoricalWeatherId");
        }
    }
}