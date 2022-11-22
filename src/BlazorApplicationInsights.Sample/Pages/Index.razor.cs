using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace BlazorApplicationInsights.Sample.Pages
{
    public partial class Index
    {
        [Inject]
        private ILogger<Index> Logger { get; set; }

        [Inject]
        private IApplicationInsights AppInsights { get; set; }

        [Inject] HttpClient HttpClient { get; set; }

        private string UserId = string.Empty;
        private string SessionId = string.Empty;

        private async Task TrackEvent()
        {
            await AppInsights.TrackEvent("My Event", new Dictionary<string, object>() {{"customProperty", "customValue"}});
            await AppInsights.Flush();
        }

        private async Task TrackTrace()
        {
            await AppInsights.TrackTrace("myMessage");
            await AppInsights.Flush();
            await AppInsights.TrackTrace("myMessage1", SeverityLevel.Critical);
            await AppInsights.Flush();
            await AppInsights.TrackTrace("myMessage2", SeverityLevel.Critical, new Dictionary<string, object>() {{"customProperty", "customValue"}});
            await AppInsights.Flush();
        }

        private async Task TrackException()
        {
            await AppInsights.TrackException(new Error() { Message = "my message", Name = "my error" }, null, SeverityLevel.Critical);
            await AppInsights.Flush();
        }

        private void TrackGlobalException()
        {
            throw new Exception("Something wrong happened :(");
        }

        private async Task SetAuthenticatedUserContext()
        {
            await AppInsights.SetAuthenticatedUserContext("myUserId", "myUserName", true);
            await AppInsights.TrackEvent("Auth Event");
            await AppInsights.Flush();
        }

        private async Task ClearAuthenticatedUserContext()
        {
            await AppInsights.SetAuthenticatedUserContext("myUserId", "myUserName", true);
            await AppInsights.TrackEvent("Auth Event");
            await AppInsights.ClearAuthenticatedUserContext();
            await AppInsights.TrackEvent("Auth Event2");
            await AppInsights.Flush();
        }

        private async Task StartStopTrackPage()
        {
            await AppInsights.StartTrackPage("myPage");
            await AppInsights.Flush();
            await Task.Delay(100);
            await AppInsights.StopTrackPage("myPage");
            await AppInsights.Flush();
        }

        private async Task TrackDependencyData()
        {
            await AppInsights.TrackDependencyData("myId", "myName", 1000, true, DateTime.Now, 200, "myContext", "myType", "mydata", "myTarget");
            await AppInsights.Flush();
        }

        private async Task TrackMetric()
        {
            await AppInsights.TrackMetric("myMetric", 100, 200, 1, 200, new Dictionary<string, object>() {{"customProperty", "customValue"}});
            await AppInsights.Flush();
        }

        private async Task TrackPageView()
        {
            await AppInsights.TrackPageView("myPage", "https://test.local", "https://test.local", "TestPage", true, new Dictionary<string, object>() {{"customProperty", "customValue"}});
            await AppInsights.Flush();
        }

        private async Task TrackPageViewPerformance()
        {
            await AppInsights.TrackPageViewPerformance(new PageViewPerformanceTelemetry()
            {
                Name = "myPerf"
            });
            await AppInsights.Flush();
        }

        private async Task TestLogger()
        {
            Logger.LogInformation("My Logging Test");
            await AppInsights.Flush();
        }

        private async Task TestSemanticLogger()
        {
            Logger.LogInformation("My Semantic Logging Test with customProperty={customProperty}", "customValue");
            await AppInsights.Flush();
        }

        private async Task StartStopTrackEvent()
        {
            await AppInsights.StartTrackEvent("myEvent");
            await AppInsights.Flush();
            await AppInsights.StopTrackEvent("myEvent");
            await AppInsights.Flush();
        }

        private async Task SetInstrumentationKey()
        {
            await AppInsights.SetInstrumentationKey("219f9af4-0842-42c8-a5b1-578f09d2ee27");
            await AppInsights.LoadAppInsights();
        }

        private async Task TrackHttpRequest()
        {
            var str = await HttpClient.GetStringAsync("https://httpbin.org/get");
            await AppInsights.Flush();
        }

        private async Task GetUserId()
        {
            this.UserId = await AppInsights.GetUserId();
        }

        private async Task GetSessionId()
        {
            this.SessionId = await AppInsights.GetSessionId();
        }
    }
}