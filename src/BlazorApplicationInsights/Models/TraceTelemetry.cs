using System.Text.Json.Serialization;

namespace BlazorApplicationInsights.Models
{
    public class TraceTelemetry : PartC
    {
        /// <summary>
        /// A message string
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// Severity level of the logging message used for filtering in the portal
        /// </summary>
        [JsonPropertyName("severityLevel")]
        public SeverityLevel? SeverityLevel { get; set; }

        /// <summary>
        /// Custom defined iKey
        /// </summary>
        [JsonPropertyName("iKey")]
        public string? IKey { get; set; }
    }
}