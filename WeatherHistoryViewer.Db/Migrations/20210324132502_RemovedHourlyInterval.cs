using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherHistoryViewer.Db.Migrations
{
    public partial class RemovedHourlyInterval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "HourlyInterval",
                "Weather");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "HourlyInterval",
                "Weather",
                "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}