using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BlazorApplicationInsights.Models.Context;
using OperatingSystem = BlazorApplicationInsights.Models.Context.OperatingSystem;

namespace BlazorApplicationInsights.Models
{
    /// <summary>
    /// Source: https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/ITelemetryContext.ts
    /// </summary>
    public class TelemetryContext
    {
        /// <summary>
        /// The object describing a component tracked by this object.
        /// </summary>
        [JsonPropertyName("application")]
        public Application Application { get; }

        /// <summary>
        /// The object describing a device tracked by this object.
        /// </summary>
        [JsonPropertyName("device")]
        public Device Device { get; }

        /// <summary>
        /// The object describing internal settings.
        /// </summary>
        [JsonPropertyName("internal")]
        public Internal Internal { get; }

        /// <summary>
        /// The object describing a location tracked by this object.
        /// </summary>
        [JsonPropertyName("location")]
        public Location Location { get; }

        /// <summary>
        /// The object describing a operation tracked by this object.
        /// </summary>
        [JsonPropertyName("telemetryTrace")]
        public TelemetryTrace TelemetryTrace { get; }

        /// <summary>
        /// The object describing a user tracked by this object.
        /// </summary>
        [JsonPropertyName("userContext")]
        public UserContext UserContext { get; }

        /// <summary>
        /// The object describing a session tracked by this object.
        /// </summary>
        [JsonPropertyName("session")]
        public Session Session { get; }

        /// <summary>
        /// The session manager that manages the automatic session from the cookies
        /// </summary>
        [JsonPropertyName("sessionManager")]
        public SessionManager SessionManager { get; }

        /// <summary>
        /// The object describing os details tracked by this object.
        /// </summary>
        [JsonPropertyName("operatingSystem")]
        public OperatingSystem OperatingSystem { get; }

        /// <summary>
        /// The object describing we details tracked by this object.
        /// </summary>
        [JsonPropertyName("web")]
        public Web Web { get; }

        /// <summary>
        /// application id obtained from breeze responses. Is used if appId is not specified by root config
        /// </summary>
        public async Task<string> AppId()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// session id obtained from session manager.
        /// </summary>
        public async Task<string> GetSessionId()
        {
            throw new NotImplementedException();
        }
    }
}
