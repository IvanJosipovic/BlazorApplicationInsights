using System;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorApplicationInsights.Models;

/// <summary>
/// Dependency Telemetry
/// Source: https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/IDependencyTelemetry.ts
/// </summary>
public class DependencyTelemetry : PartC
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("duration")]
    public double? Duration { get; set; }

    [JsonPropertyName("success")]
    public bool? Success { get; set; }

    [JsonPropertyName("startTime")]
    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime? StartTime { get; set; }

    [JsonPropertyName("responseCode")]
    public int ResponseCode { get; set; }

    [JsonPropertyName("correlationContext")]
    public string? CorrelationContext { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("data")]
    public string? Data { get; set; }

    [JsonPropertyName("target")]
    public string? Target { get; set; }

    [JsonPropertyName("iKey")]
    public string? IKey { get; set; }
}

[EditorBrowsable(EditorBrowsableState.Never)]
[Browsable(false)]
public class DateTimeJsonConverter : JsonConverter<DateTime>
{
    private JsonConverter<DateTime> _converter;

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        _converter ??= (JsonConverter<DateTime>)options.GetConverter(typeof(DateTime));

        return _converter.Read(ref reader, typeof(DateOnly), options);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue((decimal)(new DateTimeOffset(value)).ToUnixTimeMilliseconds());
    }
}