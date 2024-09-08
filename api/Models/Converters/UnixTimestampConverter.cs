using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace api.Models.Converters
{
    public class UnixTimestampConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                long unixTimestamp = reader.GetInt64();
                return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).UtcDateTime;
            }
            
            throw new JsonException("Expected unix timestamp (number)");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            long unixTimestamp = ((DateTimeOffset)value.ToUniversalTime()).ToUnixTimeSeconds();
            writer.WriteNumberValue(unixTimestamp);
        }
    }
}