using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherHistoryViewer.Db.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weather",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObservationTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temperature = table.Column<int>(type: "int", nullable: false),
                    WeatherCode = table.Column<int>(type: "int", nullable: false),
                    WeatherIcons = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeatherDescriptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WindSpeed = table.Column<int>(type: "int", nullable: false),
                    WindDegree = table.Column<int>(type: "int", nullable: false),
                    WindDir = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pressure = table.Column<int>(type: "int", nullable: false),
                    Precip = table.Column<int>(type: "int", nullable: false),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    Cloudcover = table.Column<int>(type: "int", nullable: false),
                    Feelslike = table.Column<int>(type: "int", nullable: false),
                    UvIndex = table.Column<int>(type: "int", nullable: false),
                    Visibility = table.Column<int>(type: "int", nullable: false),
                    IsDay = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weather");
        }
    }
}
