using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApplicationInsights
{
    public interface IApplicationInsights
    {
        /// <summary>
        /// Log a user action or other occurrence.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        Task TrackEvent(string name, Dictionary<string, object>? properties = null);

        /// <summary>
        /// Log a diagnostic scenario such entering or leaving a function.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="severityLevel"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        Task TrackTrace(string message, SeverityLevel? severityLevel = null, Dictionary<string, object>? properties = null);

        /// <summary>
        /// Log an exception that you have caught.
        /// </summary>
        /// <param name="error"></param>
        /// <param name="severityLevel"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        Task TrackException(Error error, SeverityLevel? severityLevel = null, Dictionary<string, object>? properties = null);

        /// <summary>
        /// Logs that a page, or similar container was displayed to the user.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="uri"></param>
        /// <param name="refUri"></param>
        /// <param name="pageType"></param>
        /// <param name="isLoggedIn"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        Task TrackPageView(string? name = null, string? uri = null, string? refUri = null, string? pageType = null, bool? isLoggedIn = null, Dictionary<string, object>? properties = null);

        /// <summary>
        /// Starts the timer for tracking a page load time. Use this instead of `trackPageView` if you want to control when the page view timer starts and stops,
        /// but don't want to calculate the duration yourself. This method doesn't send any telemetry.Call `stopTrackPage` to log the end of the page view
        /// and send the event.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task StartTrackPage(string? name = null);

        /// <summary>
        /// Stops the timer that was started by calling `startTrackPage` and sends the pageview load time telemetry with the specified properties and measurements.
        /// The duration of the page view will be the time between calling `startTrackPage` and `stopTrackPage`.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        Task StopTrackPage(string? name = null, string? url = null);

        /// <summary>
        /// Log a numeric value that is not associated with a specific event. Typically used
        /// to send regular reports of performance indicators.
        ///
        /// To send a single measurement, just use the `name` and `average` fields
        ///
        ///  If you take measurements frequently, you can reduce the telemetry bandwidth by
        ///  aggregating multiple measurements and sending the resulting average and modifying
        ///  the `sampleCount`.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="average"></param>
        /// <param name="sampleCount"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        Task TrackMetric(string name, double average, double? sampleCount = null, double? min = null, double? max = null, Dictionary<string, object>? properties = null);

        /// <summary>
        /// Log a dependency call (e.g. ajax)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="responseCode"></param>
        /// <param name="absoluteUrl"></param>
        /// <param name="success"></param>
        /// <param name="commandName"></param>
        /// <param name="duration"></param>
        /// <param name="method"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        Task TrackDependencyData(string id, double responseCode, string? absoluteUrl = null, bool? success = null, string? commandName = null, double? duration = null, string? method = null, Dictionary<string, object>? properties = null);

        /// <summary>
        /// Manually trigger an immediate send of all telemetry still in the buffer.
        /// </summary>
        /// <param name="async"></param>
        /// <returns></returns>
        Task Flush(bool? async = true);

        /// <summary>
        /// Clears the authenticated user id and account id. The associated cookie is cleared, if present.
        /// </summary>
        /// <returns></returns>
        Task ClearAuthenticatedUserContext();

        /// <summary>
        /// Set the authenticated user id and the account id. Used for identifying a specific signed-in user. Parameters must not contain whitespace or ,;=|
        ///
        /// The method will only set the `authenticatedUserId` and `accountId` in the current page view. To set them for the whole session, you should set `storeInCookie = true`
        ///
        /// </summary>
        /// <param name="authenticatedUserId"></param>
        /// <param name="accountId"></param>
        /// <param name="storeInCookie"></param>
        /// <returns></returns>
        Task SetAuthenticatedUserContext(string authenticatedUserId, string? accountId = null, bool storeInCookie = false);
    }
}