[![Demo](https://img.shields.io/badge/Live-Demo-Blue?style=flat-square)](https://BlazorApplicationInsights.netlify.app/)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/BlazorApplicationInsights.svg?style=flat-square)](https://www.nuget.org/packages/BlazorApplicationInsights)
[![Nuget (with prereleases)](https://img.shields.io/nuget/dt/BlazorApplicationInsights.svg?style=flat-square)](https://www.nuget.org/packages/BlazorApplicationInsights)
![](https://github.com/IvanJosipovic/BlazorApplicationInsights/workflows/Create%20Release/badge.svg)

Application Insights for Blazor web applications

[Sample Projects](https://github.com/IvanJosipovic/BlazorApplicationInsights/tree/master/samples)

## Features

- .NET 6/7/8 and Blazor Web App Support
- Automatically triggers Track Page View on route changes
- ILoggerProvider which sends all the logs to App Insights (Wasm only)
- Programmatically update settings, including the Connection String
- Supported [APIs](https://github.com/microsoft/ApplicationInsights-JS/blob/master/API-reference.md)
  - AddTelemetryInitializer
  - ClearAuthenticatedUserContext
  - Context
  - Flush
  - GetCookieMgr
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
  - UpdateCfg

## Install on Blazor Web App

- Add [BlazorApplicationInsights NuGet](https://www.nuget.org/packages/BlazorApplicationInsights) to the Client project
  - ```dotnet add package BlazorApplicationInsights```
- Add call to Program.cs in the root and Client project if one exists

  ```csharp
  builder.Services.AddBlazorApplicationInsights(x =>
  {
      x.ConnectionString = "{Insert Connection String}";
  });
  ```

- Add using statement to _Imports.razor
  - ```@using BlazorApplicationInsights;```
- Add component below \<base href="/" /> in App.razor
  - **NOTE:** Interactivity is necessary only when the optional onAppInsightsInit callback is configured as part of the AddBlazorApplicationInsights setup.
  - ```<ApplicationInsightsInit @rendermode="@InteractiveAuto" />```

  ```html
  <!DOCTYPE html>
  <html lang="en">

  <head>
      <meta charset="utf-8" />
      <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
      <base href="/" />
      <ApplicationInsightsInit @rendermode="@InteractiveAuto" />
      <link rel="stylesheet" href="bootstrap/bootstrap.min.css" />
      <link rel="stylesheet" href="app.css" />
      <link rel="stylesheet" href="BlazorApplicationInsights.WebApp.Sample.styles.css" />
      <link rel="icon" type="image/png" href="favicon.png" />
      <HeadOutlet @rendermode="@RenderMode.InteractiveAuto" />
  </head>
  ```

## Install on Blazor WebAssembly Standalone App

- Add [BlazorApplicationInsights NuGet](https://www.nuget.org/packages/BlazorApplicationInsights) to the Client project
  - ```dotnet add package BlazorApplicationInsights```
- Add call to Program.cs and set **both** the ConnectionString and InstrumentationKey

  ```csharp
  builder.Services.AddBlazorApplicationInsights(config =>
  {
      config.ConnectionString = "{Insert Connection String}";
      config.InstrumentationKey = "{Insert Instrumentation Key}";
  });
  ```

- Add using statement to _Imports.razor
  - ```@using BlazorApplicationInsights;```
- Add component to the top of App.razor
  - ```<ApplicationInsightsInit IsWasmStandalone="true" />```
- Add Application Insights JS to head below \<base href="/" /> in index.html
  - **Note:** Leave InstrumentationKey zero'd out and DisableTelemetry as true, they will be updated by Blazor on startup
  ```html
  <script type="text/javascript">
    !(function (cfg){function e(){cfg.onInit&&cfg.onInit(i)}var S,u,D,t,n,i,C=window,x=document,w=C.location,I="script",b="ingestionendpoint",E="disableExceptionTracking",A="ai.device.";"instrumentationKey"[S="toLowerCase"](),u="crossOrigin",D="POST",t="appInsightsSDK",n=cfg.name||"appInsights",(cfg.name||C[t])&&(C[t]=n),i=C[n]||function(l){var d=!1,g=!1,f={initialize:!0,queue:[],sv:"7",version:2,config:l};function m(e,t){var n={},i="Browser";function a(e){e=""+e;return 1===e.length?"0"+e:e}return n[A+"id"]=i[S](),n[A+"type"]=i,n["ai.operation.name"]=w&&w.pathname||"_unknown_",n["ai.internal.sdkVersion"]="javascript:snippet_"+(f.sv||f.version),{time:(i=new Date).getUTCFullYear()+"-"+a(1+i.getUTCMonth())+"-"+a(i.getUTCDate())+"T"+a(i.getUTCHours())+":"+a(i.getUTCMinutes())+":"+a(i.getUTCSeconds())+"."+(i.getUTCMilliseconds()/1e3).toFixed(3).slice(2,5)+"Z",iKey:e,name:"Microsoft.ApplicationInsights."+e.replace(/-/g,"")+"."+t,sampleRate:100,tags:n,data:{baseData:{ver:2}},ver:4,seq:"1",aiDataContract:undefined}}var h=-1,v=0,y=["js.monitor.azure.com","js.cdn.applicationinsights.io","js.cdn.monitor.azure.com","js0.cdn.applicationinsights.io","js0.cdn.monitor.azure.com","js2.cdn.applicationinsights.io","js2.cdn.monitor.azure.com","az416426.vo.msecnd.net"],k=l.url||cfg.src;if(k){if((n=navigator)&&(~(n=(n.userAgent||"").toLowerCase()).indexOf("msie")||~n.indexOf("trident/"))&&~k.indexOf("ai.3")&&(k=k.replace(/(\/)(ai\.3\.)([^\d]*)$/,function(e,t,n){return t+"ai.2"+n})),!1!==cfg.cr)for(var e=0;e<y.length;e++)if(0<k.indexOf(y[e])){h=e;break}var i=function(e){var a,t,n,i,o,r,s,c,p,u;f.queue=[],g||(0<=h&&v+1<y.length?(a=(h+v+1)%y.length,T(k.replace(/^(.*\/\/)([\w\.]*)(\/.*)$/,function(e,t,n,i){return t+y[a]+i})),v+=1):(d=g=!0,o=k,c=(p=function(){var e,t={},n=l.connectionString;if(n)for(var i=n.split(";"),a=0;a<i.length;a++){var o=i[a].split("=");2===o.length&&(t[o[0][S]()]=o[1])}return t[b]||(e=(n=t.endpointsuffix)?t.location:null,t[b]="https://"+(e?e+".":"")+"dc."+(n||"services.visualstudio.com")),t}()).instrumentationkey||l.instrumentationKey||"",p=(p=p[b])?p+"/v2/track":l.endpointUrl,(u=[]).push((t="SDK LOAD Failure: Failed to load Application Insights SDK script (See stack for details)",n=o,r=p,(s=(i=m(c,"Exception")).data).baseType="ExceptionData",s.baseData.exceptions=[{typeName:"SDKLoadFailed",message:t.replace(/\./g,"-"),hasFullStack:!1,stack:t+"\nSnippet failed to load ["+n+"] -- Telemetry is disabled\nHelp Link: https://go.microsoft.com/fwlink/?linkid=2128109\nHost: "+(w&&w.pathname||"_unknown_")+"\nEndpoint: "+r,parsedStack:[]}],i)),u.push((s=o,t=p,(r=(n=m(c,"Message")).data).baseType="MessageData",(i=r.baseData).message='AI (Internal): 99 message:"'+("SDK LOAD Failure: Failed to load Application Insights SDK script (See stack for details) ("+s+")").replace(/\"/g,"")+'"',i.properties={endpoint:t},n)),o=u,c=p,JSON&&((r=C.fetch)&&!cfg.useXhr?r(c,{method:D,body:JSON.stringify(o),mode:"cors"}):XMLHttpRequest&&((s=new XMLHttpRequest).open(D,c),s.setRequestHeader("Content-type","application/json"),s.send(JSON.stringify(o))))))},a=function(e,t){g||setTimeout(function(){!t&&f.core||i()},500),d=!1},T=function(e){var n=x.createElement(I),e=(n.src=e,cfg[u]);return!e&&""!==e||"undefined"==n[u]||(n[u]=e),n.onload=a,n.onerror=i,n.onreadystatechange=function(e,t){"loaded"!==n.readyState&&"complete"!==n.readyState||a(0,t)},cfg.ld&&cfg.ld<0?x.getElementsByTagName("head")[0].appendChild(n):setTimeout(function(){x.getElementsByTagName(I)[0].parentNode.appendChild(n)},cfg.ld||0),n};T(k)}try{f.cookie=x.cookie}catch(p){}function t(e){for(;e.length;)!function(t){f[t]=function(){var e=arguments;d||f.queue.push(function(){f[t].apply(f,e)})}}(e.pop())}var r,s,n="track",o="TrackPage",c="TrackEvent",n=(t([n+"Event",n+"PageView",n+"Exception",n+"Trace",n+"DependencyData",n+"Metric",n+"PageViewPerformance","start"+o,"stop"+o,"start"+c,"stop"+c,"addTelemetryInitializer","setAuthenticatedUserContext","clearAuthenticatedUserContext","flush"]),f.SeverityLevel={Verbose:0,Information:1,Warning:2,Error:3,Critical:4},(l.extensionConfig||{}).ApplicationInsightsAnalytics||{});return!0!==l[E]&&!0!==n[E]&&(t(["_"+(r="onerror")]),s=C[r],C[r]=function(e,t,n,i,a){var o=s&&s(e,t,n,i,a);return!0!==o&&f["_"+r]({message:e,url:t,lineNumber:n,columnNumber:i,error:a,evt:C.event}),o},l.autoExceptionInstrumented=!0),f}(cfg.cfg),(C[n]=i).queue&&0===i.queue.length?(i.queue.push(e),i.trackPageView({})):e();})({
        src: "https://js.monitor.azure.com/scripts/b/ai.3.gbl.min.js",
        ld: -1,
        crossOrigin: "anonymous",
        cfg: {
            instrumentationKey: "00000000-0000-0000-0000-000000000000",
            disableTelemetry: true
        }
    });
  </script>
  ```

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

    builder.Services.AddBlazorApplicationInsights(config =>
    {
        config.ConnectionString = "{Insert Connection String}";
    },
    async applicationInsights =>
    {
        var telemetryItem = new TelemetryItem()
        {
            Tags = new Dictionary<string, object?>()
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
