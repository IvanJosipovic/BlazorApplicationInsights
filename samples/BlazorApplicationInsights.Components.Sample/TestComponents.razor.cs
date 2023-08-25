using BlazorApplicationInsights.Interfaces;
using BlazorApplicationInsights.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace BlazorApplicationInsights.Components.Sample
{
    public partial class TestComponents
    {
        [Inject]
        private ILogger<TestComponents> Logger { get; set; }

        [Inject]
        private IApplicationInsights AppInsights { get; set; }

        [Inject] HttpClient HttpClient { get; set; }

        private string UserId = string.Empty;
        private string SessionId = string.Empty;

        private bool? CookiesEnabled;

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        private async Task TrackEvent()
        {
            await AppInsights.TrackEvent(new EventTelemetry() { Name = "My Event" }, new Dictionary<string, object>() {{"customProperty", "customValue"}});
            await AppInsights.Flush();
        }

        private async Task TrackTrace()
        {
            await AppInsights.TrackTrace(new TraceTelemetry() { Message = "myMessage" });
            await AppInsights.Flush();
            await AppInsights.TrackTrace(new TraceTelemetry() { Message = "myMessage1", SeverityLevel = SeverityLevel.Critical });
            await AppInsights.Flush();
            await AppInsights.TrackTrace(new TraceTelemetry() { Message = "myMessage2", SeverityLevel = SeverityLevel.Critical }, new Dictionary<string, object>() { { "customProperty", "customValue" } });
            await AppInsights.Flush();
        }

        private async Task TrackException()
        {
            //await AppInsights.TrackException(new Error() { Message = "my message", Name = "my error" }, null, SeverityLevel.Critical);
            await AppInsights.Flush();
        }

        private void TrackGlobalException()
        {
            throw new NotImplementedException("Something wrong happened :(", new InvalidOperationException("TEST INNER"));
        }

        private async Task SetAuthenticatedUserContext()
        {
            await AppInsights.SetAuthenticatedUserContext("myUserId", "myUserName", true);
            await AppInsights.TrackEvent(new EventTelemetry() { Name = "Auth Event" });
            await AppInsights.Flush();
        }

        private async Task ClearAuthenticatedUserContext()
        {
            await AppInsights.SetAuthenticatedUserContext("myUserId", "myUserName", true);
            await AppInsights.TrackEvent(new EventTelemetry() { Name = "Auth Event" });
            await AppInsights.ClearAuthenticatedUserContext();
            await AppInsights.TrackEvent(new EventTelemetry() { Name = "Auth Event2" });
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
            await AppInsights.TrackDependencyData(new DependencyTelemetry()
            {
                Id = "myId",
                Name = "myName",
                Duration = 1000,
                Success = true,
                StartTime = DateTime.Now,
                ResponseCode = 200,
                CorrelationContext = "myContext",
                Type = "myType",
                Data = "mydata",
                Target = "myTarget"
            });
            await AppInsights.Flush();
        }

        private async Task TrackMetric()
        {
            //await AppInsights.TrackMetric("myMetric", 100, 200, 1, 200, new Dictionary<string, object>() {{"customProperty", "customValue"}});
            await AppInsights.Flush();
        }

        private async Task TrackPageView()
        {
            //await AppInsights.TrackPageView("myPage", "https://test.local", "https://test.local", "TestPage", true, new Dictionary<string, object>() {{"customProperty", "customValue"}});
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

        private async Task TrackHttpRequest()
        {
            var str = await HttpClient.GetStringAsync("https://httpbin.org/get");
            await AppInsights.Flush();
        }

        private async Task GetUserId()
        {
            //this.UserId = await AppInsights.GetUserId();
        }

        private async Task GetSessionId()
        {
            //this.SessionId = await AppInsights.GetSessionId();
        }

        private async Task EnableCookies()
        {
            //await AppInsights.SetCookiesEnabled(true);
        }

        private async Task DisableCookies()
        {
            //await AppInsights.SetCookiesEnabled(false);
        }

        private async Task GetCookiesEnabled()
        {
            //CookiesEnabled = await AppInsights.GetCookiesEnabled();
        }
    }
}