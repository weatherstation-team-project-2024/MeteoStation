using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class V7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stations_WeatherStations_WeatherStationGeneratedAt",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "WeatherStationsId",
                table: "Stations");

            migrationBuilder.RenameColumn(
                name: "WeatherStationGeneratedAt",
                table: "Stations",
                newName: "GeneratedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Stations_WeatherStationGeneratedAt",
                table: "Stations",
                newName: "IX_Stations_GeneratedAt");

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_WeatherStations_GeneratedAt",
                table: "Stations",
                column: "GeneratedAt",
                principalTable: "WeatherStations",
                principalColumn: "GeneratedAt",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stations_WeatherStations_GeneratedAt",
                table: "Stations");

            migrationBuilder.RenameColumn(
                name: "GeneratedAt",
                table: "Stations",
                newName: "WeatherStationGeneratedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Stations_GeneratedAt",
                table: "Stations",
                newName: "IX_Stations_WeatherStationGeneratedAt");

            migrationBuilder.AddColumn<int>(
                name: "WeatherStationsId",
                table: "Stations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_WeatherStations_WeatherStationGeneratedAt",
                table: "Stations",
                column: "WeatherStationGeneratedAt",
                principalTable: "WeatherStations",
                principalColumn: "GeneratedAt",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
