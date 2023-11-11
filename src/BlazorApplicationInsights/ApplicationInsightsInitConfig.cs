using BlazorApplicationInsights.Models;

namespace BlazorApplicationInsights
{
    internal class ApplicationInsightsInitConfig
    {
        public Config? Config { get; set; }

        public TelemetryItem TelemetryInitializer { get; set; }
    }
}
