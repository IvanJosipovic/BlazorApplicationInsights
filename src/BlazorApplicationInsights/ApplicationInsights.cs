using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApplicationInsights
{
    public class ApplicationInsights : IApplicationInsights
    {
        private IJSRuntime? JSRuntime { get; set; }
        private Func<IApplicationInsights, Task>? OnInsightsInitAction { get; }

        public ApplicationInsights()
        {
        }

        public ApplicationInsights(Func<IApplicationInsights, Task> onInsightsInitAction)
        {
            OnInsightsInitAction = onInsightsInitAction;
        }

        public async Task InitBlazorApplicationInsightsAsync(IJSRuntime jSRuntime)
        {
            JSRuntime = jSRuntime;

            if (OnInsightsInitAction != null)
            {
                await OnInsightsInitAction.Invoke(this);
            }
        }

        public async Task TrackPageView(string? name = null, string? uri = null, string? refUri = null, string? pageType = null, bool? isLoggedIn = null, Dictionary<string, object>? properties = null)
        {
            await JSRuntime.InvokeVoidAsync("appInsights.trackPageView", new { name, uri, refUri, pageType, isLoggedIn, properties });
        }

        public async Task TrackEvent(string name, Dictionary<string, object>? properties = null)
        {
            await JSRuntime.InvokeVoidAsync("appInsights.trackEvent", new object[] { new { name }, properties });
        }

        public async Task TrackTrace(string message, SeverityLevel? severityLevel, Dictionary<string, object>? properties)
        {
            await JSRuntime.InvokeVoidAsync("appInsights.trackTrace", new object[] { new { message, severityLevel }, properties });
        }

        public async Task TrackException(Error exception, string? id = null, SeverityLevel? severityLevel = null)
        {
            await JSRuntime.InvokeVoidAsync("appInsights.trackException", new { id, exception, severityLevel });
        }

        public async Task StartTrackPage(string? name = null)
        {
            await JSRuntime.InvokeVoidAsync("appInsights.startTrackPage", new object[] { name });
        }

        public async Task StopTrackPage(string? name = null, string? url = null, Dictionary<string, string>? properties = null, Dictionary<string, decimal>? measurements = null)
        {
            await JSRuntime.InvokeVoidAsync("appInsights.stopTrackPage", new object[] { name, url, properties, measurements });
        }

        public async Task TrackMetric(string name, double average, double? sampleCount = null, double? min = null, double? max = null, Dictionary<string, object>? properties = null)
        {
            await JSRuntime.InvokeVoidAsync("appInsights.trackMetric", new object[] { new { name, average, sampleCount, min, max }, properties });
        }

        public async Task TrackDependencyData(string id, string name, decimal? duration = null, bool? success = null, DateTime? startTime = null, int? responseCode = null, string? correlationContext = null, string? type = null, string? data = null, string? target = null)
        {
            await JSRuntime.InvokeVoidAsync("blazorApplicationInsights.trackDependencyData", new { id, name, duration, success, startTime = startTime.HasValue ? startTime.Value.ToString("yyyy-MM-ddTHH:mm:ss") : null, responseCode, correlationContext, type, data, target });
        }

        public async Task Flush(bool? async = true)
        {
            await JSRuntime.InvokeVoidAsync("appInsights.flush", new object[] { async });
        }

        public async Task ClearAuthenticatedUserContext()
        {
            await JSRuntime.InvokeVoidAsync("appInsights.clearAuthenticatedUserContext");
        }

        public async Task SetAuthenticatedUserContext(string authenticatedUserId, string? accountId = null, bool storeInCookie = false)
        {
            await JSRuntime.InvokeVoidAsync("appInsights.setAuthenticatedUserContext", new object[] { authenticatedUserId, accountId, storeInCookie });
        }

        public async Task AddTelemetryInitializer(TelemetryItem telemetryItem)
        {
            await JSRuntime.InvokeVoidAsync("blazorApplicationInsights.addTelemetryInitializer", new object[] { telemetryItem });
        }

        public async Task TrackPageViewPerformance(PageViewPerformanceTelemetry pageViewPerformance)
        {
            await JSRuntime.InvokeVoidAsync("appInsights.trackPageViewPerformance", new object[] { pageViewPerformance });
        }

        public async Task StartTrackEvent(string name)
        {
            await JSRuntime.InvokeVoidAsync("appInsights.startTrackEvent", new object[] { name });
        }

        public async Task StopTrackEvent(string name, Dictionary<string, string>? properties = null, Dictionary<string, decimal>? measurements = null)
        {
            await JSRuntime.InvokeVoidAsync("appInsights.stopTrackEvent", new object[] { name, properties, measurements });
        }
    }
}