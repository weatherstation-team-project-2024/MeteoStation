using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.Models
{
    public class Sensor
    {
        [Key]
        public long Lsid { get; set; } 

        public int? SensorType { get; set; } 

        public string? Category { get; set; } 

        public string? Manufacturer { get; set; } 

        public string? ProductName { get; set; } 

        public string? ProductNumber { get; set; } 

        public int? RainCollectorType { get; set; } 

        public bool? Active { get; set; } 

        public long? CreatedDate { get; set; } 

        public long? ModifiedDate { get; set; } 

        public string? StationIdUuid { get; set; } 

        public string? StationName { get; set; } 

        public string? ParentDeviceType { get; set; } 

        public string? ParentDeviceName { get; set; } 

        public long? ParentDeviceId { get; set; } 

        public string? ParentDeviceIdHex { get; set; } 

        public int? PortNumber { get; set; } 

        public double? Latitude { get; set; } 

        public double? Longitude { get; set; } 

        public double? Elevation { get; set; } 

        public string? TxId { get; set; } 

        [JsonIgnore]
        //Foreign key property 
        public int StationId { get; set; }
        public Station Station { get; set; }

        [JsonIgnore]
        public ICollection<WeatherData>? WeatherData { get; set; }
    }
}