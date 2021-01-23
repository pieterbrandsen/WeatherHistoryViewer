using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherHistoryViewer.Db.Migrations
{
    public partial class AddedLists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeatherDescriptions",
                table: "Weather");

            migrationBuilder.DropColumn(
                name: "WeatherIcons",
                table: "Weather");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WeatherDescriptions",
                table: "Weather",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeatherIcons",
                table: "Weather",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
