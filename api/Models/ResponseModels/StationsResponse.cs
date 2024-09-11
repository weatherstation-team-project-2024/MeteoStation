using System.Text.Json.Serialization;

namespace api.Models.ResponseModels
{
    public class StationsResponse
    {
        [JsonPropertyName("stations")]
        public List<Station> Stations { get; set; } = new List<Station>();

        [JsonPropertyName("generated_at")]
        public long GeneratedAt { get; set; }
    }
}