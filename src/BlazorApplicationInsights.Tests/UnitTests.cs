using FluentAssertions;
using PlaywrightSharp;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace BlazorApplicationInsights.Tests
{
    public class UnitTests
    {
        private string BaseAddress = "https://localhost:5001/";
        private bool Headless = true;

        public UnitTests()
        {
            string filename = "BrowserTestsAddress.config";
            if (File.Exists(filename))
            {
                BaseAddress = File.ReadAllText(filename);
            }
        }

        [Theory]
        [InlineData("TrackEvent", false, 2)]
        [InlineData("TrackTrace", false, 4)]
        [InlineData("TrackException", false, 2)]
        [InlineData("TrackGlobalException", false, 1)]
        [InlineData("SetAuthenticatedUserContext", false, 1)]
        [InlineData("ClearAuthenticatedUserContext", false, 1)]
        [InlineData("StartStopTrackPage", false, 1)]
        [InlineData("TrackDependencyData", false, 2)]
        [InlineData("TrackMetric", false, 2)]
        [InlineData("TrackPageView", false, 2)]
        [InlineData("TrackPageViewPerformance", false, 2)]
        [InlineData("TestLogger", false, 2)]
        [InlineData("StartStopTrackEvent", false, 2)]
        public async Task Test(string id, bool shouldError, int expectedCalls)
        {
            bool hasError = false;
            int appInsightsCalls = 0;

            using var playwright = await Playwright.CreateAsync();

            await using var browser = await playwright.Chromium.LaunchAsync(Headless);
            var page = await browser.NewPageAsync();

            page.Console += (sender, e) =>
            {
                if (e.Message.Type == "error" && !e.Message.Text.Contains("Something wrong happened :("))
                {
                    hasError = true;
                }
            };

            page.Request += (sender, e) =>
            {
                if (e.Request.Url.Equals("https://dc.services.visualstudio.com/v2/track"))
                {
                    appInsightsCalls++;
                }
            };

            page.RequestFailed += (sender, e) =>
            {
                hasError = true;
            };

            await page.GoToAsync(BaseAddress);
            await page.ClickAsync("#" + id);

            await page.WaitForTimeoutAsync(1000);

            hasError.Should().Be(shouldError);
            appInsightsCalls.Should().Be(expectedCalls);
        }

        [Theory]
        [InlineData("TrackEvent", false)]
        [InlineData("TrackTrace", false)]
        [InlineData("TrackException", false)]
        [InlineData("TrackGlobalException", false)]
        [InlineData("SetAuthenticatedUserContext", false)]
        [InlineData("ClearAuthenticatedUserContext", false)]
        [InlineData("StartStopTrackPage", false)]
        [InlineData("TrackDependencyData", false)]
        [InlineData("TrackMetric", false)]
        [InlineData("TrackPageView", false)]
        [InlineData("TrackPageViewPerformance", false)]
        [InlineData("TestLogger", false)]
        [InlineData("StartStopTrackEvent", false)]
        public async Task TestBlocked(string id, bool shouldError)
        {
            bool hasError = false;

            using var playwright = await Playwright.CreateAsync();

            await using var browser = await playwright.Chromium.LaunchAsync(Headless);
            var page = await browser.NewPageAsync();

            page.Console += (sender, e) =>
            {
                if (e.Message.Type == "error" && e.Message.Text != "Failed to load resource: net::ERR_FAILED" && !e.Message.Text.Contains("Something wrong happened :("))
                {
                    hasError = true;
                }
            };

            await page.RouteAsync("https://dc.services.visualstudio.com/v2/track", async (x, y )  => {
                await x.AbortAsync();
            });

            await page.RouteAsync("https://js.monitor.azure.com/scripts/b/ai.2.min.js", async (x, y) => {
                await x.AbortAsync();
            });

            page.RequestFailed += (sender, e) =>
            {
                if (e.Request.Url != "https://js.monitor.azure.com/scripts/b/ai.2.min.js" && e.Request.Url != "https://dc.services.visualstudio.com/v2/track")
                {
                    hasError = true;
                }
            };

            await page.GoToAsync(BaseAddress);
            await page.ClickAsync("#" + id);

            await page.WaitForTimeoutAsync(1000);

            hasError.Should().Be(shouldError);
        }
    }
}
