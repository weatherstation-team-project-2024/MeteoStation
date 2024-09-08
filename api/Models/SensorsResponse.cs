using System.Text.Json.Serialization;

namespace api.Models
{
    public class SensorsResponse
    {
        [JsonPropertyName("sensors")]
        public List<Sensor> Sensors { get; set; } = new List<Sensor>();

        [JsonPropertyName("generated_at")]
        public long GeneratedAt { get; set; }
    }
}