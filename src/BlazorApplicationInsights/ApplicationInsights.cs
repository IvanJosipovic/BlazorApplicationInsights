using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorApplicationInsights
{
    /// <inheritdoc />
    public class ApplicationInsights : IApplicationInsights
    {
        private readonly Func<IApplicationInsights, Task> _onInsightsInitAction;
        private IJSRuntime _jsRuntime = new NoOpJSRuntime();

        /// <inheritdoc />
        public bool EnableAutoRouteTracking { get; set; }

        public ApplicationInsights()
            : this(null)
        {
        }

        public ApplicationInsights(Func<IApplicationInsights, Task>? onInsightsInitAction)
        {
            _onInsightsInitAction = onInsightsInitAction ?? delegate { return Task.CompletedTask; };
        }

        /// <inheritdoc />
        public async Task InitBlazorApplicationInsightsAsync(IJSRuntime jSRuntime)
        {
            _jsRuntime = jSRuntime;
            await _onInsightsInitAction(this);
        }

        /// <inheritdoc />
        public async Task TrackPageView(string? name = null, string? uri = null, string? refUri = null, string? pageType = null, bool? isLoggedIn = null, Dictionary<string, object?>? properties = null)
            => await _jsRuntime.InvokeVoidAsync("appInsights.trackPageView", new { name, uri, refUri, pageType, isLoggedIn }, properties!);

        /// <inheritdoc />
        public async Task TrackEvent(string name, Dictionary<string, object?>? properties = null)
            => await _jsRuntime.InvokeVoidAsync("appInsights.trackEvent", new { name }, properties!);

        /// <inheritdoc />
        public async Task TrackTrace(string message, SeverityLevel? severityLevel = null, Dictionary<string, object?>? properties = null)
            => await _jsRuntime.InvokeVoidAsync("appInsights.trackTrace", new { message, severityLevel }, properties!);

        /// <inheritdoc />
        public async Task TrackException(Error exception, string? id = null, SeverityLevel? severityLevel = null, Dictionary<string, object?>? properties = null)
            => await _jsRuntime.InvokeVoidAsync("appInsights.trackException", new { id, exception, severityLevel }, properties!);

        /// <inheritdoc />
        public async Task StartTrackPage(string? name = null)
            => await _jsRuntime.InvokeVoidAsync("appInsights.startTrackPage", name!);

        /// <inheritdoc />
        public async Task StopTrackPage(string? name = null, string? url = null, Dictionary<string, string?>? properties = null, Dictionary<string, decimal>? measurements = null)
            => await _jsRuntime.InvokeVoidAsync("appInsights.stopTrackPage", name!, url!, properties!, measurements!);

        /// <inheritdoc />
        public async Task TrackMetric(string name, double average, double? sampleCount = null, double? min = null, double? max = null, Dictionary<string, object?>? properties = null)
            => await _jsRuntime.InvokeVoidAsync("appInsights.trackMetric", new { name, average, sampleCount, min, max }, properties!);

        /// <inheritdoc />
        public async Task TrackDependencyData(string id, string name, decimal? duration = null, bool? success = null, DateTime? startTime = null, int? responseCode = null, string? correlationContext = null, string? type = null, string? data = null, string? target = null)
            => await _jsRuntime.InvokeVoidAsync("blazorApplicationInsights.trackDependencyData", new { id, name, duration, success, startTime = startTime?.ToString("yyyy-MM-ddTHH:mm:ss"), responseCode, correlationContext, type, data, target });

        /// <inheritdoc />
        public async Task Flush(bool? async = true)
            => await _jsRuntime.InvokeVoidAsync("appInsights.flush", async!);

        /// <inheritdoc />
        public async Task ClearAuthenticatedUserContext()
            => await _jsRuntime.InvokeVoidAsync("appInsights.clearAuthenticatedUserContext");

        /// <inheritdoc />
        public async Task SetAuthenticatedUserContext(string authenticatedUserId, string? accountId = null, bool storeInCookie = false)
            => await _jsRuntime.InvokeVoidAsync("appInsights.setAuthenticatedUserContext", authenticatedUserId, accountId!, storeInCookie);

        /// <inheritdoc />
        public async Task AddTelemetryInitializer(TelemetryItem telemetryItem)
            => await _jsRuntime.InvokeVoidAsync("blazorApplicationInsights.addTelemetryInitializer", telemetryItem);

        /// <inheritdoc />
        public async Task TrackPageViewPerformance(PageViewPerformanceTelemetry pageViewPerformance)
            => await _jsRuntime.InvokeVoidAsync("appInsights.trackPageViewPerformance", pageViewPerformance);

        /// <inheritdoc />
        public async Task StartTrackEvent(string name)
            => await _jsRuntime.InvokeVoidAsync("appInsights.startTrackEvent", name);

        /// <inheritdoc />
        public async Task StopTrackEvent(string name, Dictionary<string, string?>? properties = null, Dictionary<string, decimal>? measurements = null)
            => await _jsRuntime.InvokeVoidAsync("appInsights.stopTrackEvent", name, properties!, measurements!);

        /// <inheritdoc />
        public async Task SetInstrumentationKey(string key)
            => await _jsRuntime.InvokeVoidAsync("blazorApplicationInsights.setInstrumentationKey", key);

        /// <inheritdoc />
        public async Task LoadAppInsights()
            => await _jsRuntime.InvokeVoidAsync("blazorApplicationInsights.loadAppInsights");

        /// <inheritdoc />
        public async Task SetConnectionString(string connectionString)
            => await _jsRuntime.InvokeVoidAsync("blazorApplicationInsights.setConnectionString", connectionString);

        private class NoOpJSRuntime : IJSRuntime
        {
            public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args) => Invoked<TValue>();
            public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args) => Invoked<TValue>();

            private static ValueTask<TValue> Invoked<TValue>()
            {
                Debug.WriteLine("Attempted to use " + nameof(ApplicationInsights) + " before calling " + nameof(InitBlazorApplicationInsightsAsync));
                return new ValueTask<TValue>(default(TValue)!);
            }
        }
    }
}