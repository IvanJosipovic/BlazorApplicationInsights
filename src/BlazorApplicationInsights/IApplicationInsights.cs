using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApplicationInsights
{
    public interface IApplicationInsights
    {
        Task TrackEvent(string name, Dictionary<string, object>? properties = null);
        Task TrackTrace(string message, SeverityLevel? severityLevel = null, Dictionary<string, object>? properties = null);
        Task TrackException(Error error, SeverityLevel? severityLevel = null, Dictionary<string, object>? properties = null);
        Task TrackPageView(string? name = null, string? uri = null, string? refUri = null, string? pageType = null, bool? isLoggedIn = null, Dictionary<string, object>? properties = null);
        Task StartTrackPage(string? name = null);
        Task StopTrackPage(string? name = null, string? url = null);
        Task TrackMetric(string name, double average, double? sampleCount = null, double? min = null, double? max = null, Dictionary<string, object>? properties = null);
        Task TrackDependencyData(string id, double responseCode, string? absoluteUrl = null, bool? success = null, string? commandName = null, double? duration = null, string? method = null, Dictionary<string, object>? properties = null);
        Task Flush(bool? async = true);
        Task ClearAuthenticatedUserContext();
        Task SetAuthenticatedUserContext(string authenticatedUserId, string? accountId = null, bool storeInCookie = false);
    }
}