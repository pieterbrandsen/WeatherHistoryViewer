using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherHistoryViewer.Db.Migrations
{
    public partial class renames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weather",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeatherModelId = table.Column<int>(type: "int", nullable: false),
                    ObservationTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temperature = table.Column<int>(type: "int", nullable: false),
                    WeatherCode = table.Column<int>(type: "int", nullable: false),
                    WindSpeed = table.Column<int>(type: "int", nullable: false),
                    WindDegree = table.Column<int>(type: "int", nullable: false),
                    WindDir = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pressure = table.Column<int>(type: "int", nullable: false),
                    Precip = table.Column<double>(type: "float", nullable: false),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    Cloudcover = table.Column<int>(type: "int", nullable: false),
                    Feelslike = table.Column<int>(type: "int", nullable: false),
                    UvIndex = table.Column<int>(type: "int", nullable: false),
                    Visibility = table.Column<int>(type: "int", nullable: false),
                    IsDay = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherHistory_Weather_WeatherModelId",
                        column: x => x.WeatherModelId,
                        principalTable: "Weather",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeatherLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeatherModelId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimezoneId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Localtime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocaltimeEpoch = table.Column<int>(type: "int", nullable: false),
                    UtcOffset = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherLocation_Weather_WeatherModelId",
                        column: x => x.WeatherModelId,
                        principalTable: "Weather",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherHistory_WeatherModelId",
                table: "WeatherHistory",
                column: "WeatherModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WeatherLocation_WeatherModelId",
                table: "WeatherLocation",
                column: "WeatherModelId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherHistory");

            migrationBuilder.DropTable(
                name: "WeatherLocation");

            migrationBuilder.DropTable(
                name: "Weather");
        }
    }
}
