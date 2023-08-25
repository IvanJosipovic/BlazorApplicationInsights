using System.Text.Json.Serialization;

namespace BlazorApplicationInsights.Models
{
    /// <summary>
    /// Source: https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/IPageViewPerformanceTelemetry.ts
    /// </summary>
    public class PageViewPerformanceTelemetry : PartC
    {
        /// <summary>
        /// The name of the page. Defaults to the document title.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// A relative or absolute URL that identifies the page or other item. Defaults to the window location.
        /// </summary>
        [JsonPropertyName("uri")]
        public string? Uri { get; set; }

        /// <summary>
        /// Performance total in TimeSpan 'G' (general long) format: d:hh:mm:ss.fffffff". This is total duration in timespan format.
        /// </summary>
        [JsonPropertyName("perfTotal")]
        public string? PerfTotal { get; set; }

        /// <summary>
        /// Performance total in TimeSpan 'G' (general long) format: d:hh:mm:ss.fffffff". This represents the total page load time.
        /// </summary>
        [JsonPropertyName("duration")]
        public string? Duration { get; set; }

        /// <summary>
        /// Sent request time in TimeSpan 'G' (general long) format: d:hh:mm:ss.fffffff.
        /// </summary>
        [JsonPropertyName("networkConnect")]
        public string? NetworkConnect { get; set; }

        /// <summary>
        /// Sent request time in TimeSpan 'G' (general long) format: d:hh:mm:ss.fffffff
        /// </summary>
        [JsonPropertyName("sentRequest")]
        public string? SentRequest { get; set; }

        /// <summary>
        /// Received response time in TimeSpan 'G' (general long) format: d:hh:mm:ss.fffffff.
        /// </summary>
        [JsonPropertyName("receivedResponse")]
        public string? ReceivedResponse { get; set; }

        /// <summary>
        /// DOM processing time in TimeSpan 'G' (general long) format: d:hh:mm:ss.fffffff
        /// </summary>
        [JsonPropertyName("domProcessing")]
        public string? DomProcessing { get; set; }
    }
}
