using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class V4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WeatherStationsGeneratedAt",
                table: "Stations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "WeatherStationsId",
                table: "Stations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WeatherStations",
                columns: table => new
                {
                    GeneratedAt = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherStations", x => x.GeneratedAt);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stations_WeatherStationsGeneratedAt",
                table: "Stations",
                column: "WeatherStationsGeneratedAt");

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_WeatherStations_WeatherStationsGeneratedAt",
                table: "Stations",
                column: "WeatherStationsGeneratedAt",
                principalTable: "WeatherStations",
                principalColumn: "GeneratedAt",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stations_WeatherStations_WeatherStationsGeneratedAt",
                table: "Stations");

            migrationBuilder.DropTable(
                name: "WeatherStations");

            migrationBuilder.DropIndex(
                name: "IX_Stations_WeatherStationsGeneratedAt",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "WeatherStationsGeneratedAt",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "WeatherStationsId",
                table: "Stations");
        }
    }
}
