using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using BlazorApplicationInsights.Models;

namespace BlazorApplicationInsights.Interfaces;

/// <summary>
/// Application Insights
/// Source:
/// https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/IAppInsights.ts
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
[Browsable(false)]
public interface IAppInsights
{
    /// <summary>
    /// Get the current cookie manager for this instance
    /// </summary>
    CookieMgr GetCookieMgr();

    /// <summary>
    /// Log a user action or other occurrence.
    /// </summary>
    /// <param name="@event"></param>
    Task TrackEvent(EventTelemetry @event);

    /// <summary>
    /// Logs that a page, or similar container was displayed to the user.
    /// </summary>
    /// <param name="pageView"></param>
    Task TrackPageView(PageViewTelemetry? pageView = null);

    /// <summary>
    /// Log an exception that you have caught.
    /// </summary>
    /// <param name="exception"></param>
    Task TrackException(ExceptionTelemetry exception);

    /// <summary>
    /// Log a diagnostic scenario such entering or leaving a function.
    /// </summary>
    /// <param name="trace"></param>
    Task TrackTrace(TraceTelemetry trace);

    /// <summary>
    /// <para>
    /// Log a numeric value that is not associated with a specific event. Typically used
    /// to send regular reports of performance indicators.
    /// </para>
    /// <para>To send a single measurement, just use the `name` and `average` fields</para>
    /// <para>
    ///  If you take measurements frequently, you can reduce the telemetry bandwidth by
    ///  aggregating multiple measurements and sending the resulting average and modifying
    ///  the `sampleCount`.
    /// </para>
    /// </summary>
    /// <param name="metric">input object argument. Only `name` and `average` are mandatory.</param>
    Task TrackMetric(MetricTelemetry metric);

    /// <summary>
    /// Starts the timer for tracking a page load time. Use this instead of `trackPageView` if you want to control when the page view timer starts and stops,
    /// but don't want to calculate the duration yourself. This method doesn't send any telemetry.Call `stopTrackPage` to log the end of the page view
    /// and send the event.
    /// </summary>
    /// <param name="name">A string that identifies this item, unique within this HTML document. Defaults to the document title.</param>
    Task StartTrackPage(string? name = null);

    /// <summary>
    /// Stops the timer that was started by calling `startTrackPage` and sends the pageview load time telemetry with the specified properties and measurements.
    /// The duration of the page view will be the time between calling `startTrackPage` and `stopTrackPage`.
    /// </summary>
    /// <param name="name">The string you used as the name in startTrackPage. Defaults to the document title.</param>
    /// <param name="url">a relative or absolute URL that identifies the page or other item. Defaults to the window location.</param>
    /// <param name="customProperties">additional data used to filter pages and metrics in the portal. Defaults to empty.</param>
    /// <param name="measurements">metrics associated with this page, displayed in Metrics Explorer on the portal. Defaults to empty.</param>
    Task StopTrackPage(string? name = null, string? url = null, Dictionary<string, object?>? customProperties = null, Dictionary<string, decimal>? measurements = null);

    /// <summary>
    /// Starts logging an extended event.
    /// </summary>
    /// <param name="name">The name of the event.</param>
    Task StartTrackEvent(string name);

    /// <summary>
    /// Log an extended event that you started timing with `StartTrackEvent`.
    /// </summary>
    /// <param name="name">The string you used to identify this event in `startTrackEvent`</param>
    /// <param name="properties">additional data used to filter events and metrics in the portal. Defaults to empty.</param>
    /// <param name="measurements">metrics associated with this event, displayed in Metrics Explorer on the portal. Defaults to empty.</param>
    Task StopTrackEvent(string name, Dictionary<string, object?>? properties = null, Dictionary<string, decimal>? measurements = null);

    /// <summary>
    /// Adds a telemetry initializer to the collection. Telemetry initializers will be called one by one,
    /// in the order they were added, before the telemetry item is pushed for sending.
    /// </summary>
    /// <param name="telemetryItem"></param>
    Task AddTelemetryInitializer(TelemetryItem telemetryItem);

    /// <summary>
    /// Log a bag of performance information via the customProperties field.
    /// </summary>
    /// <param name="pageViewPerformance"></param>
    Task TrackPageViewPerformance(PageViewPerformanceTelemetry pageViewPerformance);
}