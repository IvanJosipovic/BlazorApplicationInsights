using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorApplicationInsights
{
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
        public string? Name { get; set; }

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
}
