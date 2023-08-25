using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorApplicationInsights.Models.Context
{
    /// <summary>
    /// Source: https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/Context/IOperatingSystem.ts
    /// </summary>
    public class OperatingSystem
    {
        /// <summary>
        /// The name of the operating system.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
