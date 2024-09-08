using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class MigrationV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Lsid = table.Column<int>(type: "integer", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SensorType = table.Column<int>(type: "integer", nullable: true),
                    Category = table.Column<string>(type: "text", nullable: true),
                    Manufacturer = table.Column<string>(type: "text", nullable: true),
                    ProductName = table.Column<string>(type: "text", nullable: true),
                    ProductNumber = table.Column<string>(type: "text", nullable: true),
                    RainCollectorType = table.Column<int>(type: "integer", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StationId = table.Column<int>(type: "integer", nullable: false),
                    StationIdUuid = table.Column<string>(type: "text", nullable: true),
                    StationName = table.Column<string>(type: "text", nullable: true),
                    ParentDeviceType = table.Column<string>(type: "text", nullable: true),
                    ParentDeviceName = table.Column<string>(type: "text", nullable: true),
                    ParentDeviceId = table.Column<long>(type: "bigint", nullable: true),
                    ParentDeviceIdHex = table.Column<string>(type: "text", nullable: true),
                    PortNumber = table.Column<int>(type: "integer", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: true),
                    Longitude = table.Column<double>(type: "double precision", nullable: true),
                    Elevation = table.Column<double>(type: "double precision", nullable: true),
                    TxId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => new { x.Lsid, x.ModifiedDate });
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    StationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StationIdUuidString = table.Column<string>(type: "text", nullable: false),
                    StationIdUuid = table.Column<Guid>(type: "uuid", nullable: false),
                    StationName = table.Column<string>(type: "text", nullable: true),
                    GatewayId = table.Column<int>(type: "integer", nullable: false),
                    GatewayIdHex = table.Column<string>(type: "text", nullable: true),
                    ProductNumber = table.Column<string>(type: "text", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: true),
                    UserEmail = table.Column<string>(type: "text", nullable: true),
                    CompanyName = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Private = table.Column<bool>(type: "boolean", nullable: false),
                    RecordingInterval = table.Column<int>(type: "integer", nullable: false),
                    FirmwareVersion = table.Column<string>(type: "text", nullable: true),
                    RegisteredDate = table.Column<long>(type: "bigint", nullable: false),
                    TimeZone = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Region = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Elevation = table.Column<double>(type: "double precision", nullable: false),
                    GatewayType = table.Column<string>(type: "text", nullable: true),
                    RelationshipType = table.Column<string>(type: "text", nullable: true),
                    SubscriptionType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.StationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stations_StationIdUuid",
                table: "Stations",
                column: "StationIdUuid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "Stations");
        }
    }
}
