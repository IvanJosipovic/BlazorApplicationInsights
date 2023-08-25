using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorApplicationInsights.Models
{
    /// <summary>
    /// PartC telemetry class.
    /// Source: https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/IPartC.ts
    /// </summary>
    public abstract class PartC
    {
        /// <summary>
        /// Property bag to contain additional custom properties (Part C).
        /// </summary>
        [JsonPropertyName("properties")]
        public Dictionary<string, object> Properties { get; set; }
    }
}
