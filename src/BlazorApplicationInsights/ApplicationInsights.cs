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
            await JSRuntime.InvokeVoidAsync("appInsights.trackEvent", new { name, properties });
        }

        public async Task TrackTrace(string message, SeverityLevel? severityLevel, Dictionary<string, object>? properties)
        {
            await JSRuntime.InvokeVoidAsync("appInsights.trackTrace", new { message, severityLevel, properties });
        }

        public async Task TrackException(Error error, SeverityLevel? severityLevel = null, Dictionary<string, object>? properties = null)
        {
            await JSRuntime.InvokeVoidAsync("appInsights.trackException", new { error, severityLevel, properties });
        }

        public async Task StartTrackPage(string? name = null)
        {
            await JSRuntime.InvokeVoidAsync("appInsights.startTrackPage", new object[] { name });
        }

        public async Task StopTrackPage(string? name = null, string? url = null)
        {
            await JSRuntime.InvokeVoidAsync("appInsights.stopTrackPage", new object[] { name, url });
        }

        public async Task TrackMetric(string name, double average, double? sampleCount = null, double? min = null, double? max = null, Dictionary<string, object>? properties = null)
        {
            await JSRuntime.InvokeVoidAsync("appInsights.trackMetric", new { name, average, sampleCount, min, max, properties });
        }

        public async Task TrackDependencyData(string id, double responseCode, string? absoluteUrl = null, bool? success = null, string? commandName = null, double? duration = null, string? method = null, Dictionary<string, object>? properties = null)
        {
            await JSRuntime.InvokeVoidAsync("appInsights.trackDependencyData", new { id, responseCode, absoluteUrl, success, commandName, duration, method, properties });
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

        public async Task AddTelemetryInitializer(ITelemetryItem telemetryItem)
        {
            await JSRuntime.InvokeVoidAsync("blazorApplicationInsights.addTelemetryInitializer", new object[] { telemetryItem });
        }

        public async Task TrackPageViewPerformance(IPageViewPerformanceTelemetry pageViewPerformance, Dictionary<string, object> customProperties = null)
        {
            await JSRuntime.InvokeVoidAsync("appInsights.trackPageViewPerformance", new object[] { pageViewPerformance, customProperties });
        }
    }
}