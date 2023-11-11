using BlazorApplicationInsights.Models;
using System;

namespace BlazorApplicationInsights
{
    internal class ApplicationInsightsInitConfig
    {
        public Config? Config { get; set; }

        public Func<TelemetryItem, bool>? TelemetryInitializer { get; set; }
    }
}
