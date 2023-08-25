using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorApplicationInsights.Models
{
    /// <summary>
    /// Pageview telemetry class, extending IPartC.
    /// </summary>
    public class PageViewTelemetry : PartC
    {
        /// <summary>
        /// The string you used as the name in startTrackPage. Defaults to the document title.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// A relative or absolute URL that identifies the page or other item. Defaults to the window location.
        /// </summary>
        [JsonPropertyName("uri")]
        public string? Uri { get; set; }

        /// <summary>
        /// The URL of the source page where the current page is loaded from.
        /// </summary>
        [JsonPropertyName("refUri")]
        public string? RefUri { get; set; }

        /// <summary>
        /// Page type.
        /// </summary>
        [JsonPropertyName("pageType")]
        public string? PageType { get; set; }

        /// <summary>
        /// Boolean indicating whether the user is logged in.
        /// </summary>
        [JsonPropertyName("isLoggedIn")]
        public bool? IsLoggedIn { get; set; }

        /// <summary>
        /// Custom defined iKey.
        /// </summary>
        [JsonPropertyName("iKey")]
        public string? iKey { get; set; }
    }
}