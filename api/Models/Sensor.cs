using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using api.Models.Converters;

namespace api.Models
{
    [PrimaryKey(nameof(Lsid), nameof(ModifiedDate))]
    public class Sensor
    {
        [JsonPropertyName("lsid")]
        public int Lsid { get; set; }

        [JsonPropertyName("modified_date")]
        [JsonConverter(typeof(UnixTimestampConverter))]
        public DateTime ModifiedDate { get; set; }

        [JsonPropertyName("sensor_type")]
        public int? SensorType { get; set; }

        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("manufacturer")]
        public string? Manufacturer { get; set; }

        [JsonPropertyName("product_name")]
        public string? ProductName { get; set; }

        [JsonPropertyName("product_number")]
        public string? ProductNumber { get; set; }

        [JsonPropertyName("rain_collector_type")]
        public int? RainCollectorType { get; set; }

        [JsonPropertyName("active")]
        public bool? Active { get; set; }

        [JsonPropertyName("created_date")]
        [JsonConverter(typeof(UnixTimestampConverter))]
        public DateTime CreatedDate { get; set; }

        [JsonPropertyName("station_id")]
        public int StationId { get; set; }

        [JsonPropertyName("station_id_uuid")]
        public string? StationIdUuid { get; set; }

        [JsonPropertyName("station_name")]
        public string? StationName { get; set; }

        [JsonPropertyName("parent_device_type")]
        public string? ParentDeviceType { get; set; }

        [JsonPropertyName("parent_device_name")]
        public string? ParentDeviceName { get; set; }

        [JsonPropertyName("parent_device_id")]
        public long? ParentDeviceId { get; set; }

        [JsonPropertyName("parent_device_id_hex")]
        public string? ParentDeviceIdHex { get; set; }

        [JsonPropertyName("port_number")]
        public int? PortNumber { get; set; }

        [JsonPropertyName("latitude")]
        public double? Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double? Longitude { get; set; }

        [JsonPropertyName("elevation")]
        public double? Elevation { get; set; }

        [JsonPropertyName("tx_id")]
        public int? TxId { get; set; }
    }
}