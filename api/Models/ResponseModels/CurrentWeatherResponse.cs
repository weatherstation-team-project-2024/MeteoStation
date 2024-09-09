using System.Text.Json.Serialization;

namespace api.Models
{
    public class CurrentWeatherResponse
    {
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

        [JsonPropertyName("sensors")]
        public List<WeatherSensorsResponse> SensorList { get; set; } = new List<WeatherSensorsResponse>();

        [JsonPropertyName("generated_at")]
        public long GeneratedAt { get; set; }

        [JsonPropertyName("station_id")]
        public int StationId { get; set; }
    }
}
