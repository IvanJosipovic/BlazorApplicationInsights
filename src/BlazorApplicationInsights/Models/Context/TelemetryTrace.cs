using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorApplicationInsights.Models.Context;

/// <summary>
/// Source: https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/Context/ITelemetryTrace.ts
/// </summary>
public class TelemetryTrace
{
    /// <summary>
    /// Trace id.
    /// </summary>
    [JsonPropertyName("traceID")]
    public string? TraceID { get; set; }

    /// <summary>
    /// Parent id.
    /// </summary>
    [JsonPropertyName("parentID")]
    public string? ParentID { get; set; }

    /// <summary>
    /// An integer representation of the W3C TraceContext trace-flags. https://www.w3.org/TR/trace-context/#trace-flags
    /// </summary>
    [JsonPropertyName("traceFlags")]
    public int? TraceFlags { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
