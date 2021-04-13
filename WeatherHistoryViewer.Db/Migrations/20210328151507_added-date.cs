using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherHistoryViewer.Db.Migrations
{
    public partial class addeddate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "Date",
                "Times",
                "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                "LastUpdateTimes",
                "Name",
                "Weather",
                "Date",
                "0001/01/01");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Date",
                "Times");

            migrationBuilder.UpdateData(
                "LastUpdateTimes",
                "Name",
                "Weather",
                "Date",
                "0000/01/01");
        }
    }
}