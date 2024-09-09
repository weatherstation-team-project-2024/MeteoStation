using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using api.Models.Converters;

namespace api.Models
{
    [PrimaryKey(nameof(Lsid), nameof(Time))]
    public class WeatherData
    {
        [JsonPropertyName("lsid")]
        public int Lsid { get; set; }

        [JsonPropertyName("ts")]
        [JsonConverter(typeof(UnixTimestampConverter))]
        public DateTime Time { get; set; }

        [JsonPropertyName("hum")]
        public double? Humidity { get; set; }

        [JsonPropertyName("temp")]
        public double? Temperature { get; set; }

        [JsonPropertyName("pm_2p5")]
        public double? Pm2p5 { get; set; }
    }
}
