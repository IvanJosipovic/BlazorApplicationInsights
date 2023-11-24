using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorApplicationInsights.Models;

/// <summary>
/// Application Insights Configuration object
/// Source:
/// https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCore/src/JavaScriptSDK.Interfaces/IConfiguration.ts
/// </summary>
public class Configuration
{
    /// <summary>
    /// Instrumentation key of resource. Either this or connectionString must be specified.
    /// </summary>
    [JsonPropertyName("instrumentationKey")]
    public string? InstrumentationKey { get; set; }

    /// <summary>
    /// Connection string of resource. Either this or instrumentationKey must be specified.
    /// </summary>
    [JsonPropertyName("connectionString")]

    public string? ConnectionString { get; set; }

    /// <summary>
    /// Set the timer interval (in ms) for internal logging queue, this is the
    /// amount of time to wait after logger.queue messages are detected to be sent.
    /// Note: since 3.0.1 and 2.8.13 the diagnostic logger timer is a normal timeout timer
    /// and not an interval timer. So this now represents the timer "delay" and not
    /// the frequency at which the events are sent.
    /// </summary>
    [JsonPropertyName("diagnosticLogInterval")]
    public int? DiagnosticLogInterval { get; set; }

    /// <summary>
    /// Maximum number of iKey transmitted logging telemetry per page view
    /// </summary>
    [JsonPropertyName("maxMessageLimit")]
    public int? MaxMessageLimit { get; set; }

    /// <summary>
    /// Console logging level. All logs with a severity level higher
    /// than the configured level will be printed to console. Otherwise
    /// they are suppressed. ie Level 2 will print both CRITICAL and
    /// WARNING logs to console, level 1 prints only CRITICAL.
    ///
    /// Note: Logs sent as telemetry to instrumentation key will also
    /// be logged to console if their severity meets the configured loggingConsoleLevel
    ///
    /// 0: ALL console logging off
    /// 1: logs to console: severity >= CRITICAL
    /// 2: logs to console: severity >= WARNING
    /// </summary>
    [JsonPropertyName("loggingLevelConsole")]
    public int? LoggingLevelConsole { get; set; }

    /// <summary>
    /// Telemtry logging level to instrumentation key. All logs with a severity
    /// level higher than the configured level will sent as telemetry data to
    /// the configured instrumentation key.
    ///
    /// 0: ALL iKey logging off
    /// 1: logs to iKey: severity >= CRITICAL
    /// 2: logs to iKey: severity >= WARNING
    /// </summary>
    [JsonPropertyName("loggingLevelTelemetry")]
    public int? LoggingLevelTelemetry { get; set; }

    /// <summary>
    /// If enabled, uncaught exceptions will be thrown to help with debugging
    /// </summary>
    [JsonPropertyName("enableDebug")]
    public bool? EnableDebug { get; set; }

    /// <summary>
    /// Endpoint where telemetry data is sent
    /// </summary>
    [JsonPropertyName("endpointUrl")]
    public string? EndpointUrl { get; set; }

    /// <summary>
    /// Extension configs loaded in SDK
    /// </summary>
    [JsonPropertyName("extensionConfig")]
    public Dictionary<string, object>? ExtensionConfig { get; set; }

    /// <summary>
    /// Flag that disables the Instrumentation Key validation.
    /// </summary>
    [JsonPropertyName("disableInstrumentationKeyValidation")]
    public bool? DisableInstrumentationKeyValidation { get; set; }

    /// <summary>
    /// [Optional] Fire every single performance event not just the top level root performance event. Defaults to false.
    /// </summary>
    [JsonPropertyName("perfEvtsSendAll")]
    public bool? PerfEvtsSendAll { get; set; }

    /// <summary>
    /// [Optional] Identifies the default length used to generate random session and user id's if non currently exists for the user / session.
    /// Defaults to 22, previous default value was 5, if you need to keep the previous maximum length you should set this value to 5.
    /// </summary>
    [JsonPropertyName("idLength")]
    public int? IdLength { get; set; }

    /// <summary>
    /// Custom cookie domain. This is helpful if you want to share Application Insights cookies across subdomains.
    /// It can be set here or as part of the cookieCfg.domain, the cookieCfg takes precedence if both are specified.
    /// </summary>
    [JsonPropertyName("cookieDomain")]
    public string? CookieDomain { get; set; }

    /// <summary>
    /// Custom cookie path. This is helpful if you want to share Application Insights cookies behind an application gateway.
    /// It can be set here or as part of the cookieCfg.domain, the cookieCfg takes precedence if both are specified.
    /// </summary>
    [JsonPropertyName("cookiePath")]
    public string? CookiePath { get; set; }

    /// <summary>
    /// A boolean that indicated whether to disable the use of cookies by the SDK. If true, the SDK will not store or
    /// read any data from cookies. Cookie usage can be re-enabled after initialization via the core.getCookieMgr().enable().
    /// </summary>
    [JsonPropertyName("disableCookiesUsage")]
    public bool? DisableCookiesUsage { get; set; }

    /// <summary>
    /// [Optional] An array of the page unload events that you would like to be ignored, special note there must be at least one valid unload
    /// event hooked, if you list all or the runtime environment only supports a listed "disabled" event it will still be hooked, if required by the SDK.
    /// Unload events include "beforeunload", "unload", "visibilitychange" (with 'hidden' state) and "pagehide"
    /// </summary>
    [JsonPropertyName("disablePageUnloadEvents")]
    public string[]? DisablePageUnloadEvents { get; set; }

    /// <summary>
    /// [Optional] An array of page show events that you would like to be ignored, special note there must be at lease one valid show event
    /// hooked, if you list all or the runtime environment only supports a listed (disabled) event it will STILL be hooked, if required by the SDK.
    /// Page Show events include "pageshow" and "visibilitychange" (with 'visible' state)
    /// </summary>
    [JsonPropertyName("disablePageShowEvents")]
    public string[]? DisablePageShowEvents { get; set; }

    /// <summary>
    /// [Optional] A flag for performance optimization to disable attempting to use the Chrome Debug Extension, if disabled and the extension is installed
    /// this will not send any notifications.
    /// </summary>
    [JsonPropertyName("disableDbgExt")]
    public bool? DisableDbgExt { get; set; }

    /// <summary>
    /// Add &quot;&amp;w=0&quot; parameter to support UA Parsing when web-workers don't have access to Document.
    /// Default is false
    /// </summary>
    [JsonPropertyName("enableWParam")]
    public bool? EnableWParam { get; set; }

    /// <summary>
    /// Custom optional value that will be added as a prefix for storage name.
    /// </summary>
    [JsonPropertyName("storagePrefix")]
    public string? StoragePrefix { get; set; }
}
