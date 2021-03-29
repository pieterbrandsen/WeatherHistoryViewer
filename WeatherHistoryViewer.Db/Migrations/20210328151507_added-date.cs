using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherHistoryViewer.Db.Migrations
{
    public partial class addeddate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Times",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "LastUpdateTimes",
                keyColumn: "Name",
                keyValue: "Weather",
                column: "Date",
                value: "0001/01/01");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Times");

            migrationBuilder.UpdateData(
                table: "LastUpdateTimes",
                keyColumn: "Name",
                keyValue: "Weather",
                column: "Date",
                value: "0000/01/01");
        }
    }
}
