using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.Models
{
    public class Node
    {
        [Key]
        public int NodeId { get; set; } 
        public string? NodeName { get; set; } 

        public long? RegisteredDate { get; set; } 

        public long? DeviceId { get; set; } 

        public string? DeviceIdHex { get; set; } 

        public int? FirmwareVersion { get; set; } 

        public bool? Active { get; set; } 

        public string? StationIdUuid { get; set; } 

        public string? StationName { get; set; } 

        public double? Latitude { get; set; } 

        public double? Longitude { get; set; } 

        public double? Elevation { get; set; } 

        [JsonIgnore]
        //Foreign key 
        public int StationId { get; set; } 
        public Station Station { get; set; }
    }
}