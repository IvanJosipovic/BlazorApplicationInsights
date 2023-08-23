using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorApplicationInsights
{
    // https://github.com/microsoft/ApplicationInsights-JS/blob/master/shared/AppInsightsCommon/src/Interfaces/IAppInsights.ts
    // https://github.com/microsoft/ApplicationInsights-JS/blob/master/AISKU/src/Initialization.ts
    // https://github.com/microsoft/ApplicationInsights-JS/blob/master/API-reference.md
    /// <summary>
    /// Application Insights for Blazor web applications
    /// </summary>
    public interface IApplicationInsights
    {
        /// <summary>
        /// Set IJSRuntime and run init action queue
        /// </summary>
        /// <param name="jSRuntime"></param>
        Task InitBlazorApplicationInsightsAsync(IJSRuntime jSRuntime);

        /// <summary>
        /// Log a user action or other occurrence.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="properties"></param>
        Task TrackEvent(string name, Dictionary<string, object?>? properties = null);

        /// <summary>
        /// Log a diagnostic scenario such entering or leaving a function.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="severityLevel"></param>
        /// <param name="properties"></param>
        Task TrackTrace(string message, SeverityLevel? severityLevel = null, Dictionary<string, object?>? properties = null);

        /// <summary>
        /// Log an exception that you have caught.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="id">Unique guid identifying this error</param>
        /// <param name="severityLevel"></param>
        /// <param name="properties"></param>
        Task TrackException(Error exception, string? id = null, SeverityLevel? severityLevel = null, Dictionary<string, object?>? properties = null);

        /// <summary>
        /// Logs that a page, or similar container was displayed to the user.
        /// </summary>
        /// <param name="name">The string you used as the name in startTrackPage. Defaults to the document title.</param>
        /// <param name="uri">a relative or absolute URL that identifies the page or other item. Defaults to the window location.</param>
        /// <param name="refUri">the URL of the source page where current page is loaded from</param>
        /// <param name="pageType">page type</param>
        /// <param name="isLoggedIn">is user logged in</param>
        /// <param name="properties">Property bag to contain additional custom properties (Part C)</param>
        Task TrackPageView(string? name = null, string? uri = null, string? refUri = null, string? pageType = null, bool? isLoggedIn = null, Dictionary<string, object?>? properties = null);

        /// <summary>
        /// Starts the timer for tracking a page load time. Use this instead of `trackPageView` if you want to control when the page view timer starts and stops,
        /// but don't want to calculate the duration yourself. This method doesn't send any telemetry.Call `stopTrackPage` to log the end of the page view
        /// and send the event.
        /// </summary>
        /// <param name="name">A string that idenfities this item, unique within this HTML document. Defaults to the document title.</param>
        Task StartTrackPage(string? name = null);

        /// <summary>
        /// Stops the timer that was started by calling `startTrackPage` and sends the pageview load time telemetry with the specified properties and measurements.
        /// The duration of the page view will be the time between calling `startTrackPage` and `stopTrackPage`.
        /// </summary>
        /// <param name="name">The string you used as the name in startTrackPage. Defaults to the document title.</param>
        /// <param name="url">a relative or absolute URL that identifies the page or other item. Defaults to the window location.</param>
        /// <param name="properties">additional data used to filter pages and metrics in the portal. Defaults to empty.</param>
        /// <param name="measurements">map[string, number] - metrics associated with this page, displayed in Metrics Explorer on the portal. Defaults to empty.</param>
        Task StopTrackPage(string? name = null, string? url = null, Dictionary<string, string?>? properties = null, Dictionary<string, decimal>? measurements = null);

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
        /// <param name="name">name of this metric</param>
        /// <param name="average">Recorded value/average for this metric</param>
        /// <param name="sampleCount">Number of samples represented by the average.</param>
        /// <param name="min">The smallest measurement in the sample. Defaults to the average</param>
        /// <param name="max">The largest measurement in the sample. Defaults to the average.</param>
        /// <param name="properties"></param>
        Task TrackMetric(string name, double average, double? sampleCount = null, double? min = null, double? max = null, Dictionary<string, object?>? properties = null);

        /// <summary>
        /// Log a dependency call (e.g. ajax)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="duration"></param>
        /// <param name="success"></param>
        /// <param name="startTime"></param>
        /// <param name="responseCode"></param>
        /// <param name="correlationContext"></param>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <param name="target"></param>
        Task TrackDependencyData(
                                string id,
                                string name,
                                decimal? duration = null,
                                bool? success = null,
                                DateTime? startTime = null,
                                int? responseCode = null,
                                string? correlationContext = null,
                                string? type = null,
                                string? data = null,
                                string? target = null
            );

        /// <summary>
        /// Manually trigger an immediate send of all telemetry still in the buffer.
        /// </summary>
        /// <param name="async"></param>
        Task Flush(bool? async = true);

        /// <summary>
        /// Clears the authenticated user id and account id. The associated cookie is cleared, if present.
        /// </summary>
        Task ClearAuthenticatedUserContext();

        /// <summary>
        /// <para>Set the authenticated user id and the account id. Used for identifying a specific signed-in user. Parameters must not contain whitespace or ,;=|</para>
        /// <para>The method will only set the `authenticatedUserId` and `accountId` in the current page view. To set them for the whole session, you should set `storeInCookie = true`</para>
        ///
        /// </summary>
        /// <param name="authenticatedUserId"></param>
        /// <param name="accountId"></param>
        /// <param name="storeInCookie"></param>
        Task SetAuthenticatedUserContext(string authenticatedUserId, string? accountId = null, bool storeInCookie = false);

        /// <summary>
        /// Adds a telemetry initializer to the collection. Telemetry initializers will be called one by one,
        /// in the order they were added, before the telemetry item is pushed for sending.
        /// If one of the telemetry initializers returns false or throws an error then the telemetry item will not be sent.
        /// </summary>
        /// <param name="telemetryItem"></param>
        Task AddTelemetryInitializer(TelemetryItem telemetryItem);

        /// <summary>
        /// Send browser performance metrics.
        /// </summary>
        /// <param name="pageViewPerformance"></param>
        Task TrackPageViewPerformance(PageViewPerformanceTelemetry pageViewPerformance);

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
        Task StopTrackEvent(string name, Dictionary<string, string?>? properties = null, Dictionary<string, decimal>? measurements = null);

        /// <summary>
        /// Sets the Connection String
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        Task SetConnectionString(string connectionString);

        /// <summary>
        /// Initializes Application Insights
        /// </summary>
        /// <returns></returns>
        Task LoadAppInsights();

        /// <summary>
        /// <para>
        /// Enables automatic Track Page View on Route changes
        /// </para>
        /// Should only be enabled through AddBlazorApplicationInsights
        /// </summary>
        bool EnableAutoRouteTracking { get; set; }

        /// <summary>
        /// Returns the session id sent on every request
        /// </summary>
        /// <returns></returns>
        Task<string> GetSessionId();

        /// <summary>
        /// Returns the user id sent on every request
        /// </summary>
        /// <returns></returns>
        Task<string> GetUserId();

        /// <summary>
        /// Sets whether cookies are enabled
        /// </summary>
        /// <param name="enabled"></param>
        /// <returns></returns>
        Task SetCookiesEnabled(bool enabled);

        /// <summary>
        /// Gets whether cookies are enabled
        /// </summary>
        /// <returns></returns>
        Task<bool> GetCookiesEnabled();
    }
}