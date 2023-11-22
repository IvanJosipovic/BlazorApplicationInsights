[![Demo](https://img.shields.io/badge/Live-Demo-Blue?style=flat-square)](https://BlazorApplicationInsights.netlify.app/)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/BlazorApplicationInsights.svg?style=flat-square)](https://www.nuget.org/packages/BlazorApplicationInsights)
[![Nuget (with prereleases)](https://img.shields.io/nuget/dt/BlazorApplicationInsights.svg?style=flat-square)](https://www.nuget.org/packages/BlazorApplicationInsights)
![](https://github.com/IvanJosipovic/BlazorApplicationInsights/workflows/Create%20Release/badge.svg)

Application Insights for Blazor web applications

[Sample Projects](https://github.com/IvanJosipovic/BlazorApplicationInsights/tree/master/samples)

## Features

- Automatically triggers Track Page View on route changes
- ILoggerProvider which sends all the logs to App Insights (Wasm only)
- Supported [APIs](https://github.com/microsoft/ApplicationInsights-JS/blob/master/API-reference.md)
  - AddTelemetryInitializer
  - ClearAuthenticatedUserContext
  - Flush
  - SetAuthenticatedUserContext
  - StartTrackEvent
  - StartTrackPage
  - StopTrackEvent
  - StopTrackPage
  - TrackDependencyData
  - TrackEvent
  - TrackException
  - TrackMetric
  - TrackPageView
  - TrackPageViewPerformance
  - TrackTrace

## Install on Blazor Web App

- Add [BlazorApplicationInsights NuGet](https://www.nuget.org/packages/BlazorApplicationInsights) to the Client project
  - dotnet add package BlazorApplicationInsights
- Add call to Program.cs

  ```csharp
  builder.Services.AddBlazorApplicationInsights(x =>
  {
      x.ConnectionString = "{Insert Connection String}";
  });
  ```

- Add using statement to _Imports.razor
  - ```@using BlazorApplicationInsights;```
- Add component below \<base href="/" /> in App.razor
  - ```<ApplicationInsightsInit />```

  ```csharp
  <!DOCTYPE html>
  <html lang="en">

  <head>
      <meta charset="utf-8" />
      <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
      <base href="/" />
      <ApplicationInsightsInit/>
      <link rel="stylesheet" href="bootstrap/bootstrap.min.css" />
      <link rel="stylesheet" href="app.css" />
      <link rel="stylesheet" href="BlazorApplicationInsights.WebApp.Sample.styles.css" />
      <link rel="icon" type="image/png" href="favicon.png" />
      <HeadOutlet @rendermode="@RenderMode.InteractiveAuto" />
  </head>
  ```


## Install on Blazor WebAssembly Standalone App



## TrackEvent

```razor
@page "/"

<button class="btn btn-primary" @onclick="TrackEvent">Track Event</button>

@code {
    [Inject] private IApplicationInsights AppInsights { get; set; }

    private async Task TrackEvent()
    {
        await AppInsights.TrackEvent(new EventTelemetry() { Name = "My Event" });
    }
}
```

## Set User Name
- Edit Authentication.razor
```razor
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

## Set Role and Instance
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
