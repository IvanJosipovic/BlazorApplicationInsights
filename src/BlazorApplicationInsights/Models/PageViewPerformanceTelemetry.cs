using System.ComponentModel;
using System.Text.Json;
using System;
using System.Text.Json.Serialization;

namespace BlazorApplicationInsights.Models;

/// <summary>
/// Source: https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/IPageViewPerformanceTelemetry.ts
/// </summary>
public class PageViewPerformanceTelemetry : PartC
{
    /// <summary>
    /// The name of the page. Defaults to the document title.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// A relative or absolute URL that identifies the page or other item. Defaults to the window location.
    /// </summary>
    [JsonPropertyName("uri")]
    public string? Uri { get; set; }

    /// <summary>
    /// Performance total. This is total duration in timespan format.
    /// </summary>
    [JsonPropertyName("perfTotal")]
    [JsonConverter(typeof(TimeSpanJsonConverter))]
    public TimeSpan? PerfTotal { get; set; }

    /// <summary>
    /// Performance total. This represents the total page load time.
    /// </summary>
    [JsonPropertyName("duration")]
    [JsonConverter(typeof(TimeSpanJsonConverter))]
    public TimeSpan? Duration { get; set; }

    /// <summary>
    /// Sent request time.
    /// </summary>
    [JsonPropertyName("networkConnect")]
    [JsonConverter(typeof(TimeSpanJsonConverter))]
    public TimeSpan? NetworkConnect { get; set; }

    /// <summary>
    /// Sent request time.
    /// </summary>
    [JsonPropertyName("sentRequest")]
    [JsonConverter(typeof(TimeSpanJsonConverter))]
    public TimeSpan? SentRequest { get; set; }

    /// <summary>
    /// Received response time.
    /// </summary>
    [JsonPropertyName("receivedResponse")]
    [JsonConverter(typeof(TimeSpanJsonConverter))]
    public TimeSpan? ReceivedResponse { get; set; }

    /// <summary>
    /// DOM processing time.
    /// </summary>
    [JsonPropertyName("domProcessing")]
    [JsonConverter(typeof(TimeSpanJsonConverter))]
    public TimeSpan? DomProcessing { get; set; }
}

[EditorBrowsable(EditorBrowsableState.Never)]
[Browsable(false)]
public class TimeSpanJsonConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        //d:hh:mm:ss.fffffff
        //@"d\:hh\:mm\:ss\.fffffff"
        writer.WriteStringValue(value.ToString("G"));
    }
}