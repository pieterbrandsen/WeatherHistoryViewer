using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherHistoryViewer.Db.Migrations
{
    public partial class addTotalSnowDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                "TotalSnow",
                "Weather",
                "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                "TotalSnow",
                "Weather",
                "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}