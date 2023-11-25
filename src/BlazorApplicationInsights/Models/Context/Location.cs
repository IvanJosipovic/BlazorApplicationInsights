using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorApplicationInsights.Models.Context;

/// <summary>
/// Source: https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/Context/ILocation.ts
/// </summary>
public class Location
{
    /// <summary>
    /// Client IP address for reverse lookup.
    /// </summary>
    [JsonPropertyName("ip")]
    public string Ip { get; set; }
}
