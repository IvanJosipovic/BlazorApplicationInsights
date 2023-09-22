using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorApplicationInsights.Models;

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
    public string Name { get; set; }

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
    public Dictionary<string, object>? Ext { get; set; }

    /// <summary>
    /// System context property extensions that are not global (not in ctx)
    /// </summary>
    [JsonPropertyName("tags")]
    [JsonConverter(typeof(DictionaryStringObjectJsonConverter<string, object>))]
    public Dictionary<string, object>? Tags { get; set; }

    /// <summary>
    /// Custom data
    /// </summary>
    [JsonPropertyName("data")]
    public Dictionary<string, object>? Data { get; set; }

    /// <summary>
    /// Telemetry type used for part B
    /// </summary>
    [JsonPropertyName("baseType")]
    public string? BaseType { get; set; }

    /// <summary>
    /// Based on schema for part B
    /// </summary>
    [JsonPropertyName("baseData")]
    public Dictionary<string, object>? BaseData { get; set; }
}

/// <summary>
/// This is needed as TelemetryItem.Tags returns a [] when empty
/// https://github.com/dotnet/runtime/blob/96c2e1d099a427a0c7f432c0c1ff7b2ec485b583/src/libraries/System.Text.Json/tests/System.Text.Json.Tests/Serialization/CustomConverterTests/CustomConverterTests.DictionaryKeyValueConverter.cs#L56
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
internal class DictionaryStringObjectJsonConverter<TKey, TValue> : JsonConverter<Dictionary<TKey, TValue>>
{
    private JsonConverter<KeyValuePair<TKey, TValue>> _converter;

    public override Dictionary<TKey, TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject && reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException($"JsonTokenType was of type {reader.TokenType}, only objects are supported");
        }

        _converter ??= (JsonConverter<KeyValuePair<TKey, TValue>>)options.GetConverter(typeof(KeyValuePair<TKey, TValue>));

        var dictionary = new Dictionary<TKey, TValue>();
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject || reader.TokenType == JsonTokenType.EndArray)
            {
                return dictionary;
            }

            var kv = _converter.Read(ref reader, typeof(KeyValuePair<TKey, TValue>), options);

            dictionary.Add(kv.Key, kv.Value);
        }

        return dictionary;
    }

    public override void Write(Utf8JsonWriter writer, Dictionary<TKey, TValue> value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (IDictionary<TKey, TValue>)value, options);
    }
}
