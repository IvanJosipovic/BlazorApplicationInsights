using System.Text.Json.Serialization;

namespace BlazorApplicationInsights.Models
{
    /// <summary>
    /// Source: https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/IEventTelemetry.ts
    /// </summary>
    public class EventTelemetry : PartC
    {
        /// <summary>
        /// An event name string.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Custom defined iKey.
        /// </summary>
        [JsonPropertyName("iKey")]
        public string? iKey { get; set; }
    }
}