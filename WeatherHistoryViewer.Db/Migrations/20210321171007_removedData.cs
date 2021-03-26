using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherHistoryViewer.Db.Migrations
{
    public partial class removedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherHourly");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherHourly",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chanceoffog = table.Column<int>(type: "int", nullable: false),
                    Chanceoffrost = table.Column<int>(type: "int", nullable: false),
                    Chanceofhightemp = table.Column<int>(type: "int", nullable: false),
                    Chanceofovercast = table.Column<int>(type: "int", nullable: false),
                    Chanceofrain = table.Column<int>(type: "int", nullable: false),
                    Chanceofremdry = table.Column<int>(type: "int", nullable: false),
                    Chanceofsnow = table.Column<int>(type: "int", nullable: false),
                    Chanceofsunshine = table.Column<int>(type: "int", nullable: false),
                    Chanceofthunder = table.Column<int>(type: "int", nullable: false),
                    Chanceofwindy = table.Column<int>(type: "int", nullable: false),
                    Cloudcover = table.Column<int>(type: "int", nullable: false),
                    Dewpoint = table.Column<int>(type: "int", nullable: false),
                    Feelslike = table.Column<int>(type: "int", nullable: false),
                    FullDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Heatindex = table.Column<int>(type: "int", nullable: false),
                    HistoricalWeatherId = table.Column<int>(type: "int", nullable: false),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    Precip = table.Column<double>(type: "float", nullable: false),
                    Pressure = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<int>(type: "int", nullable: false),
                    Ticks = table.Column<long>(type: "bigint", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UvIndex = table.Column<int>(type: "int", nullable: false),
                    Visibility = table.Column<int>(type: "int", nullable: false),
                    WeatherCode = table.Column<int>(type: "int", nullable: false),
                    WindDegree = table.Column<int>(type: "int", nullable: false),
                    WindDir = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WindSpeed = table.Column<int>(type: "int", nullable: false),
                    Windchill = table.Column<int>(type: "int", nullable: false),
                    Windgust = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherHourly", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherHourly_Weather_HistoricalWeatherId",
                        column: x => x.HistoricalWeatherId,
                        principalTable: "Weather",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherHourly_HistoricalWeatherId",
                table: "WeatherHourly",
                column: "HistoricalWeatherId");
        }
    }
}
