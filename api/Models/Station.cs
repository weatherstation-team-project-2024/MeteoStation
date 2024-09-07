using System;
using System.Text.Json.Serialization;

namespace api.Models
{
    public class Station
    {
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
                    // If parsing fails, generate a new Guid
                    _stationIdUuid = Guid.NewGuid();
                }
            }
        }

        public Guid StationIdUuid => _stationIdUuid;

        [JsonPropertyName("station_name")]
        public string? StationName { get; set; }

        [JsonPropertyName("gateway_id")]
        public int GatewayId { get; set; }

        [JsonPropertyName("gateway_id_hex")]
        public string? GatewayIdHex { get; set; }

        [JsonPropertyName("product_number")]
        public string? ProductNumber { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("user_email")]
        public string? UserEmail { get; set; }

        [JsonPropertyName("company_name")]
        public string? CompanyName { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("private")]
        public bool Private { get; set; }

        [JsonPropertyName("recording_interval")]
        public int RecordingInterval { get; set; }

        [JsonPropertyName("firmware_version")]
        public string? FirmwareVersion { get; set; }

        [JsonPropertyName("registered_date")]
        public long RegisteredDate { get; set; }

        [JsonPropertyName("time_zone")]
        public string? TimeZone { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("region")]
        public string? Region { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("elevation")]
        public double Elevation { get; set; }

        [JsonPropertyName("gateway_type")]
        public string? GatewayType { get; set; }

        [JsonPropertyName("relationship_type")]
        public string? RelationshipType { get; set; }

        [JsonPropertyName("subscription_type")]
        public string? SubscriptionType { get; set; }

        public List<Sensor> Sensors { get; set; } = new List<Sensor>();
        public List<Node> Nodes { get; set; } = new List<Node>();

        //Foreign key property 
        public long GeneratedAt { get; set; }
        public WeatherStations WeatherStation { get; set; }
    }
}