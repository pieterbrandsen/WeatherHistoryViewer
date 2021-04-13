using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherHistoryViewer.Db.Migrations
{
    public partial class NameChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_WeatherWarehouse_WeatherMeasurments_WeatherMeasurmentId",
                "WeatherWarehouse");

            migrationBuilder.DropPrimaryKey(
                "PK_WeatherMeasurments",
                "WeatherMeasurments");

            migrationBuilder.RenameTable(
                "WeatherMeasurments",
                newName: "WeatherMeasurements");

            migrationBuilder.RenameColumn(
                "WeatherMeasurmentId",
                "WeatherWarehouse",
                "WeatherMeasurementId");

            migrationBuilder.RenameIndex(
                "IX_WeatherWarehouse_WeatherMeasurmentId",
                table: "WeatherWarehouse",
                newName: "IX_WeatherWarehouse_WeatherMeasurementId");

            migrationBuilder.AddPrimaryKey(
                "PK_WeatherMeasurements",
                "WeatherMeasurements",
                "Id");

            migrationBuilder.AddForeignKey(
                "FK_WeatherWarehouse_WeatherMeasurements_WeatherMeasurementId",
                "WeatherWarehouse",
                "WeatherMeasurementId",
                "WeatherMeasurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_WeatherWarehouse_WeatherMeasurements_WeatherMeasurementId",
                "WeatherWarehouse");

            migrationBuilder.DropPrimaryKey(
                "PK_WeatherMeasurements",
                "WeatherMeasurements");

            migrationBuilder.RenameTable(
                "WeatherMeasurements",
                newName: "WeatherMeasurments");

            migrationBuilder.RenameColumn(
                "WeatherMeasurementId",
                "WeatherWarehouse",
                "WeatherMeasurmentId");

            migrationBuilder.RenameIndex(
                "IX_WeatherWarehouse_WeatherMeasurementId",
                table: "WeatherWarehouse",
                newName: "IX_WeatherWarehouse_WeatherMeasurmentId");

            migrationBuilder.AddPrimaryKey(
                "PK_WeatherMeasurments",
                "WeatherMeasurments",
                "Id");

            migrationBuilder.AddForeignKey(
                "FK_WeatherWarehouse_WeatherMeasurments_WeatherMeasurmentId",
                "WeatherWarehouse",
                "WeatherMeasurmentId",
                "WeatherMeasurments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}