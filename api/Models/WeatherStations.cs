using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.Models
{
    public class WeatherStations
    {
        [Key]
        [JsonPropertyName("generated_at")]
        public long GeneratedAt { get; set; }
        public List<Station>? Stations { get; set; } = new List<Station>();
    }
}