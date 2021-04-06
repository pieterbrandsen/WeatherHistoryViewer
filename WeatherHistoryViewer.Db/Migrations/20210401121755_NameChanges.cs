using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherHistoryViewer.Db.Migrations
{
    public partial class NameChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherWarehouse_WeatherMeasurments_WeatherMeasurmentId",
                table: "WeatherWarehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeatherMeasurments",
                table: "WeatherMeasurments");

            migrationBuilder.RenameTable(
                name: "WeatherMeasurments",
                newName: "WeatherMeasurements");

            migrationBuilder.RenameColumn(
                name: "WeatherMeasurmentId",
                table: "WeatherWarehouse",
                newName: "WeatherMeasurementId");

            migrationBuilder.RenameIndex(
                name: "IX_WeatherWarehouse_WeatherMeasurmentId",
                table: "WeatherWarehouse",
                newName: "IX_WeatherWarehouse_WeatherMeasurementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeatherMeasurements",
                table: "WeatherMeasurements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherWarehouse_WeatherMeasurements_WeatherMeasurementId",
                table: "WeatherWarehouse",
                column: "WeatherMeasurementId",
                principalTable: "WeatherMeasurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherWarehouse_WeatherMeasurements_WeatherMeasurementId",
                table: "WeatherWarehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeatherMeasurements",
                table: "WeatherMeasurements");

            migrationBuilder.RenameTable(
                name: "WeatherMeasurements",
                newName: "WeatherMeasurments");

            migrationBuilder.RenameColumn(
                name: "WeatherMeasurementId",
                table: "WeatherWarehouse",
                newName: "WeatherMeasurmentId");

            migrationBuilder.RenameIndex(
                name: "IX_WeatherWarehouse_WeatherMeasurementId",
                table: "WeatherWarehouse",
                newName: "IX_WeatherWarehouse_WeatherMeasurmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeatherMeasurments",
                table: "WeatherMeasurments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherWarehouse_WeatherMeasurments_WeatherMeasurmentId",
                table: "WeatherWarehouse",
                column: "WeatherMeasurmentId",
                principalTable: "WeatherMeasurments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
