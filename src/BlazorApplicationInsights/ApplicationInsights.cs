using BlazorApplicationInsights.Interfaces;
using BlazorApplicationInsights.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorApplicationInsights;

/// <inheritdoc />
public class ApplicationInsights : IApplicationInsights
{
    public ApplicationInsights(IJSRuntime jSRuntime)
    {
        _jsRuntime = jSRuntime;
    }

    private readonly IJSRuntime _jsRuntime;

    /// <inheritdoc />
    public async Task TrackPageView(PageViewTelemetry? pageView = null)
        => await _jsRuntime.InvokeVoidAsync("appInsights.trackPageView", pageView);

    /// <inheritdoc />
    public async Task TrackEvent(EventTelemetry @event, Dictionary<string, object>? customProperties = null)
        => await _jsRuntime.InvokeVoidAsync("appInsights.trackEvent", @event, customProperties);

    /// <inheritdoc />
    public async Task TrackTrace(TraceTelemetry trace, Dictionary<string, object>? customProperties = null)
        => await _jsRuntime.InvokeVoidAsync("appInsights.trackTrace", trace, customProperties);

    /// <inheritdoc />
    public async Task TrackException(ExceptionTelemetry exception, Dictionary<string, object>? customProperties = null)
        => await _jsRuntime.InvokeVoidAsync("appInsights.trackException", exception, customProperties);

    /// <inheritdoc />
    public async Task StartTrackPage(string? name = null)
        => await _jsRuntime.InvokeVoidAsync("appInsights.startTrackPage", name!);

    /// <inheritdoc />
    public async Task StopTrackPage(string? name = null, string? url = null, Dictionary<string, string>? customProperties = null, Dictionary<string, decimal>? measurements = null)
        => await _jsRuntime.InvokeVoidAsync("appInsights.stopTrackPage", name, url, customProperties, measurements);

    /// <inheritdoc />
    public async Task TrackMetric(MetricTelemetry metric, Dictionary<string, object>? customProperties = null)
        => await _jsRuntime.InvokeVoidAsync("appInsights.trackMetric", metric, customProperties);

    /// <inheritdoc />
    public async Task TrackDependencyData(DependencyTelemetry dependency)
        => await _jsRuntime.InvokeVoidAsync("appInsights.trackDependencyData", dependency);

    /// <inheritdoc />
    public async Task Flush(bool? async = null)
        => await _jsRuntime.InvokeVoidAsync("appInsights.flush", async);

    /// <inheritdoc />
    public async Task ClearAuthenticatedUserContext()
        => await _jsRuntime.InvokeVoidAsync("appInsights.clearAuthenticatedUserContext");

    /// <inheritdoc />
    public async Task SetAuthenticatedUserContext(string authenticatedUserId, string? accountId = null, bool? storeInCookie = null)
        => await _jsRuntime.InvokeVoidAsync("appInsights.setAuthenticatedUserContext", authenticatedUserId, accountId, storeInCookie);

    /// <inheritdoc />
    public async Task AddTelemetryInitializer(Func<TelemetryItem, bool> telemetryInitializer)
        => await _jsRuntime.InvokeVoidAsync("blazorApplicationInsights.addTelemetryInitializer", DotNetObjectReference.Create(new TelemetryInitializer(telemetryInitializer)));

    /// <inheritdoc />
    public async Task TrackPageViewPerformance(PageViewPerformanceTelemetry pageViewPerformance)
        => await _jsRuntime.InvokeVoidAsync("appInsights.trackPageViewPerformance", pageViewPerformance);

    /// <inheritdoc />
    public async Task StartTrackEvent(string name)
        => await _jsRuntime.InvokeVoidAsync("appInsights.startTrackEvent", name);

    /// <inheritdoc />
    public async Task StopTrackEvent(string name, Dictionary<string, string?>? properties = null, Dictionary<string, decimal>? measurements = null)
        => await _jsRuntime.InvokeVoidAsync("appInsights.stopTrackEvent", name, properties, measurements);

    /// <inheritdoc />
    public async Task UpdateCfg(BlazorApplicationInsightsConfig newConfig, bool? mergeExisting)
        => await _jsRuntime.InvokeVoidAsync("appInsights.updateCfg", newConfig, mergeExisting);

    /// <inheritdoc />
    public async Task<TelemetryContext> Context()
        => await _jsRuntime.InvokeAsync<TelemetryContext>("blazorApplicationInsights.getContext");

    /// <inheritdoc />
    public CookieMgr GetCookieMgr()
    {
        return new CookieMgr(_jsRuntime);
    }
}
