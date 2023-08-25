using System.Text.Json.Serialization;

namespace BlazorApplicationInsights.Models
{
    /// <summary>
    /// Source: https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/IMetricTelemetry.ts
    /// </summary>
    public class MetricTelemetry : PartC
    {
        /// <summary>
        /// (required) - Name of this metric
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// (required) - Recorded value/average for this metric
        /// </summary>
        [JsonPropertyName("average")]
        public double Average { get; set; }

        /// <summary>
        /// (optional) Number of samples represented by the average.
        /// </summary>
        [JsonPropertyName("sampleCount")]
        public int? SampleCount { get; set; }

        /// <summary>
        /// (optional) The smallest measurement in the sample. Defaults to the average.
        /// </summary>
        [JsonPropertyName("min")]
        public double? Min { get; set; }

        /// <summary>
        /// (optional) The largest measurement in the sample. Defaults to the average.
        /// </summary>
        [JsonPropertyName("max")]
        public double? Max { get; set; }

        /// <summary>
        /// (optional) The standard deviation measurement in the sample. Defaults to undefined which results in zero.
        /// </summary>
        [JsonPropertyName("stdDev")]
        public double? StdDev { get; set; }

        /// <summary>
        /// Custom defined iKey
        /// </summary>
        [JsonPropertyName("iKey")]
        public string? IKey { get; set; }
    }
}