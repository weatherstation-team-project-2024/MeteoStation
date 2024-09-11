using System.Text.Json.Serialization;

namespace api.Models.ResponseModels
{
    public class NodeResponse
    {
        [JsonPropertyName("nodes")]
        public List<Node> Nodes { get; set; } = new List<Node>();

        [JsonPropertyName("generated_at")]
        public long GeneratedAt { get; set; }
    }
}