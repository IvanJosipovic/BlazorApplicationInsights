using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorApplicationInsights.Models.Context;

/// <summary>
/// Source: https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/Context/ISession.ts
/// </summary>
public class Session
{
    /// <summary>
    /// The session ID.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// The date at which this guid was generated.
    /// Per the spec the ID will be regenerated if more than acquisitionSpan milliseconds elapse from this time.
    /// </summary>
    [JsonPropertyName("acquisitionDate")]
    public long? AcquisitionDate { get; set; }

    /// <summary>
    /// The date at which this session ID was last reported.
    /// This value should be updated whenever telemetry is sent using this ID.
    /// Per the spec the ID will be regenerated if more than renewalSpan milliseconds elapse from this time with no activity.
    /// </summary>
    [JsonPropertyName("renewalDate")]
    public long? RenewalDate { get; set; }
}
