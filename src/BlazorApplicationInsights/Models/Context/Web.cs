using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorApplicationInsights.Models.Context
{
    /// <summary>
    /// Source: https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/Context/IWeb.ts
    /// </summary>
    public class Web
    {
        /// <summary>
        /// Browser name, set at ingestion.
        /// </summary>
        [JsonPropertyName("browser")]
        public string Browser { get; set; }

        /// <summary>
        /// Browser version, set at ingestion.
        /// </summary>
        [JsonPropertyName("browserVer")]
        public string BrowserVer { get; set; }

        /// <summary>
        /// Language.
        /// </summary>
        [JsonPropertyName("browserLang")]
        public string BrowserLang { get; set; }

        /// <summary>
        /// User consent, populated to properties bag.
        /// </summary>
        [JsonPropertyName("userConsent")]
        public bool UserConsent { get; set; }

        /// <summary>
        /// Whether the event was fired manually, populated to properties bag.
        /// </summary>
        [JsonPropertyName("isManual")]
        public bool IsManual { get; set; }

        /// <summary>
        /// Screen resolution, populated to properties bag.
        /// </summary>
        [JsonPropertyName("screenRes")]
        public string ScreenRes { get; set; }

        /// <summary>
        /// Current domain. Leverages Window.location.hostname. Populated to properties bag.
        /// </summary>
        [JsonPropertyName("domain")]
        public string Domain { get; set; }
    }
}
