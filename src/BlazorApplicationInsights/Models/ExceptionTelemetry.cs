using System.Text.Json.Serialization;

namespace BlazorApplicationInsights.Models;

/// <summary>
/// Exception interface used as primary parameter to trackException
/// Source: https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/IExceptionTelemetry.ts
/// </summary>
public class ExceptionTelemetry : PartC
{
    /// <summary>
    /// Unique guid identifying this error.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Error Object(s).
    /// </summary>
    [JsonPropertyName("exception")]
    public Error? Exception { get; set; }

    /// <summary>
    /// Specified severity of exception for use with telemetry filtering in dashboard.
    /// </summary>
    [JsonPropertyName("severityLevel")]
    public SeverityLevel? SeverityLevel { get; set; }
}