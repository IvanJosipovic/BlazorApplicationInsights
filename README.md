[![Demo](https://img.shields.io/badge/Live-Demo-Blue?style=flat-square)](https://BlazorApplicationInsights.netlify.app/)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/BlazorApplicationInsights.svg?style=flat-square)](https://www.nuget.org/packages/BlazorApplicationInsights)
[![Nuget (with prereleases)](https://img.shields.io/nuget/dt/BlazorApplicationInsights.svg?style=flat-square)](https://www.nuget.org/packages/BlazorApplicationInsights)
![](https://github.com/IvanJosipovic/BlazorApplicationInsights/workflows/Create%20Release/badge.svg)

Application Insights for Blazor web applications

[Sample Projects](https://github.com/IvanJosipovic/BlazorApplicationInsights/tree/master/samples)

## Features

- .NET 8/9 and Blazor Web App Support
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
- Add call to Program.cs and set the ConnectionString

  ```csharp
  builder.Services.AddBlazorApplicationInsights(config =>
  {
      config.ConnectionString = "{Insert Connection String}";
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
    !(function (cfg){var k,x,D,E,L,C,b,U,O,A,e,t="track",n="TrackPage",i="TrackEvent",I=[t+"Event",t+"Exception",t+"PageView",t+"PageViewPerformance","addTelemetryInitializer",t+"Trace",t+"DependencyData",t+"Metric","start"+n,"stop"+n,"start"+i,"stop"+i,"setAuthenticatedUserContext","clearAuthenticatedUserContext","flush"];function a(){cfg.onInit&&cfg.onInit(e)}k=window,x=document,D=k.location,E="script",L="ingestionendpoint",C="disableExceptionTracking",b="crossOrigin",U="POST",O=cfg.pn||"aiPolicy",t="appInsightsSDK",A=cfg.name||"appInsights",(cfg.name||k[t])&&(k[t]=A),e=k[A]||function(u){var n=u.url||cfg.src,s=!1,p=!1,l={initialize:!0,queue:[],sv:"10",config:u,version:2,extensions:void 0};function d(e){var t,n,i,a,r,o,c,s;!0!==cfg.dle&&(o=(t=function(){var e,t={},n=u.connectionString;if("string"==typeof n&&n)for(var i=n.split(";"),a=0;a<i.length;a++){var r=i[a].split("=");2===r.length&&(t[r[0].toLowerCase()]=r[1])}return t[L]||(e=(n=t.endpointsuffix)?t.location:null,t[L]="https://"+(e?e+".":"")+"dc."+(n||"services.visualstudio.com")),t}()).instrumentationkey||u.instrumentationKey||"",t=(t=(t=t[L])&&"/"===t.slice(-1)?t.slice(0,-1):t)?t+"/v2/track":u.endpointUrl,t=u.userOverrideEndpointUrl||t,(n=[]).push((i="SDK LOAD Failure: Failed to load Application Insights SDK script (See stack for details)",a=e,c=t,(s=(r=f(o,"Exception")).data).baseType="ExceptionData",s.baseData.exceptions=[{typeName:"SDKLoadFailed",message:i.replace(/\./g,"-"),hasFullStack:!1,stack:i+"\nSnippet failed to load ["+a+"] -- Telemetry is disabled\nHelp Link: https://go.microsoft.com/fwlink/?linkid=2128109\nHost: "+(D&&D.pathname||"_unknown_")+"\nEndpoint: "+c,parsedStack:[]}],r)),n.push((s=e,i=t,(c=(a=f(o,"Message")).data).baseType="MessageData",(r=c.baseData).message='AI (Internal): 99 message:"'+("SDK LOAD Failure: Failed to load Application Insights SDK script (See stack for details) ("+s+")").replace(/\"/g,"")+'"',r.properties={endpoint:i},a)),e=n,o=t,JSON&&((c=k.fetch)&&!cfg.useXhr?c(o,{method:U,body:JSON.stringify(e),mode:"cors"}):XMLHttpRequest&&((s=new XMLHttpRequest).open(U,o),s.setRequestHeader("Content-type","application/json"),s.send(JSON.stringify(e)))))}function f(e,t){return e=e,t=t,i=l.sv,a=l.version,r=D,(o={})["ai.device."+"id"]="browser",o["ai.device.type"]="Browser",o["ai.operation.name"]=r&&r.pathname||"_unknown_",o["ai.internal.sdkVersion"]="javascript:snippet_"+(i||a),{time:(r=new Date).getUTCFullYear()+"-"+n(1+r.getUTCMonth())+"-"+n(r.getUTCDate())+"T"+n(r.getUTCHours())+":"+n(r.getUTCMinutes())+":"+n(r.getUTCSeconds())+"."+(r.getUTCMilliseconds()/1e3).toFixed(3).slice(2,5)+"Z",iKey:e,name:"Microsoft.ApplicationInsights."+e.replace(/-/g,"")+"."+t,sampleRate:100,tags:o,data:{baseData:{ver:2}},ver:undefined,seq:"1",aiDataContract:undefined};function n(e){e=""+e;return 1===e.length?"0"+e:e}var i,a,r,o}var i,a,t,r,g=-1,h=0,m=["js.monitor.azure.com","js.cdn.applicationinsights.io","js.cdn.monitor.azure.com","js0.cdn.applicationinsights.io","js0.cdn.monitor.azure.com","js2.cdn.applicationinsights.io","js2.cdn.monitor.azure.com","az416426.vo.msecnd.net"],o=function(){return c(n,null)};function c(t,r){if((n=navigator)&&(~(n=(n.userAgent||"").toLowerCase()).indexOf("msie")||~n.indexOf("trident/"))&&~t.indexOf("ai.3")&&(t=t.replace(/(\/)(ai\.3\.)([^\d]*)$/,function(e,t,n){return t+"ai.2"+n})),!1!==cfg.cr)for(var e=0;e<m.length;e++)if(0<t.indexOf(m[e])){g=e;break}var n,o=function(e){var a;l.queue=[],p||(0<=g&&h+1<m.length?(a=(g+h+1)%m.length,i(t.replace(/^(.*\/\/)([\w\.]*)(\/.*)$/,function(e,t,n,i){return t+m[a]+i})),h+=1):(s=p=!0,d(t)))},c=function(e,t){p||setTimeout(function(){t&&!l.core&&o()},500),s=!1},i=function(e){var n,i=x.createElement(E),e=(cfg.pl?cfg.ttp&&cfg.ttp.createScript?i.src=cfg.ttp.createScriptURL(e):i.src=(null==(n=window.trustedTypes)?void 0:n.createPolicy(O,{createScriptURL:function(e){try{var t=new URL(e);if(t.host&&"js.monitor.azure.com"===t.host)return e;a(e)}catch(n){a(e)}}})).createScriptURL(e):i.src=e,cfg.nt&&i.setAttribute("nonce",cfg.nt),r&&(i.integrity=r),i.setAttribute("data-ai-name",A),cfg[b]);function a(e){d("AI policy blocked URL: "+e)}return!e&&""!==e||"undefined"==i[b]||(i[b]=e),i.onload=c,i.onerror=o,i.onreadystatechange=function(e,t){"loaded"!==i.readyState&&"complete"!==i.readyState||c(0,t)},cfg.ld&&cfg.ld<0?x.getElementsByTagName("head")[0].appendChild(i):setTimeout(function(){x.getElementsByTagName(E)[0].parentNode.appendChild(i)},cfg.ld||0),i};i(t)}cfg.sri&&(i=n.match(/^((http[s]?:\/\/.*\/)\w+(\.\d+){1,5})\.(([\w]+\.){0,2}js)$/))&&6===i.length?(T="".concat(i[1],".integrity.json"),a="@".concat(i[4]),S=window.fetch,t=function(e){if(!e.ext||!e.ext[a]||!e.ext[a].file)throw Error("Error Loading JSON response");var t=e.ext[a].integrity||null;c(n=i[2]+e.ext[a].file,t)},S&&!cfg.useXhr?S(T,{method:"GET",mode:"cors"}).then(function(e){return e.json()["catch"](function(){return{}})}).then(t)["catch"](o):XMLHttpRequest&&((r=new XMLHttpRequest).open("GET",T),r.onreadystatechange=function(){if(r.readyState===XMLHttpRequest.DONE)if(200===r.status)try{t(JSON.parse(r.responseText))}catch(e){o()}else o()},r.send())):n&&o();try{l.cookie=x.cookie}catch(w){}function e(e){for(;e.length;)!function(t){l[t]=function(){var e=arguments;s||l.queue.push(function(){l[t].apply(l,e)})}}(e.pop())}e(I);var v,y,S=!(l.SeverityLevel={Verbose:0,Information:1,Warning:2,Error:3,Critical:4}),T=(u.extensionConfig||{}).ApplicationInsightsAnalytics||{};return(S=!0!==u[C]&&!0!==T[C]||S)&&(e(["_"+(v="onerror")]),y=k[v],k[v]=function(e,t,n,i,a){var r=y&&y(e,t,n,i,a);return!0!==r&&l["_"+v]({message:e,url:t,lineNumber:n,columnNumber:i,error:a,evt:k.event}),r},u.autoExceptionInstrumented=!0),l}(cfg.cfg),(k[A]=e).queue&&0===e.queue.length?(e.queue.push(a),e.trackPageView({})):a();})({
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

## Install on Blazor WebAssembly Standalone App - Manual

- Add [BlazorApplicationInsights NuGet](https://www.nuget.org/packages/BlazorApplicationInsights) to the Client project
  - ```dotnet add package BlazorApplicationInsights```
- Add call to Program.cs

  ```csharp
  builder.Services.AddBlazorApplicationInsights();
  ```

- Add using statement to _Imports.razor
  - ```@using BlazorApplicationInsights;```
- Add component to the top of App.razor
  - ```<ApplicationInsightsInit IsWasmStandalone="true" />```
- Add Application Insights JS to head below \<base href="/" /> in index.html
  - **Note:** Set all required configuration in the cfg section
  ```html
  <script type="text/javascript">
    !(function (cfg){var k,x,D,E,L,C,b,U,O,A,e,t="track",n="TrackPage",i="TrackEvent",I=[t+"Event",t+"Exception",t+"PageView",t+"PageViewPerformance","addTelemetryInitializer",t+"Trace",t+"DependencyData",t+"Metric","start"+n,"stop"+n,"start"+i,"stop"+i,"setAuthenticatedUserContext","clearAuthenticatedUserContext","flush"];function a(){cfg.onInit&&cfg.onInit(e)}k=window,x=document,D=k.location,E="script",L="ingestionendpoint",C="disableExceptionTracking",b="crossOrigin",U="POST",O=cfg.pn||"aiPolicy",t="appInsightsSDK",A=cfg.name||"appInsights",(cfg.name||k[t])&&(k[t]=A),e=k[A]||function(u){var n=u.url||cfg.src,s=!1,p=!1,l={initialize:!0,queue:[],sv:"10",config:u,version:2,extensions:void 0};function d(e){var t,n,i,a,r,o,c,s;!0!==cfg.dle&&(o=(t=function(){var e,t={},n=u.connectionString;if("string"==typeof n&&n)for(var i=n.split(";"),a=0;a<i.length;a++){var r=i[a].split("=");2===r.length&&(t[r[0].toLowerCase()]=r[1])}return t[L]||(e=(n=t.endpointsuffix)?t.location:null,t[L]="https://"+(e?e+".":"")+"dc."+(n||"services.visualstudio.com")),t}()).instrumentationkey||u.instrumentationKey||"",t=(t=(t=t[L])&&"/"===t.slice(-1)?t.slice(0,-1):t)?t+"/v2/track":u.endpointUrl,t=u.userOverrideEndpointUrl||t,(n=[]).push((i="SDK LOAD Failure: Failed to load Application Insights SDK script (See stack for details)",a=e,c=t,(s=(r=f(o,"Exception")).data).baseType="ExceptionData",s.baseData.exceptions=[{typeName:"SDKLoadFailed",message:i.replace(/\./g,"-"),hasFullStack:!1,stack:i+"\nSnippet failed to load ["+a+"] -- Telemetry is disabled\nHelp Link: https://go.microsoft.com/fwlink/?linkid=2128109\nHost: "+(D&&D.pathname||"_unknown_")+"\nEndpoint: "+c,parsedStack:[]}],r)),n.push((s=e,i=t,(c=(a=f(o,"Message")).data).baseType="MessageData",(r=c.baseData).message='AI (Internal): 99 message:"'+("SDK LOAD Failure: Failed to load Application Insights SDK script (See stack for details) ("+s+")").replace(/\"/g,"")+'"',r.properties={endpoint:i},a)),e=n,o=t,JSON&&((c=k.fetch)&&!cfg.useXhr?c(o,{method:U,body:JSON.stringify(e),mode:"cors"}):XMLHttpRequest&&((s=new XMLHttpRequest).open(U,o),s.setRequestHeader("Content-type","application/json"),s.send(JSON.stringify(e)))))}function f(e,t){return e=e,t=t,i=l.sv,a=l.version,r=D,(o={})["ai.device."+"id"]="browser",o["ai.device.type"]="Browser",o["ai.operation.name"]=r&&r.pathname||"_unknown_",o["ai.internal.sdkVersion"]="javascript:snippet_"+(i||a),{time:(r=new Date).getUTCFullYear()+"-"+n(1+r.getUTCMonth())+"-"+n(r.getUTCDate())+"T"+n(r.getUTCHours())+":"+n(r.getUTCMinutes())+":"+n(r.getUTCSeconds())+"."+(r.getUTCMilliseconds()/1e3).toFixed(3).slice(2,5)+"Z",iKey:e,name:"Microsoft.ApplicationInsights."+e.replace(/-/g,"")+"."+t,sampleRate:100,tags:o,data:{baseData:{ver:2}},ver:undefined,seq:"1",aiDataContract:undefined};function n(e){e=""+e;return 1===e.length?"0"+e:e}var i,a,r,o}var i,a,t,r,g=-1,h=0,m=["js.monitor.azure.com","js.cdn.applicationinsights.io","js.cdn.monitor.azure.com","js0.cdn.applicationinsights.io","js0.cdn.monitor.azure.com","js2.cdn.applicationinsights.io","js2.cdn.monitor.azure.com","az416426.vo.msecnd.net"],o=function(){return c(n,null)};function c(t,r){if((n=navigator)&&(~(n=(n.userAgent||"").toLowerCase()).indexOf("msie")||~n.indexOf("trident/"))&&~t.indexOf("ai.3")&&(t=t.replace(/(\/)(ai\.3\.)([^\d]*)$/,function(e,t,n){return t+"ai.2"+n})),!1!==cfg.cr)for(var e=0;e<m.length;e++)if(0<t.indexOf(m[e])){g=e;break}var n,o=function(e){var a;l.queue=[],p||(0<=g&&h+1<m.length?(a=(g+h+1)%m.length,i(t.replace(/^(.*\/\/)([\w\.]*)(\/.*)$/,function(e,t,n,i){return t+m[a]+i})),h+=1):(s=p=!0,d(t)))},c=function(e,t){p||setTimeout(function(){t&&!l.core&&o()},500),s=!1},i=function(e){var n,i=x.createElement(E),e=(cfg.pl?cfg.ttp&&cfg.ttp.createScript?i.src=cfg.ttp.createScriptURL(e):i.src=(null==(n=window.trustedTypes)?void 0:n.createPolicy(O,{createScriptURL:function(e){try{var t=new URL(e);if(t.host&&"js.monitor.azure.com"===t.host)return e;a(e)}catch(n){a(e)}}})).createScriptURL(e):i.src=e,cfg.nt&&i.setAttribute("nonce",cfg.nt),r&&(i.integrity=r),i.setAttribute("data-ai-name",A),cfg[b]);function a(e){d("AI policy blocked URL: "+e)}return!e&&""!==e||"undefined"==i[b]||(i[b]=e),i.onload=c,i.onerror=o,i.onreadystatechange=function(e,t){"loaded"!==i.readyState&&"complete"!==i.readyState||c(0,t)},cfg.ld&&cfg.ld<0?x.getElementsByTagName("head")[0].appendChild(i):setTimeout(function(){x.getElementsByTagName(E)[0].parentNode.appendChild(i)},cfg.ld||0),i};i(t)}cfg.sri&&(i=n.match(/^((http[s]?:\/\/.*\/)\w+(\.\d+){1,5})\.(([\w]+\.){0,2}js)$/))&&6===i.length?(T="".concat(i[1],".integrity.json"),a="@".concat(i[4]),S=window.fetch,t=function(e){if(!e.ext||!e.ext[a]||!e.ext[a].file)throw Error("Error Loading JSON response");var t=e.ext[a].integrity||null;c(n=i[2]+e.ext[a].file,t)},S&&!cfg.useXhr?S(T,{method:"GET",mode:"cors"}).then(function(e){return e.json()["catch"](function(){return{}})}).then(t)["catch"](o):XMLHttpRequest&&((r=new XMLHttpRequest).open("GET",T),r.onreadystatechange=function(){if(r.readyState===XMLHttpRequest.DONE)if(200===r.status)try{t(JSON.parse(r.responseText))}catch(e){o()}else o()},r.send())):n&&o();try{l.cookie=x.cookie}catch(w){}function e(e){for(;e.length;)!function(t){l[t]=function(){var e=arguments;s||l.queue.push(function(){l[t].apply(l,e)})}}(e.pop())}e(I);var v,y,S=!(l.SeverityLevel={Verbose:0,Information:1,Warning:2,Error:3,Critical:4}),T=(u.extensionConfig||{}).ApplicationInsightsAnalytics||{};return(S=!0!==u[C]&&!0!==T[C]||S)&&(e(["_"+(v="onerror")]),y=k[v],k[v]=function(e,t,n,i,a){var r=y&&y(e,t,n,i,a);return!0!==r&&l["_"+v]({message:e,url:t,lineNumber:n,columnNumber:i,error:a,evt:k.event}),r},u.autoExceptionInstrumented=!0),l}(cfg.cfg),(k[A]=e).queue&&0===e.queue.length?(e.queue.push(a),e.trackPageView({})):a();})({
        src: "https://js.monitor.azure.com/scripts/b/ai.3.gbl.min.js",
        ld: -1,
        crossOrigin: "anonymous",
        cfg: {
            connectionString: "{Insert Connection String}",
            enableAutoRouteTracking: true
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

