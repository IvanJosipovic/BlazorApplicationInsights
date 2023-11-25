using FluentAssertions;
using Microsoft.Playwright;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace BlazorApplicationInsights.Tests
{
    public class UnitTests
    {
        private readonly ITestOutputHelper output;

        private string BaseAddress = "https://localhost:5001/";
        private bool Headless = true;

        public UnitTests(ITestOutputHelper output)
        {
            this.output = output;

            string filename = "BrowserTestsAddress.config";
            if (File.Exists(filename))
            {
                BaseAddress = File.ReadAllText(filename);
            }
        }

        private List<AIRequestObject> GetData(string json)
        {
            return JsonConvert.DeserializeObject<List<AIRequestObject>>(json);
        }

        public static IEnumerable<object[]> GetTests()
        {
            return new List<object[]>
            {
                new object[] { "TrackEvent", 1000, new List<AIRequestObject>()
                {
                    new()
                    {
                        data = new Data()
                        {
                            baseType = "EventData",
                            baseData = new Basedata()
                            {
                                name = "My Event",
                                properties = new Dictionary<string, string>()
                                {
                                    { "customProperty", "customValue" }
                                }
                            }
                        }
                    }
                }},
                new object[] { "TrackTrace", 1000, new List<AIRequestObject>()
                {
                    new()
                    {
                        data = new Data()
                        {
                            baseType = "MessageData",
                            baseData = new Basedata()
                            {
                                message = "myMessage"
                            }
                        }
                    },
                    new()
                    {
                        data = new Data()
                        {
                            baseType = "MessageData",
                            baseData = new Basedata()
                            {
                                message = "myMessage1",
                                severityLevel = 4
                            }
                        }
                    },
                    new()
                    {
                        data = new Data()
                        {
                            baseType = "MessageData",
                            baseData = new Basedata()
                            {
                                message = "myMessage2",
                                severityLevel = 4,
                                properties = new Dictionary<string, string>()
                                {
                                    { "customProperty", "customValue" }
                                }
                            }
                        }
                    }
                }},
                new object[] { "TrackException", 1000, new List<AIRequestObject>()
                {
                    new()
                    {
                        data = new ()
                        {
                            baseType = "ExceptionData",
                            baseData = new Basedata()
                            {
                                exceptions = new []
                                {
                                    new Exception()
                                    {
                                        typeName = "my error",
                                        message = "my error: my message",
                                        hasFullStack = false,
                                        stack = "my message\n"
                                    }
                                },
                                severityLevel = 3,
                                properties = new Dictionary<string, string>()
                                {
                                    { "customProperty", "customValue" },
                                    { "typeName", "my error" }
                                }
                            }
                        }
                    }
                }},
                new object[] { "TrackGlobalException", 20000, new List<AIRequestObject>()
                {
                    new()
                    {
                        data = new Data()
                        {
                            baseType = "ExceptionData",
                            baseData = new Basedata()
                            {
                                exceptions = new []
                                {
                                    new Exception()
                                    {
                                        typeName = "NotImplementedException",
                                        message = "NotImplementedException: Something wrong happened :(",
                                        hasFullStack = false,
                                    }
                                },
                                severityLevel = 4,
                                properties = new Dictionary<string, string>()
                                {
                                    { "CategoryName", "Microsoft.AspNetCore.Components.WebAssembly.Rendering.WebAssemblyRenderer" },
                                    { "EventId", "100" },
                                    { "EventName", "ExceptionRenderingComponent" },
                                    { "Message", "Something wrong happened :(" },
                                    { "OriginalFormat", "Unhandled exception rendering component: {Message}" },
                                    { "typeName", "NotImplementedException" }
                                }
                            }
                        }
                    }
                }},
                new object[] { "SetAuthenticatedUserContext", 1000, new List<AIRequestObject>()
                {
                    new()
                    {
                        tags = new Dictionary<string, string>()
                        {
                            { "ai.user.authUserId", "myUserId" }
                        },
                        data = new Data()
                        {
                            baseType = "EventData",
                            baseData = new Basedata()
                            {
                                name = "Auth Event"
                            }
                        }
                    }
                }},
                new object[] { "ClearAuthenticatedUserContext", 1000, new List<AIRequestObject>()
                {
                    new()
                    {
                        tags = new Dictionary<string, string>()
                        {
                            { "ai.user.authUserId", "myUserId" }
                        },
                        data = new Data()
                        {
                            baseType = "EventData",
                            baseData = new Basedata()
                            {
                                name = "Auth Event"
                            }
                        }
                    },
                    new()
                    {
                        tags = new Dictionary<string, string>()
                        {
                            { "ai.user.id", "R3DiFTEkHCFJZ+UCOWntgB" }
                        },
                        data = new Data()
                        {
                            baseType = "EventData",
                            baseData = new Basedata()
                            {
                                name = "Auth Event2"
                            }
                        }
                    }
                }},
                new object[] { "StartStopTrackPage", 1000, new List<AIRequestObject>()
                {
                    new()
                    {
                        data = new Data()
                        {
                            baseType = "PageviewData",
                            baseData = new Basedata()
                            {
                                name ="BlazorApplicationInsights",
                                url = "https://localhost:5001/",
                                properties = new Dictionary<string, string>()
                                {
                                    { "refUri", "" }
                                }
                            }
                        }
                    },
                    new()
                    {
                        data = new Data()
                        {
                            baseType = "PageviewData",
                            baseData = new Basedata()
                            {
                                name = "myPage",
                                url = "https://localhost:5001/",
                                properties = new Dictionary<string, string>()
                                {
                                    { "customProperty", "customValue" },
                                    { "refUri", "" }
                                }
                            }
                        }
                    }
                }},
                new object[] { "TrackDependencyData", 1000, new List<AIRequestObject>()
                {
                    new()
                    {
                        data = new Data()
                        {
                            baseType = "RemoteDependencyData",
                            baseData = new Basedata()
                            {
                                id = "myId.",
                                name = "myName",
                                resultCode = "200",
                                duration = "00:00:01.000",
                                success = true,
                                data = "myName",
                                target = "localhost:5001 | myContext",
                                type = "myType",
                                properties = new Dictionary<string, string>()
                                {
                                    { "customProperty", "customValue" },
                                }
                            }
                        }
                    }
                }},
                new object[] { "TrackMetric", 1000, new List<AIRequestObject>()
                {
                    new()
                    {
                        data = new Data()
                        {
                            baseType = "MetricData",
                            baseData = new Basedata()
                            {
                                metrics = new[]
                                {
                                    new Metric()
                                    {
                                        name = "myMetric",
                                        kind = 0,
                                        value = 100,
                                        count = 200,
                                        min = 1,
                                        max = 200
                                    }
                                },
                                properties = new Dictionary<string, string>()
                                {
                                    { "customProperty", "customValue" }
                                }
                            }
                        }
                    }
                }},
                new object[] { "TrackPageView", 1000, new List<AIRequestObject>()
                {
                    new()
                    {
                        data = new Data()
                        {
                            baseType = "PageviewData",
                            baseData = new Basedata()
                            {
                                name ="BlazorApplicationInsights",
                                url = "https://localhost:5001/",
                                properties = new Dictionary<string, string>()
                                {
                                    { "refUri", "" }
                                }
                            }
                        }
                    },
                    new()
                    {
                        data = new Data()
                        {
                            baseType = "PageviewData",
                            baseData = new Basedata()
                            {
                                name = "myPage",
                                url = "https://test.local",
                                properties = new Dictionary<string, string>()
                                {
                                    { "customProperty", "customValue" },
                                    { "refUri", "https://test.local" },
                                    { "pageType", "TestPage" },
                                    { "isLoggedIn", "true" },
                                }
                            }
                        }
                    }
                }},
                new object[] { "TrackPageViewPerformance", 5000, new List<AIRequestObject>()
                {
                    new()
                    {
                        data = new Data()
                        {
                            baseType = "PageviewPerformanceData",
                            baseData = new Basedata()
                            {
                                name = "myPerf",
                                url = "/test123",
                                //duration = "00:00:00.111",
                                //perfTotal = "00:00:00.222",
                                //networkConnect = "00:00:00.333",
                                //sentRequest = "00:00:00.444",
                                //receivedResponse = "00:00:00.555",
                                //domProcessing = "00:00:00.666",
                                properties = new Dictionary<string, string>()
                                {
                                    { "customProperty", "customValue" }
                                }
                            }
                        }
                    }
                }},
                new object[] { "TestLogger", 1000, new List<AIRequestObject>()
                {
                    new()
                    {
                        data = new Data()
                        {
                            baseType = "MessageData",
                            baseData = new Basedata()
                            {
                                message = "My Logging Test",
                                severityLevel = 1,
                                properties = new Dictionary<string, string>
                                {
                                    { "OriginalFormat", "My Logging Test" }
                                }
                            }
                        }
                    }
                }},
                new object[] { "TestSemanticLogger", 1000, new List<AIRequestObject>()
                {
                    new()
                    {
                        data = new Data()
                        {
                            baseType = "MessageData",
                            baseData = new Basedata()
                            {
                                message = "My Semantic Logging Test with customProperty=customValue",
                                severityLevel = 1,
                                properties = new Dictionary<string, string>
                                {
                                    { "customProperty", "customValue" },
                                    { "OriginalFormat", "My Semantic Logging Test with customProperty={customProperty}" }
                                }
                            }
                        }
                    }
                }},
                new object[] { "StartStopTrackEvent", 1000, new List<AIRequestObject>()
                {
                    new()
                    {
                        data = new Data()
                        {
                            baseType = "EventData",
                            baseData = new Basedata()
                            {
                                name = "myEvent",
                                properties = new Dictionary<string, string>
                                {
                                    { "customProperty", "customValue" }
                                }
                            }
                        }
                    }
                }},
                new object[] { "TrackHttpRequest", 60000, new List<AIRequestObject>()
                {
                    new()
                    {
                        data = new Data()
                        {
                            baseType = "RemoteDependencyData",
                            baseData = new Basedata()
                            {
                                name = "GET https://httpbin.org/get",
                                resultCode = "200",
                                success = true,
                                data = "GET https://httpbin.org/get",
                                target = "httpbin.org",
                                type = "Fetch",
                                properties = new Dictionary<string, string>()
                                {
                                    { "HttpMethod", "GET" }
                                }
                            }
                        }
                    }
                }},
            };
        }

        [Theory]
        [MemberData(nameof(GetTests))]
        public async Task Test(string id, int timeout, List<AIRequestObject> expectedCalls)
        {
            bool hasError = false;
            int validCalls = 0;
            List<AIRequestObject> requestData = new();

            using var playwright = await Playwright.CreateAsync();

            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions() { Headless = Headless });
            var page = await browser.NewPageAsync(new BrowserNewPageOptions() { IgnoreHTTPSErrors = true });

            page.Console += (sender, e) =>
            {
                if (e.Type == "error" && !e.Text.Contains("Something wrong happened :("))
                {
                    hasError = true;
                }
            };

            page.Request += (sender, e) =>
            {
                if (e.Url.Equals("https://dc.services.visualstudio.com/v2/track"))
                {
                    if (!string.IsNullOrEmpty(e.PostData))
                    {
                        requestData.AddRange(GetData(e.PostData));
                    }
                }
            };

            page.RequestFailed += (sender, e) =>
            {
                if (!e.Url.Equals("https://localhost:5001/_framework/blazor-hotreload"))
                {
                    hasError = true;
                }
            };

            await page.Context.AddCookiesAsync(new List<Cookie>() { new() { Url = BaseAddress, Secure = true, Name = "ai_user", Value = "R3DiFTEkHCFJZ+UCOWntgB|2021-10-06T03:25:18.134Z" } });

            await page.GotoAsync(BaseAddress);

            await page.WaitForLoadStateAsync();

            await page.ClickAsync("#" + id);

            await page.WaitForTimeoutAsync(timeout);

            for (int i = 0; i < expectedCalls.Count; i++)
            {
                var expectedCall = expectedCalls[i];
                var call = requestData.Where(x => x.data.baseType == expectedCall.data.baseType
                                               && x.tags.ContainsKey("ai.cloud.roleInstance")
                                               && (x.tags["ai.cloud.roleInstance"] == "Blazor Wasm") || x.tags["ai.cloud.roleInstance"] == "Blazor Server")
                                                .ToArray()[i];

                var compare = CompareObjects(expectedCall, call);

                if (compare)
                {
                    validCalls++;
                }
                else
                {
                    hasError = true;
                    var calls = JsonConvert.SerializeObject(requestData, new JsonSerializerSettings() { Formatting = Formatting.Indented });
                    output.WriteLine(calls);
                }
            }

            hasError.Should().Be(false);
            validCalls.Should().Be(expectedCalls.Count);
        }

        internal bool CompareObjects(object sourceObj, object desinationObj)
        {
            foreach (PropertyInfo propertyInfo in sourceObj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                try
                {
                    var source = propertyInfo.GetValue(sourceObj, null);
                    var dest = propertyInfo.GetValue(desinationObj, null);

                    if (source == null) continue;

                    if (typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType) || propertyInfo.PropertyType.IsPrimitive || propertyInfo.PropertyType.IsValueType)
                    {
                        if (!source.Equals(dest))
                        {
                            return false;
                        }
                    }
                    else if (typeof(IDictionary).IsAssignableFrom(propertyInfo.PropertyType))
                    {
                        var destinationDict = (Dictionary<string, string>)dest;
                        foreach (KeyValuePair<string, string> entry in (Dictionary<string, string>)source)
                        {
                            if (destinationDict[entry.Key] != entry.Value)
                            {
                                return false;
                            }
                        }
                    }
                    else if (propertyInfo.PropertyType.IsArray || propertyInfo.PropertyType.GetInterface(nameof(IEnumerable)) != null)
                    {
                        var collectionItems1 = ((IEnumerable)source).Cast<object>();
                        var collectionItems2 = ((IEnumerable)dest).Cast<object>();
                        var collectionItemsCount1 = collectionItems1.Count();

                        for (int i = 0; i < collectionItemsCount1; i++)
                        {
                            if (!CompareObjects(collectionItems1.ElementAt(i), collectionItems2.ElementAt(i)))
                            {
                                return false;
                            }
                        }
                    }
                    else if (propertyInfo.PropertyType.IsClass)
                    {
                        if (!CompareObjects(source, dest))
                        {
                            return false;
                        }
                    }
                }
                catch (System.Exception)
                {
                    return false;
                }
            }

            return true;
        }

        [Theory]
        [InlineData("TrackEvent")]
        [InlineData("TrackTrace")]
        [InlineData("TrackException")]
        [InlineData("TrackGlobalException")]
        [InlineData("SetAuthenticatedUserContext")]
        [InlineData("ClearAuthenticatedUserContext")]
        [InlineData("StartStopTrackPage")]
        [InlineData("TrackDependencyData")]
        [InlineData("TrackMetric")]
        [InlineData("TrackPageView")]
        [InlineData("TrackPageViewPerformance")]
        [InlineData("TestLogger")]
        [InlineData("TestSemanticLogger")]
        [InlineData("StartStopTrackEvent")]
        [InlineData("TrackHttpRequest")]
        [InlineData("GetUserId")]
        [InlineData("GetSessionId")]
        [InlineData("EnableCookies")]
        [InlineData("DisableCookies")]
        [InlineData("GetCookiesEnabled")]
        public async Task TestBlocked(string id)
        {
            bool hasError = false;

            using var playwright = await Playwright.CreateAsync();

            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions() { Headless = Headless });
            var page = await browser.NewPageAsync(new BrowserNewPageOptions() { IgnoreHTTPSErrors = true });

            page.Console += (sender, e) =>
            {
                if (e.Type == "error" && e.Text != "Failed to load resource: net::ERR_FAILED" && !e.Text.Contains("Something wrong happened :("))
                {
                    hasError = true;
                }
            };

            await page.RouteAsync("https://dc.services.visualstudio.com/v2/track", async (x) => await x.AbortAsync());

            await page.RouteAsync("https://js.monitor.azure.com/scripts/b/ai.3.gbl.min.js", async (x) => await x.AbortAsync());

            page.RequestFailed += (sender, e) =>
            {
                if (e.Url != "https://js.monitor.azure.com/scripts/b/ai.3.gbl.min.js" &&
                    e.Url != "https://dc.services.visualstudio.com/v2/track" &&
                    e.Url != "https://localhost:5001/_framework/blazor-hotreload")
                {
                    hasError = true;
                }
            };

            await page.GotoAsync(BaseAddress);
            await page.ClickAsync("#" + id);

            await page.WaitForTimeoutAsync(1000);

            hasError.Should().Be(false);
        }

        [Fact]
        public async Task GetUserId_ShouldReturnAGeneratedValue_WhenNotSet()
        {
            using var playwright = await Playwright.CreateAsync();

            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions() { Headless = Headless });
            var page = await browser.NewPageAsync(new BrowserNewPageOptions() { IgnoreHTTPSErrors = true });

            await page.GotoAsync(BaseAddress);
            await page.ClickAsync("#GetUserId");

            await page.WaitForTimeoutAsync(1000);

            var userId = (await page.Locator("#UserId").AllInnerTextsAsync()).FirstOrDefault();

            userId.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetUserId_ShouldReturnSetUserId_WhenSet()
        {
            using var playwright = await Playwright.CreateAsync();

            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions() { Headless = Headless });
            var page = await browser.NewPageAsync(new BrowserNewPageOptions() { IgnoreHTTPSErrors = true });

            await page.GotoAsync(BaseAddress);
            await page.ClickAsync("#SetAuthenticatedUserContext");

            await page.WaitForTimeoutAsync(1000);
            await page.ClickAsync("#GetUserId");

            await page.WaitForTimeoutAsync(1000);

            var userId = (await page.Locator("#UserId").AllInnerTextsAsync()).FirstOrDefault();

            userId.Should().Be("myUserId");
        }

        [Fact]
        public async Task GetSessionId_ShouldReturnAGeneratedValue()
        {
            using var playwright = await Playwright.CreateAsync();

            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions() { Headless = Headless });
            var page = await browser.NewPageAsync(new BrowserNewPageOptions() { IgnoreHTTPSErrors = true });

            await page.GotoAsync(BaseAddress);
            await page.ClickAsync("#GetSessionId");

            await page.WaitForTimeoutAsync(1000);

            var sessionId = (await page.Locator("#SessionId").AllInnerTextsAsync()).FirstOrDefault();

            sessionId.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task SetCookiesEnabled_False_ShouldDisableCookies()
        {
            using var playwright = await Playwright.CreateAsync();

            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions() { Headless = Headless });
            var page = await browser.NewPageAsync(new BrowserNewPageOptions() { IgnoreHTTPSErrors = true });

            await page.GotoAsync(BaseAddress);
            await page.ClickAsync("#DisableCookies");

            await page.WaitForTimeoutAsync(1000);

            await page.ClickAsync("#GetCookiesEnabled");
            var enabled = (await page.Locator("#CookiesEnabled").AllInnerTextsAsync()).FirstOrDefault();

            enabled.Should().Be("False");
        }


        [Fact]
        public async Task SetCookiesEnabled_True_ShouldEnableCookies()
        {
            using var playwright = await Playwright.CreateAsync();

            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions() { Headless = Headless });
            var page = await browser.NewPageAsync(new BrowserNewPageOptions() { IgnoreHTTPSErrors = true });

            await page.GotoAsync(BaseAddress);
            await page.ClickAsync("#DisableCookies");
            await page.WaitForTimeoutAsync(1000);

            await page.ClickAsync("#EnableCookies");
            await page.WaitForTimeoutAsync(1000);

            await page.ClickAsync("#GetCookiesEnabled");
            var enabled = (await page.Locator("#CookiesEnabled").AllInnerTextsAsync()).FirstOrDefault();

            enabled.Should().Be("True");
        }

    }
}
