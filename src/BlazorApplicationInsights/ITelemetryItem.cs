using System.Collections.Generic;

namespace BlazorApplicationInsights
{
    public interface ITelemetryItem
    {
        /// <summary>
        /// CommonSchema Version of this SDK
        /// </summary>
        public string? Ver { get; set; }

        /// <summary>
        /// Unique name of the telemetry item
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Timestamp when item was sent
        /// </summary>
        public string? Time { get; set; }

        /// <summary>
        /// Identifier of the resource that uniquely identifies which resource data is sent to
        /// </summary>
        public string? IKey { get; set; }

        /// <summary>
        /// System context properties of the telemetry item, example: ip address, city etc
        /// </summary>
        public Dictionary<string, object>? Ext { get; set; }

        /// <summary>
        /// System context property extensions that are not global (not in ctx)
        /// </summary>
        public Dictionary<string, object>? Tags { get; set; }

        /// <summary>
        /// Custom data
        /// </summary>
        public Dictionary<string, object>? Data { get; set; }

        /// <summary>
        /// Telemetry type used for part B
        /// </summary>
        public string? BaseType { get; set; }

        /// <summary>
        /// Based on schema for part B
        /// </summary>
        public Dictionary<string, object>? BaseData { get; set; }
    }
}
