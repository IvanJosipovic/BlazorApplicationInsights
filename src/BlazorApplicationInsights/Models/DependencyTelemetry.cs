using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorApplicationInsights.Models
{
    /// <summary>
    /// Dependency Telemetry
    /// Source: https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/IDependencyTelemetry.ts
    /// </summary>
    public class DependencyTelemetry : PartC
    {
        [JsonPropertyName("id")]
        string Id { get; set; }

        [JsonPropertyName("name")]
        string? Name { get; set; }

        [JsonPropertyName("duration")]
        double? Duration { get; set; }

        [JsonPropertyName("success")]
        bool? Success { get; set; }

        [JsonPropertyName("startTime")]
        DateTime? StartTime { get; set; }

        [JsonPropertyName("responseCode")]
        int ResponseCode { get; set; }

        [JsonPropertyName("correlationContext")]
        string? CorrelationContext { get; set; }

        [JsonPropertyName("type")]
        string? Type { get; set; }

        [JsonPropertyName("data")]
        string? Data { get; set; }

        [JsonPropertyName("target")]
        string? Target { get; set; }

        [JsonPropertyName("iKey")]
        string? IKey { get; set; }
    }
}
