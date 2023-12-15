using BlazorApplicationInsights.Interfaces;
using BlazorApplicationInsights.Models;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorApplicationInsights;

/// <inheritdoc />
public class ApplicationInsights : IApplicationInsights
{
    private IJSRuntime _jsRuntime;

    /// <inheritdoc />
    public void InitJSRuntime(IJSRuntime jSRuntime)
    {
        _jsRuntime = jSRuntime;
    }

    /// <inheritdoc />
    public async Task TrackPageView(PageViewTelemetry? pageView = null)
        => await _jsRuntime.InvokeVoidAsync("appInsights.trackPageView", pageView);

    /// <inheritdoc />
    public async Task TrackEvent(EventTelemetry @event)
        => await _jsRuntime.InvokeVoidAsync("appInsights.trackEvent", @event);

    /// <inheritdoc />
    public async Task TrackTrace(TraceTelemetry trace)
        => await _jsRuntime.InvokeVoidAsync("appInsights.trackTrace", trace);

    /// <inheritdoc />
    public async Task TrackException(ExceptionTelemetry exception)
        => await _jsRuntime.InvokeVoidAsync("appInsights.trackException", exception);

    /// <inheritdoc />
    public async Task StartTrackPage(string? name = null)
        => await _jsRuntime.InvokeVoidAsync("appInsights.startTrackPage", name!);

    /// <inheritdoc />
    public async Task StopTrackPage(string? name = null, string? url = null, Dictionary<string, object?>? customProperties = null, Dictionary<string, decimal>? measurements = null)
        => await _jsRuntime.InvokeVoidAsync("appInsights.stopTrackPage", name, url, customProperties, measurements);

    /// <inheritdoc />
    public async Task TrackMetric(MetricTelemetry metric)
        => await _jsRuntime.InvokeVoidAsync("appInsights.trackMetric", metric);

    /// <inheritdoc />
    public async Task TrackDependencyData(DependencyTelemetry dependency)
        => await _jsRuntime.InvokeVoidAsync("blazorApplicationInsights.trackDependencyData", dependency);

    /// <inheritdoc />
    public async Task Flush()
        => await _jsRuntime.InvokeVoidAsync("appInsights.flush", false);

    /// <inheritdoc />
    public async Task ClearAuthenticatedUserContext()
        => await _jsRuntime.InvokeVoidAsync("appInsights.clearAuthenticatedUserContext");

    /// <inheritdoc />
    public async Task SetAuthenticatedUserContext(string authenticatedUserId, string? accountId = null, bool? storeInCookie = null)
        => await _jsRuntime.InvokeVoidAsync("appInsights.setAuthenticatedUserContext", authenticatedUserId, accountId, storeInCookie);

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
    public async Task StopTrackEvent(string name, Dictionary<string, object?>? properties = null, Dictionary<string, decimal>? measurements = null)
        => await _jsRuntime.InvokeVoidAsync("appInsights.stopTrackEvent", name, properties, measurements);

    /// <inheritdoc />
    public async Task UpdateCfg(Config newConfig, bool? mergeExisting = true)
    {
        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        var configJson = JsonSerializer.Serialize(newConfig, options);

        await _jsRuntime.InvokeVoidAsync("appInsights.updateCfg", configJson, mergeExisting);
    }

    /// <inheritdoc />
    public async Task<TelemetryContext> Context()
        => await _jsRuntime.InvokeAsync<TelemetryContext>("blazorApplicationInsights.getContext");

    /// <inheritdoc />
    public CookieMgr GetCookieMgr()
    {
        return new CookieMgr(_jsRuntime);
    }
}
