using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherHistoryViewer.Db.Migrations
{
    public partial class dataWarehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "LastUpdateTimes",
                table => new
                {
                    Name = table.Column<string>("nvarchar(450)", nullable: false),
                    Date = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_LastUpdateTimes", x => x.Name); });

            migrationBuilder.CreateTable(
                "LocationsWarehouse",
                table => new
                {
                    LocationName = table.Column<string>("nvarchar(450)", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_LocationsWarehouse", x => x.LocationName); });

            migrationBuilder.CreateTable(
                "Times",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>("int", nullable: false),
                    Month = table.Column<int>("int", nullable: false),
                    Year = table.Column<int>("int", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Times", x => x.Id); });

            migrationBuilder.CreateTable(
                "WeatherMeasurments",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvgTemp = table.Column<double>("float", nullable: false),
                    MaxTemp = table.Column<double>("float", nullable: false),
                    MinTemp = table.Column<double>("float", nullable: false),
                    SunHour = table.Column<double>("float", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_WeatherMeasurments", x => x.Id); });

            migrationBuilder.CreateTable(
                "WeatherWarehouse",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>("nvarchar(450)", nullable: true),
                    TimeId = table.Column<int>("int", nullable: true),
                    WeatherMeasurmentId = table.Column<int>("int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherWarehouse", x => x.Id);
                    table.ForeignKey(
                        "FK_WeatherWarehouse_LocationsWarehouse_LocationName",
                        x => x.LocationName,
                        "LocationsWarehouse",
                        "LocationName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_WeatherWarehouse_Times_TimeId",
                        x => x.TimeId,
                        "Times",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_WeatherWarehouse_WeatherMeasurments_WeatherMeasurmentId",
                        x => x.WeatherMeasurmentId,
                        "WeatherMeasurments",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                "LastUpdateTimes",
                new[] {"Name", "Date"},
                new object[] {"Weather", "0000/01/01"});

            migrationBuilder.CreateIndex(
                "IX_WeatherWarehouse_LocationName",
                "WeatherWarehouse",
                "LocationName");

            migrationBuilder.CreateIndex(
                "IX_WeatherWarehouse_TimeId",
                "WeatherWarehouse",
                "TimeId");

            migrationBuilder.CreateIndex(
                "IX_WeatherWarehouse_WeatherMeasurmentId",
                "WeatherWarehouse",
                "WeatherMeasurmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "LastUpdateTimes");

            migrationBuilder.DropTable(
                "WeatherWarehouse");

            migrationBuilder.DropTable(
                "LocationsWarehouse");

            migrationBuilder.DropTable(
                "Times");

            migrationBuilder.DropTable(
                "WeatherMeasurments");
        }
    }
}