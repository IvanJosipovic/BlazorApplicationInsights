[![Demo](https://img.shields.io/badge/Live-Demo-Blue?style=flat-square)](https://BlazorApplicationInsights.netlify.app/)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/BlazorApplicationInsights.svg?style=flat-square)](https://www.nuget.org/packages/BlazorApplicationInsights)
[![Nuget (with prereleases)](https://img.shields.io/nuget/dt/BlazorApplicationInsights.svg?style=flat-square)](https://www.nuget.org/packages/BlazorApplicationInsights)
![](https://github.com/IvanJosipovic/BlazorApplicationInsights/workflows/Create%20Release/badge.svg)

Blazor Application Insights

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
  - [Source](https://docs.microsoft.com/en-us/azure/azure-monitor/app/javascript#snippet-based-setup)
  - Set 'ld: -1' so that the page will be blocked until the JS is loaded
    - Example
      ```
      <script type="text/javascript">
        !function(T,l,y){// Removed for brevity}
        src: "https://az416426.vo.msecnd.net/scripts/b/ai.2.min.js",
        ld: -1, // Set this to -1
        cfg: {
            instrumentationKey: "YOUR_INSTRUMENTATION_KEY_GOES_HERE"
        }});
      </script>
      ```

## [Example Project](https://github.com/IvanJosipovic/BlazorApplicationInsights/tree/master/src/BlazorApplicationInsights.Sample)

# Features
 - Automatically triggers Track Page View on route changes
 - Supported [APIs](https://github.com/microsoft/ApplicationInsights-JS/blob/master/API-reference.md#addTelemetryInitializer)
   - ClearAuthenticatedUserContext
   - Flush
   - StartTrackPage
   - StopTrackPage
   - SetAuthenticatedUserContext
   - TrackMetric
   - TrackDependencyData
   - TrackEvent
   - TrackException
   - TrackPageView
   - TrackTrace
 - Todo
   - AddTelemetryInitializer


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
