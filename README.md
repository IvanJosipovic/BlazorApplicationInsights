[![Demo](https://img.shields.io/badge/Live-Demo-Blue?style=flat-square)](https://BlazorApplicationInsights.netlify.app/)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/BlazorApplicationInsights.svg?style=flat-square)](https://www.nuget.org/packages/BlazorApplicationInsights)
[![Nuget (with prereleases)](https://img.shields.io/nuget/dt/BlazorApplicationInsights.svg?style=flat-square)](https://www.nuget.org/packages/BlazorApplicationInsights)
![](https://github.com/IvanJosipovic/BlazorApplicationInsights/workflows/CI/CD/badge.svg)

Blazor Application Insights

# Install

- Add [BlazorApplicationInsights Nuget](https://www.nuget.org/packages/BlazorApplicationInsights)
  - dotnet add package BlazorApplicationInsights
- Add AddBlazorApplicationInsights() to ConfigureServices
- Add '@using BlazorApplicationInsights;' to _Imports.razor
- Add ApplicationInsightsComponent to App.razor
- Add Application Insights JS to index.html
  - [Source](https://docs.microsoft.com/en-us/azure/azure-monitor/app/javascript#snippet-based-setup)

# Features
 - Automatically triggers Track Page View on route changes
 - Supported [APIs](https://github.com/microsoft/ApplicationInsights-JS/blob/master/API-reference.md#addTelemetryInitializer)
   - TrackEvent
   - TrackPageView
   - TrackTrace
 - Todo
   - AddTelemetryInitializer
   - ClearAuthenticatedUserContext
   - Flush
   - StartTrackPage
   - StopTrackPage
   - SetAuthenticatedUserContext
   - TrackMetric
   - TrackException
   - TrackDependencyData

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