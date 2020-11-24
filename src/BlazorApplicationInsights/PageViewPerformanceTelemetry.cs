using System.Text.Json.Serialization;

namespace BlazorApplicationInsights
{
    class PageViewPerformanceTelemetry : IPageViewPerformanceTelemetry
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("uri")]
        public string? Uri { get; set; }

        [JsonPropertyName("perfTotal")]
        public string? PerfTotal { get; set; }

        [JsonPropertyName("duration")]
        public string? Duration { get; set; }

        [JsonPropertyName("networkConnect")]
        public string? NetworkConnect { get; set; }

        [JsonPropertyName("sentRequest")]
        public string? SentRequest { get; set; }

        [JsonPropertyName("receivedResponse")]
        public string? ReceivedResponse { get; set; }

        [JsonPropertyName("domProcessing")]
        public string? DomProcessing { get; set; }
    }
}
