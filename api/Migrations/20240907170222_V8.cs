using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class V8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stations_WeatherStations_GeneratedAt",
                table: "Stations");

            migrationBuilder.AlterColumn<long>(
                name: "GeneratedAt",
                table: "Stations",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_WeatherStations_GeneratedAt",
                table: "Stations",
                column: "GeneratedAt",
                principalTable: "WeatherStations",
                principalColumn: "GeneratedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stations_WeatherStations_GeneratedAt",
                table: "Stations");

            migrationBuilder.AlterColumn<long>(
                name: "GeneratedAt",
                table: "Stations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_WeatherStations_GeneratedAt",
                table: "Stations",
                column: "GeneratedAt",
                principalTable: "WeatherStations",
                principalColumn: "GeneratedAt",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
