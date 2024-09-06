using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.Models
{
    public class WeatherData
    {
        [Key]
        public long Id { get; set; }
        public int? TzOffset { get; set; }

        public long? Ts { get; set; }

        public double? TempIn { get; set; }

        public double? HeatIndexIn { get; set; }

        public double? DewPointIn { get; set; }

        public double? HumIn { get; set; }

        public double? BarAbsolute { get; set; }

        public double? BarSeaLevel { get; set; }

        public double? BarOffset { get; set; }

        public double? BarTrend { get; set; }

        public int? BatteryVoltage { get; set; }

        public int? WifiRssi { get; set; }

        public string? NetworkError { get; set; }

        public string? IpV4Gateway { get; set; }

        public string? BluetoothVersion { get; set; }

        public string? Bgn { get; set; }

        public long? FirmwareVersion { get; set; }

        public int? LocalApiQueries { get; set; }

        public long? RxBytes { get; set; }

        public int? HealthVersion { get; set; }

        public long? RadioVersion { get; set; }

        public int? IpAddressType { get; set; }

        public long? LinkUptime { get; set; }

        public int? InputVoltage { get; set; }

        public long? TxBytes { get; set; }

        public string? IpV4Netmask { get; set; }

        public int? RapidRecordsSent { get; set; }

        public long? Uptime { get; set; }

        public int? TouchpadWakeups { get; set; }

        public string? IpV4Address { get; set; }

        public long? BootloaderVersion { get; set; }

        public long? EspressifVersion { get; set; }

        public string? DnsTypeUsed { get; set; }

        public int? NetworkType { get; set; }

        public int? RxState { get; set; }

        public double? WindSpeedHiLast2Min { get; set; }

        public double? Hum { get; set; }

        public double? WindDirAtHiSpeedLast10Min { get; set; }

        public double? WindChill { get; set; }

        public double? RainRateHiLast15MinClicks { get; set; }

        public double? ThwIndex { get; set; }

        public double? WindDirScalarAvgLast10Min { get; set; }

        public int? RainSize { get; set; }

        public double? UvIndex { get; set; }

        public double? WindSpeedLast { get; set; }

        public double? RainfallLast60MinClicks { get; set; }

        public double? WetBulb { get; set; }

        public int? RainfallMonthlyClicks { get; set; }

        public double? WindSpeedAvgLast10Min { get; set; }

        public double? WindDirAtHiSpeedLast2Min { get; set; }

        public double? RainfallDailyIn { get; set; }

        public double? WindDirLast { get; set; }

        public double? RainfallDailyMm { get; set; }

        public int? RainStormLastClicks { get; set; }

        public int? TxId { get; set; }

        public long? RainStormLastStartAt { get; set; }

        public double? RainRateHiClicks { get; set; }

        public double? RainfallLast15MinIn { get; set; }

        public int? RainfallDailyClicks { get; set; }

        public double? RainfallLast15MinMm { get; set; }

        public double? RainRateHiIn { get; set; }

        public int? RainStormClicks { get; set; }

        public double? RainRateHiMm { get; set; }

        public int? RainfallYearClicks { get; set; }

        public double? RainStormIn { get; set; }

        public long? RainStormLastEndAt { get; set; }

        public double? RainStormMm { get; set; }

        public double? WindDirScalarAvgLast2Min { get; set; }

        public double? HeatIndex { get; set; }

        public double? RainfallLast24HrIn { get; set; }

        public double? RainfallLast60MinMm { get; set; }

        public int? TransBatteryFlag { get; set; }

        public double? RainfallLast60MinIn { get; set; }

        public string? RainStormStartTime { get; set; }

        public double? RainfallLast24HrMm { get; set; }

        public double? RainfallYearIn { get; set; }

        public double? WindSpeedHiLast10Min { get; set; }

        public double? RainfallLast15MinClicks { get; set; }

        public double? RainfallYearMm { get; set; }

        public double? WindDirScalarAvgLast1Min { get; set; }

        public double? Temp { get; set; }

        public double? WindSpeedAvgLast2Min { get; set; }

        public double? SolarRad { get; set; }

        public double? RainfallMonthlyMm { get; set; }

        public double? RainStormLastMm { get; set; }

        public double? WindSpeedAvgLast1Min { get; set; }

        public double? ThswIndex { get; set; }

        public double? RainfallMonthlyIn { get; set; }

        public double? RainRateLastMm { get; set; }

        public double? RainRateLastClicks { get; set; }

        public double? RainfallLast24HrClicks { get; set; }

        public double? RainStormLastIn { get; set; }

        public double? RainRateLastIn { get; set; }

        public double? RainRateHiLast15MinMm { get; set; }

        public double? RainRateHiLast15MinIn { get; set; }

        public double? Pm10_3Hour { get; set; }

        public double? Pm10_24Hour { get; set; }

        public double? Pm2p5_1Hour { get; set; }

        public double? AqiNowcastVal { get; set; }

        public string? AqiType { get; set; }

        public double? Pm2p5Nowcast { get; set; }

        public double? Pm2p5_24Hour { get; set; }

        public double? Pm1 { get; set; }

        public int? PctPmDataNowcast { get; set; }

        public int? PctPmData24Hour { get; set; }

        public double? AqiVal { get; set; }

        public string? AqiDesc { get; set; }

        public double? Pm2p5_3Hour { get; set; }

        public int? PctPmData3Hour { get; set; }

        public long? LastReportTime { get; set; }

        public string? AqiNowcastDesc { get; set; }

        public double? Aqi1HourVal { get; set; }

        public double? Pm10Nowcast { get; set; }

        public string? Aqi1HourDesc { get; set; }

        public double? Pm10_1Hour { get; set; }

        public double? Pm10 { get; set; }

        public int? PctPmData1Hour { get; set; }

        public double? Pm2p5 { get; set; }

        [JsonIgnore]
        //Foreign key property 
        public int SensorId { get; set; }
        public Sensor Sensor { get; set; }
    }
}