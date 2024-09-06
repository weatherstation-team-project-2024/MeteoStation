using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.Models
{
    public class Station
    {
        [Key]
        public int StationId { get; set; }
        public string? StationIdUuid { get; set; }
        public string? StationName { get; set; }
        public int? GatewayId { get; set; }
        public string? GatewayIdHex { get; set; }
        public string? ProductNumber { get; set; }
        public string? Username { get; set; }
        public string? UserEmail { get; set; }
        public string? CompanyName { get; set; }
        public bool? Active { get; set; }
        public bool? Private { get; set; }
        public int? RecordingInterval { get; set; }
        public string? FirmwareVersion { get; set; }
        public int? RegisteredDate { get; set; } 
        public string? TimeZone { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Country { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Elevation { get; set; }
        public string? GatewayType { get; set; }
        public string? RelationshipType { get; set; }
        public string? SubscriptionType { get; set; }
        [JsonIgnore]
        public ICollection<Sensor>? Sensors { get; set; }
        [JsonIgnore]
        public ICollection<Node>? Nodes { get; set; }
    }
}