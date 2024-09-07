using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class V6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stations_WeatherStations_WeatherStationsGeneratedAt",
                table: "Stations");

            migrationBuilder.RenameColumn(
                name: "WeatherStationsGeneratedAt",
                table: "Stations",
                newName: "WeatherStationGeneratedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Stations_WeatherStationsGeneratedAt",
                table: "Stations",
                newName: "IX_Stations_WeatherStationGeneratedAt");

            migrationBuilder.CreateTable(
                name: "Nodes",
                columns: table => new
                {
                    NodeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NodeName = table.Column<string>(type: "text", nullable: true),
                    RegisteredDate = table.Column<long>(type: "bigint", nullable: true),
                    DeviceId = table.Column<long>(type: "bigint", nullable: true),
                    DeviceIdHex = table.Column<string>(type: "text", nullable: true),
                    FirmwareVersion = table.Column<int>(type: "integer", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: true),
                    StationIdUuid = table.Column<string>(type: "text", nullable: true),
                    StationName = table.Column<string>(type: "text", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: true),
                    Longitude = table.Column<double>(type: "double precision", nullable: true),
                    Elevation = table.Column<double>(type: "double precision", nullable: true),
                    StationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => x.NodeId);
                    table.ForeignKey(
                        name: "FK_Nodes_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Lsid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SensorType = table.Column<int>(type: "integer", nullable: true),
                    Category = table.Column<string>(type: "text", nullable: true),
                    Manufacturer = table.Column<string>(type: "text", nullable: true),
                    ProductName = table.Column<string>(type: "text", nullable: true),
                    ProductNumber = table.Column<string>(type: "text", nullable: true),
                    RainCollectorType = table.Column<int>(type: "integer", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedDate = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<long>(type: "bigint", nullable: true),
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
                    TxId = table.Column<string>(type: "text", nullable: true),
                    StationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Lsid);
                    table.ForeignKey(
                        name: "FK_Sensors_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeatherData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TzOffset = table.Column<int>(type: "integer", nullable: true),
                    Ts = table.Column<long>(type: "bigint", nullable: true),
                    TempIn = table.Column<double>(type: "double precision", nullable: true),
                    HeatIndexIn = table.Column<double>(type: "double precision", nullable: true),
                    DewPointIn = table.Column<double>(type: "double precision", nullable: true),
                    HumIn = table.Column<double>(type: "double precision", nullable: true),
                    BarAbsolute = table.Column<double>(type: "double precision", nullable: true),
                    BarSeaLevel = table.Column<double>(type: "double precision", nullable: true),
                    BarOffset = table.Column<double>(type: "double precision", nullable: true),
                    BarTrend = table.Column<double>(type: "double precision", nullable: true),
                    BatteryVoltage = table.Column<int>(type: "integer", nullable: true),
                    WifiRssi = table.Column<int>(type: "integer", nullable: true),
                    NetworkError = table.Column<string>(type: "text", nullable: true),
                    IpV4Gateway = table.Column<string>(type: "text", nullable: true),
                    BluetoothVersion = table.Column<string>(type: "text", nullable: true),
                    Bgn = table.Column<string>(type: "text", nullable: true),
                    FirmwareVersion = table.Column<long>(type: "bigint", nullable: true),
                    LocalApiQueries = table.Column<int>(type: "integer", nullable: true),
                    RxBytes = table.Column<long>(type: "bigint", nullable: true),
                    HealthVersion = table.Column<int>(type: "integer", nullable: true),
                    RadioVersion = table.Column<long>(type: "bigint", nullable: true),
                    IpAddressType = table.Column<int>(type: "integer", nullable: true),
                    LinkUptime = table.Column<long>(type: "bigint", nullable: true),
                    InputVoltage = table.Column<int>(type: "integer", nullable: true),
                    TxBytes = table.Column<long>(type: "bigint", nullable: true),
                    IpV4Netmask = table.Column<string>(type: "text", nullable: true),
                    RapidRecordsSent = table.Column<int>(type: "integer", nullable: true),
                    Uptime = table.Column<long>(type: "bigint", nullable: true),
                    TouchpadWakeups = table.Column<int>(type: "integer", nullable: true),
                    IpV4Address = table.Column<string>(type: "text", nullable: true),
                    BootloaderVersion = table.Column<long>(type: "bigint", nullable: true),
                    EspressifVersion = table.Column<long>(type: "bigint", nullable: true),
                    DnsTypeUsed = table.Column<string>(type: "text", nullable: true),
                    NetworkType = table.Column<int>(type: "integer", nullable: true),
                    RxState = table.Column<int>(type: "integer", nullable: true),
                    WindSpeedHiLast2Min = table.Column<double>(type: "double precision", nullable: true),
                    Hum = table.Column<double>(type: "double precision", nullable: true),
                    WindDirAtHiSpeedLast10Min = table.Column<double>(type: "double precision", nullable: true),
                    WindChill = table.Column<double>(type: "double precision", nullable: true),
                    RainRateHiLast15MinClicks = table.Column<double>(type: "double precision", nullable: true),
                    ThwIndex = table.Column<double>(type: "double precision", nullable: true),
                    WindDirScalarAvgLast10Min = table.Column<double>(type: "double precision", nullable: true),
                    RainSize = table.Column<int>(type: "integer", nullable: true),
                    UvIndex = table.Column<double>(type: "double precision", nullable: true),
                    WindSpeedLast = table.Column<double>(type: "double precision", nullable: true),
                    RainfallLast60MinClicks = table.Column<double>(type: "double precision", nullable: true),
                    WetBulb = table.Column<double>(type: "double precision", nullable: true),
                    RainfallMonthlyClicks = table.Column<int>(type: "integer", nullable: true),
                    WindSpeedAvgLast10Min = table.Column<double>(type: "double precision", nullable: true),
                    WindDirAtHiSpeedLast2Min = table.Column<double>(type: "double precision", nullable: true),
                    RainfallDailyIn = table.Column<double>(type: "double precision", nullable: true),
                    WindDirLast = table.Column<double>(type: "double precision", nullable: true),
                    RainfallDailyMm = table.Column<double>(type: "double precision", nullable: true),
                    RainStormLastClicks = table.Column<int>(type: "integer", nullable: true),
                    TxId = table.Column<int>(type: "integer", nullable: true),
                    RainStormLastStartAt = table.Column<long>(type: "bigint", nullable: true),
                    RainRateHiClicks = table.Column<double>(type: "double precision", nullable: true),
                    RainfallLast15MinIn = table.Column<double>(type: "double precision", nullable: true),
                    RainfallDailyClicks = table.Column<int>(type: "integer", nullable: true),
                    RainfallLast15MinMm = table.Column<double>(type: "double precision", nullable: true),
                    RainRateHiIn = table.Column<double>(type: "double precision", nullable: true),
                    RainStormClicks = table.Column<int>(type: "integer", nullable: true),
                    RainRateHiMm = table.Column<double>(type: "double precision", nullable: true),
                    RainfallYearClicks = table.Column<int>(type: "integer", nullable: true),
                    RainStormIn = table.Column<double>(type: "double precision", nullable: true),
                    RainStormLastEndAt = table.Column<long>(type: "bigint", nullable: true),
                    RainStormMm = table.Column<double>(type: "double precision", nullable: true),
                    WindDirScalarAvgLast2Min = table.Column<double>(type: "double precision", nullable: true),
                    HeatIndex = table.Column<double>(type: "double precision", nullable: true),
                    RainfallLast24HrIn = table.Column<double>(type: "double precision", nullable: true),
                    RainfallLast60MinMm = table.Column<double>(type: "double precision", nullable: true),
                    TransBatteryFlag = table.Column<int>(type: "integer", nullable: true),
                    RainfallLast60MinIn = table.Column<double>(type: "double precision", nullable: true),
                    RainStormStartTime = table.Column<string>(type: "text", nullable: true),
                    RainfallLast24HrMm = table.Column<double>(type: "double precision", nullable: true),
                    RainfallYearIn = table.Column<double>(type: "double precision", nullable: true),
                    WindSpeedHiLast10Min = table.Column<double>(type: "double precision", nullable: true),
                    RainfallLast15MinClicks = table.Column<double>(type: "double precision", nullable: true),
                    RainfallYearMm = table.Column<double>(type: "double precision", nullable: true),
                    WindDirScalarAvgLast1Min = table.Column<double>(type: "double precision", nullable: true),
                    Temp = table.Column<double>(type: "double precision", nullable: true),
                    WindSpeedAvgLast2Min = table.Column<double>(type: "double precision", nullable: true),
                    SolarRad = table.Column<double>(type: "double precision", nullable: true),
                    RainfallMonthlyMm = table.Column<double>(type: "double precision", nullable: true),
                    RainStormLastMm = table.Column<double>(type: "double precision", nullable: true),
                    WindSpeedAvgLast1Min = table.Column<double>(type: "double precision", nullable: true),
                    ThswIndex = table.Column<double>(type: "double precision", nullable: true),
                    RainfallMonthlyIn = table.Column<double>(type: "double precision", nullable: true),
                    RainRateLastMm = table.Column<double>(type: "double precision", nullable: true),
                    RainRateLastClicks = table.Column<double>(type: "double precision", nullable: true),
                    RainfallLast24HrClicks = table.Column<double>(type: "double precision", nullable: true),
                    RainStormLastIn = table.Column<double>(type: "double precision", nullable: true),
                    RainRateLastIn = table.Column<double>(type: "double precision", nullable: true),
                    RainRateHiLast15MinMm = table.Column<double>(type: "double precision", nullable: true),
                    RainRateHiLast15MinIn = table.Column<double>(type: "double precision", nullable: true),
                    Pm10_3Hour = table.Column<double>(type: "double precision", nullable: true),
                    Pm10_24Hour = table.Column<double>(type: "double precision", nullable: true),
                    Pm2p5_1Hour = table.Column<double>(type: "double precision", nullable: true),
                    AqiNowcastVal = table.Column<double>(type: "double precision", nullable: true),
                    AqiType = table.Column<string>(type: "text", nullable: true),
                    Pm2p5Nowcast = table.Column<double>(type: "double precision", nullable: true),
                    Pm2p5_24Hour = table.Column<double>(type: "double precision", nullable: true),
                    Pm1 = table.Column<double>(type: "double precision", nullable: true),
                    PctPmDataNowcast = table.Column<int>(type: "integer", nullable: true),
                    PctPmData24Hour = table.Column<int>(type: "integer", nullable: true),
                    AqiVal = table.Column<double>(type: "double precision", nullable: true),
                    AqiDesc = table.Column<string>(type: "text", nullable: true),
                    Pm2p5_3Hour = table.Column<double>(type: "double precision", nullable: true),
                    PctPmData3Hour = table.Column<int>(type: "integer", nullable: true),
                    LastReportTime = table.Column<long>(type: "bigint", nullable: true),
                    AqiNowcastDesc = table.Column<string>(type: "text", nullable: true),
                    Aqi1HourVal = table.Column<double>(type: "double precision", nullable: true),
                    Pm10Nowcast = table.Column<double>(type: "double precision", nullable: true),
                    Aqi1HourDesc = table.Column<string>(type: "text", nullable: true),
                    Pm10_1Hour = table.Column<double>(type: "double precision", nullable: true),
                    Pm10 = table.Column<double>(type: "double precision", nullable: true),
                    PctPmData1Hour = table.Column<int>(type: "integer", nullable: true),
                    Pm2p5 = table.Column<double>(type: "double precision", nullable: true),
                    SensorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherData_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensors",
                        principalColumn: "Lsid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_StationId",
                table: "Nodes",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_StationId",
                table: "Sensors",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherData_SensorId",
                table: "WeatherData",
                column: "SensorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_WeatherStations_WeatherStationGeneratedAt",
                table: "Stations",
                column: "WeatherStationGeneratedAt",
                principalTable: "WeatherStations",
                principalColumn: "GeneratedAt",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stations_WeatherStations_WeatherStationGeneratedAt",
                table: "Stations");

            migrationBuilder.DropTable(
                name: "Nodes");

            migrationBuilder.DropTable(
                name: "WeatherData");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.RenameColumn(
                name: "WeatherStationGeneratedAt",
                table: "Stations",
                newName: "WeatherStationsGeneratedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Stations_WeatherStationGeneratedAt",
                table: "Stations",
                newName: "IX_Stations_WeatherStationsGeneratedAt");

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_WeatherStations_WeatherStationsGeneratedAt",
                table: "Stations",
                column: "WeatherStationsGeneratedAt",
                principalTable: "WeatherStations",
                principalColumn: "GeneratedAt",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
