[![Demo](https://img.shields.io/badge/Live-Demo-Blue?style=flat-square)](https://BlazorApplicationInsights.netlify.app/)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/BlazorApplicationInsights.svg?style=flat-square)](https://www.nuget.org/packages/BlazorApplicationInsights)
[![Nuget (with prereleases)](https://img.shields.io/nuget/dt/BlazorApplicationInsights.svg?style=flat-square)](https://www.nuget.org/packages/BlazorApplicationInsights)
![](https://github.com/IvanJosipovic/BlazorApplicationInsights/workflows/CI/CD/badge.svg)

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
- Add Application Insights JS to index.html
  - [Source](https://docs.microsoft.com/en-us/azure/azure-monitor/app/javascript#snippet-based-setup)

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