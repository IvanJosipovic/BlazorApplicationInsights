using System.Text.Json.Serialization;

namespace BlazorApplicationInsights.Models
{
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