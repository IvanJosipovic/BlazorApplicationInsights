using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BlazorApplicationInsights.Models.Context;
using OperatingSystem = BlazorApplicationInsights.Models.Context.OperatingSystem;

namespace BlazorApplicationInsights.Models;

/// <summary>
/// Source: https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/ITelemetryContext.ts
/// </summary>
public class TelemetryContext
{
    /// <summary>
    /// The object describing a component tracked by this object.
    /// </summary>
    [JsonPropertyName("application")]
    [JsonInclude]
    public Application Application { get; private set; }

    /// <summary>
    /// The object describing a device tracked by this object.
    /// </summary>
    [JsonPropertyName("device")]
    [JsonInclude]
    public Device Device { get; private set; }

    /// <summary>
    /// The object describing internal settings.
    /// </summary>
    [JsonPropertyName("internal")]
    [JsonInclude]
    public Internal Internal { get; private set; }

    /// <summary>
    /// The object describing a location tracked by this object.
    /// </summary>
    [JsonPropertyName("location")]
    [JsonInclude]
    public Location Location { get; private set; }

    /// <summary>
    /// The object describing a operation tracked by this object.
    /// </summary>
    [JsonPropertyName("telemetryTrace")]
    [JsonInclude]
    public TelemetryTrace TelemetryTrace { get; private set; }

    /// <summary>
    /// The object describing a user tracked by this object.
    /// </summary>
    [JsonPropertyName("user")]
    [JsonInclude]
    public UserContext User { get; private set; }

    /// <summary>
    /// The object describing a session tracked by this object.
    /// </summary>
    [JsonPropertyName("session")]
    [JsonInclude]
    public Session Session { get; private set; }

    /// <summary>
    /// The session manager that manages the automatic session from the cookies
    /// </summary>
    [JsonPropertyName("sessionManager")]
    [JsonInclude]
    public SessionManager SessionManager { get; private set; }

    /// <summary>
    /// The object describing os details tracked by this object.
    /// </summary>
    [JsonPropertyName("os")]
    [JsonInclude]
    public OperatingSystem OS { get; private set; }

    /// <summary>
    /// The object describing we details tracked by this object.
    /// </summary>
    [JsonPropertyName("web")]
    [JsonInclude]
    public Web Web { get; private set; }

    /// <summary>
    /// application id obtained from breeze responses. Is used if appId is not specified by root config
    /// </summary>
    public async Task<string> AppId()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    // session id obtained from session manager.
    /// </summary>
    public async Task<string> GetSessionId()
    {
        throw new NotImplementedException();
    }
}
