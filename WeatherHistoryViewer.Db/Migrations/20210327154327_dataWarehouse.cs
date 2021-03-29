using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherHistoryViewer.Db.Migrations
{
    public partial class dataWarehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LastUpdateTimes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LastUpdateTimes", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "LocationsWarehouse",
                columns: table => new
                {
                    LocationName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationsWarehouse", x => x.LocationName);
                });

            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Times", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherMeasurments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvgTemp = table.Column<double>(type: "float", nullable: false),
                    MaxTemp = table.Column<double>(type: "float", nullable: false),
                    MinTemp = table.Column<double>(type: "float", nullable: false),
                    SunHour = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherMeasurments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherWarehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TimeId = table.Column<int>(type: "int", nullable: true),
                    WeatherMeasurmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherWarehouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherWarehouse_LocationsWarehouse_LocationName",
                        column: x => x.LocationName,
                        principalTable: "LocationsWarehouse",
                        principalColumn: "LocationName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WeatherWarehouse_Times_TimeId",
                        column: x => x.TimeId,
                        principalTable: "Times",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WeatherWarehouse_WeatherMeasurments_WeatherMeasurmentId",
                        column: x => x.WeatherMeasurmentId,
                        principalTable: "WeatherMeasurments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "LastUpdateTimes",
                columns: new[] { "Name", "Date" },
                values: new object[] { "Weather", "0000/01/01" });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherWarehouse_LocationName",
                table: "WeatherWarehouse",
                column: "LocationName");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherWarehouse_TimeId",
                table: "WeatherWarehouse",
                column: "TimeId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherWarehouse_WeatherMeasurmentId",
                table: "WeatherWarehouse",
                column: "WeatherMeasurmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LastUpdateTimes");

            migrationBuilder.DropTable(
                name: "WeatherWarehouse");

            migrationBuilder.DropTable(
                name: "LocationsWarehouse");

            migrationBuilder.DropTable(
                name: "Times");

            migrationBuilder.DropTable(
                name: "WeatherMeasurments");
        }
    }
}
