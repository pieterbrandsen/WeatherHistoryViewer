using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherHistoryViewer.Db.Migrations
{
    public partial class AddedFullDateToSnapshot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "FullDate",
                "WeatherHourly",
                "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "FullDate",
                "WeatherHourly");
        }
    }
}