using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorApplicationInsights
{
    /// <summary>
    /// Telemety item
    /// https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCore/src/JavaScriptSDK.Interfaces/ITelemetryItem.ts
    /// </summary>
    public class TelemetryItem
    {
        /// <summary>
        /// CommonSchema Version of this SDK
        /// </summary>
        [JsonPropertyName("ver")]
        public string? Ver { get; set; }

        /// <summary>
        /// Unique name of the telemetry item
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Timestamp when item was sent
        /// </summary>
        [JsonPropertyName("time")]
        public string? Time { get; set; }

        /// <summary>
        /// Identifier of the resource that uniquely identifies which resource data is sent to
        /// </summary>
        [JsonPropertyName("iKey")]
        public string? IKey { get; set; }

        /// <summary>
        /// System context properties of the telemetry item, example: ip address, city etc
        /// </summary>
        [JsonPropertyName("ext")]
        public Dictionary<string, dynamic>? Ext { get; set; }

        /// <summary>
        /// System context property extensions that are not global (not in ctx)
        /// </summary>
        [JsonPropertyName("tags")]
        [JsonConverter(typeof(DictionaryStringObjectJsonConverter))]
        public Dictionary<string, object>? Tags { get; set; }

        /// <summary>
        /// Custom data
        /// </summary>
        [JsonPropertyName("data")]
        public Dictionary<string, dynamic>? Data { get; set; }

        /// <summary>
        /// Telemetry type used for part B
        /// </summary>
        [JsonPropertyName("baseType")]
        public string? BaseType { get; set; }

        /// <summary>
        /// Based on schema for part B
        /// </summary>
        [JsonPropertyName("baseData")]
        public Dictionary<string, dynamic>? BaseData { get; set; }
    }

    // https://josef.codes/custom-dictionary-string-object-jsonconverter-for-system-text-json/
    public class DictionaryStringObjectJsonConverter : JsonConverter<Dictionary<string, object?>>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(Dictionary<string, object>)
                   || typeToConvert == typeof(Dictionary<string, object?>);
        }

        public override Dictionary<string, object?> Read(
            ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject && reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException($"JsonTokenType was of type {reader.TokenType}, only objects are supported");
            }

            var dictionary = new Dictionary<string, object?>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject || reader.TokenType == JsonTokenType.EndArray)
                {
                    return dictionary;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("JsonTokenType was not PropertyName");
                }

                var propertyName = reader.GetString();

                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    throw new JsonException("Failed to get property name");
                }

                reader.Read();

                dictionary.Add(propertyName!, ExtractValue(ref reader, options));
            }

            return dictionary;
        }

        public override void Write(
            Utf8JsonWriter writer, Dictionary<string, object?> value, JsonSerializerOptions options)
        {
            // We don't need any custom serialization logic for writing the json.
            // Ideally, this method should not be called at all. It's only called if you
            // supply JsonSerializerOptions that contains this JsonConverter in it's Converters list.
            // Don't do that, you will lose performance because of the cast needed below.
            // Cast to avoid infinite loop: https://github.com/dotnet/docs/issues/19268
            JsonSerializer.Serialize(writer, (IDictionary<string, object?>)value, options);
        }

        private object? ExtractValue(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    if (reader.TryGetDateTime(out var date))
                    {
                        return date;
                    }
                    return reader.GetString();
                case JsonTokenType.False:
                    return false;
                case JsonTokenType.True:
                    return true;
                case JsonTokenType.Null:
                    return null;
                case JsonTokenType.Number:
                    if (reader.TryGetInt64(out var result))
                    {
                        return result;
                    }
                    return reader.GetDecimal();
                case JsonTokenType.StartObject:
                    return Read(ref reader, null!, options);
                case JsonTokenType.StartArray:
                    var list = new List<object?>();
                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        list.Add(ExtractValue(ref reader, options));
                    }
                    return list;
                default:
                    throw new JsonException($"'{reader.TokenType}' is not supported");
            }
        }
    }
}
