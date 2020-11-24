using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorApplicationInsights
{
    public class TelemetryItem : ITelemetryItem
    {
        [JsonPropertyName("ver")]
        public string? Ver { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("time")]
        public string? Time { get; set; }

        [JsonPropertyName("iKey")]
        public string? IKey { get; set; }

        [JsonPropertyName("ext")]
        public Dictionary<string, object>? Ext { get; set; }

        [JsonPropertyName("tags")]
        public Dictionary<string, object>? Tags { get; set; }

        [JsonPropertyName("data")]
        public Dictionary<string, object>? Data { get; set; }

        [JsonPropertyName("baseType")]
        public string? BaseType { get; set; }

        [JsonPropertyName("baseData")]
        public Dictionary<string, object>? BaseData { get; set; }
    }
}
