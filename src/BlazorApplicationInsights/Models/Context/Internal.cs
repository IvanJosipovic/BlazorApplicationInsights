using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorApplicationInsights.Models.Context;

/// <summary>
/// Source: https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/Context/IInternal.ts
/// </summary>
public class Internal
{
    /// <summary>
    /// The SDK version used to create this telemetry item.
    /// </summary>
    [JsonPropertyName("sdkVersion")]
    public string SdkVersion { get; set; }

    /// <summary>
    /// The SDK agent version.
    /// </summary>
    [JsonPropertyName("agentVersion")]
    public string AgentVersion { get; set; }

    /// <summary>
    /// The Snippet version used to initialize the sdk instance, this will contain either
    /// undefined/null - Snippet not used
    /// '-' - Version and legacy mode not determined
    /// # - Version # of the snippet
    /// #.l - Version # in legacy mode
    /// .l - No defined version, but used legacy mode initialization
    /// </summary>
    [JsonPropertyName("snippetVer")]
    public string SnippetVer { get; set; }

    /// <summary>
    /// Identifies the source of the sdk script.
    /// </summary>
    [JsonPropertyName("sdkSrc")]
    public string SdkSrc { get; set; }
}
