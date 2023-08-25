using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorApplicationInsights.Models.Context
{
    /// <summary>
    /// Source: https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/Context/IDevice.ts
    /// </summary>
    public class Device
    {
        /// <summary>
        /// The type for the current device.
        /// </summary>
        [JsonPropertyName("deviceClass")]
        public string DeviceClass { get; set; }

        /// <summary>
        /// A device unique ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The device model for the current device.
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; }

        /// <summary>
        /// The application screen resolution.
        /// </summary>
        [JsonPropertyName("resolution")]
        public string Resolution { get; set; }

        /// <summary>
        /// The IP address.
        /// </summary>
        [JsonPropertyName("ip")]
        public string Ip { get; set; }
    }
}
