[![Demo](https://img.shields.io/badge/Live-Demo-Blue?style=flat-square)](https://BlazorApplicationInsights.netlify.app/)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/BlazorApplicationInsights.svg?style=flat-square)](https://www.nuget.org/packages/BlazorApplicationInsights)
[![Nuget (with prereleases)](https://img.shields.io/nuget/dt/BlazorApplicationInsights.svg?style=flat-square)](https://www.nuget.org/packages/BlazorApplicationInsights)
![](https://github.com/IvanJosipovic/BlazorApplicationInsights/workflows/Create%20Release/badge.svg)

Application Insights for Blazor web applications

# Install

- Add [BlazorApplicationInsights Nuget](https://www.nuget.org/packages/BlazorApplicationInsights)
  - dotnet add package BlazorApplicationInsights
- Add call to Program.cs
  - ```builder.Services.AddBlazorApplicationInsights();```
- Add using statement to _Imports.razor
  - ```@using BlazorApplicationInsights;```
- Add component to App.razor
  - ```<ApplicationInsightsComponent />```
- Add Application Insights JS to head in index.html
  - [Source](https://github.com/microsoft/ApplicationInsights-JS#snippet-setup-ignore-if-using-npm-setup)
  - Set 'ld: -1' so that the page will be blocked until the JS is loaded and enter your instrumentationKey
    ```html
    <script type="text/javascript">
    !function(T,l,y){ // Removed for brevity...
    src: "https://js.monitor.azure.com/scripts/b/ai.2.min.js", 
    ld: -1,  // Set this to -1
    crossOrigin: "anonymous",
    cfg: {
        instrumentationKey: "YOUR_INSTRUMENTATION_KEY_GOES_HERE"
    }});
    </script>
    ```
- Add JS Interop to the bottom of body in index.html
  ```html
  <script src="_content/BlazorApplicationInsights/JsInterop.js"></script>
  ```

## [Example Project](https://github.com/IvanJosipovic/BlazorApplicationInsights/tree/master/src/BlazorApplicationInsights.Sample)

# Features
- Automatically triggers Track Page View on route changes
- ILoggerProvider which sends all the logs to App Insights
- Supported [APIs](https://github.com/microsoft/ApplicationInsights-JS/blob/master/API-reference.md)
  - AddTelemetryInitializer
  - ClearAuthenticatedUserContext
  - Flush
  - SetAuthenticatedUserContext
  - StartTrackPage
  - StopTrackPage
  - TrackDependencyData
  - TrackEvent
  - TrackException
  - TrackMetric
  - TrackPageView
  - TrackPageViewPerformance
  - TrackTrace

# TrackEvent

```csharp
@page "/"

<button class="btn btn-primary" @onclick="TrackEvent">Track Event</button>

@code {
    [Inject] private IApplicationInsights AppInsights { get; set; }

    private async Task TrackEvent()
    {
        await AppInsights.TrackEvent("My Event");
    }
}
```

# Set User Name
- Edit Authentication.razor
```csharp
@page "/authentication/{action}"

<RemoteAuthenticatorView Action="@Action" OnLogInSucceeded="OnLogInSucceeded" OnLogOutSucceeded="OnLogOutSucceeded" />

@code{
    [Parameter] public string Action { get; set; }

    [CascadingParameter] public Task<AuthenticationState> AuthenticationState { get; set; }

    [Inject] private IApplicationInsights AppInsights { get; set; }

    public async Task OnLogInSucceeded()
    {
        var user = (await AuthenticationState).User;

        await AppInsights.SetAuthenticatedUserContext(user.FindFirst("preferred_username")?.Value);
    }

    public async Task OnLogOutSucceeded()
    {
        await AppInsights.ClearAuthenticatedUserContext();
    }
}
```

# Set Role and Instance
- Edit Program.cs
```csharp
public static async Task Main(string[] args)
{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("app");

    builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

    builder.Services.AddBlazorApplicationInsights(async applicationInsights =>
    {
        var telemetryItem = new TelemetryItem()
        {
            Tags = new Dictionary<string, object>()
            {
                { "ai.cloud.role", "SPA" },
                { "ai.cloud.roleInstance", "Blazor Wasm" },
            }
        };

        await applicationInsights.AddTelemetryInitializer(telemetryItem);
    });

    await builder.Build().RunAsync();
}

```
