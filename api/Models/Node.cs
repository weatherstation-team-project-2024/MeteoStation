using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.Models
{
    public class Node
    {
        [Key]
        [JsonPropertyName("node_id")]
        public int NodeId { get; set; }

        [JsonPropertyName("node_name")]
        public string? NodeName { get; set; }

        [JsonPropertyName("registered_date")]
        public long RegisteredDate { get; set; }

        [JsonPropertyName("device_id")]
        public int DeviceId { get; set; }

        [JsonPropertyName("device_id_hex")]
        public string? DeviceIdHex { get; set; }

        [JsonPropertyName("firmware_version")]
        public int FirmwareVersion { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("station_id")]
        public int StationId { get; set; }

        private Guid _stationIdUuid;

        [JsonPropertyName("station_id_uuid")]
        public string StationIdUuidString
        {
            get => _stationIdUuid.ToString();
            set
            {
                if (Guid.TryParse(value, out Guid result))
                {
                    _stationIdUuid = result;
                }
                else
                {
                    _stationIdUuid = Guid.NewGuid();
                }
            }
        }

        public Guid StationIdUuid => _stationIdUuid;

        [JsonPropertyName("station_name")]
        public string? StationName { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("elevation")]
        public double Elevation { get; set; }
    }
}