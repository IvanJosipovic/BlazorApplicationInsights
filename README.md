[![Demo](https://img.shields.io/badge/Live-Demo-Blue?style=flat-square)](https://BlazorApplicationInsights.netlify.app/)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/BlazorApplicationInsights.svg?style=flat-square)](https://www.nuget.org/packages/BlazorApplicationInsights)
[![Nuget (with prereleases)](https://img.shields.io/nuget/dt/BlazorApplicationInsights.svg?style=flat-square)](https://www.nuget.org/packages/BlazorApplicationInsights)
![](https://github.com/IvanJosipovic/BlazorApplicationInsights/workflows/Create%20Release/badge.svg)

Application Insights for Blazor web applications

## Install

- Add [BlazorApplicationInsights Nuget](https://www.nuget.org/packages/BlazorApplicationInsights)
  - dotnet add package BlazorApplicationInsights
- Add call to Program.cs
  - ```builder.Services.AddBlazorApplicationInsights();```
- Add using statement to _Imports.razor
  - ```@using BlazorApplicationInsights;```
- Add component to App.razor
  - ```<ApplicationInsightsComponent />```
- Add Application Insights JS to head in index.html
  - Enter your Connection String
    ```html
    <script type="text/javascript">
      !function (v, y, T) { var S = v.location, k = "script", D = "instrumentationKey", C = "ingestionendpoint", I = "disableExceptionTracking", E = "ai.device.", b = "toLowerCase", w = (D[b](), "crossOrigin"), N = "POST", e = "appInsightsSDK", t = T.name || "appInsights", n = ((T.name || v[e]) && (v[e] = t), v[t] || function (l) { var u = !1, d = !1, g = { initialize: !0, queue: [], sv: "6", version: 2, config: l }; function m(e, t) { var n = {}, a = "Browser"; return n[E + "id"] = a[b](), n[E + "type"] = a, n["ai.operation.name"] = S && S.pathname || "_unknown_", n["ai.internal.sdkVersion"] = "javascript:snippet_" + (g.sv || g.version), { time: (a = new Date).getUTCFullYear() + "-" + i(1 + a.getUTCMonth()) + "-" + i(a.getUTCDate()) + "T" + i(a.getUTCHours()) + ":" + i(a.getUTCMinutes()) + ":" + i(a.getUTCSeconds()) + "." + (a.getUTCMilliseconds() / 1e3).toFixed(3).slice(2, 5) + "Z", iKey: e, name: "Microsoft.ApplicationInsights." + e.replace(/-/g, "") + "." + t, sampleRate: 100, tags: n, data: { baseData: { ver: 2 } } }; function i(e) { e = "" + e; return 1 === e.length ? "0" + e : e } } var e, n, f = l.url || T.src; function a(e) { var t, n, a, i, o, s, r, c, p; u = !0, g.queue = [], d || (d = !0, i = f, r = (c = function () { var e, t = {}, n = l.connectionString; if (n) for (var a = n.split(";"), i = 0; i < a.length; i++) { var o = a[i].split("="); 2 === o.length && (t[o[0][b]()] = o[1]) } return t[C] || (t[C] = "https://" + ((e = (n = t.endpointsuffix) ? t.location : null) ? e + "." : "") + "dc." + (n || "services.visualstudio.com")), t }()).instrumentationkey || l[D] || "", c = (c = c[C]) ? c + "/v2/track" : l.endpointUrl, (p = []).push((t = "SDK LOAD Failure: Failed to load Application Insights SDK script (See stack for details)", n = i, o = c, (s = (a = m(r, "Exception")).data).baseType = "ExceptionData", s.baseData.exceptions = [{ typeName: "SDKLoadFailed", message: t.replace(/\./g, "-"), hasFullStack: !1, stack: t + "\nSnippet failed to load [" + n + "] -- Telemetry is disabled\nHelp Link: https://go.microsoft.com/fwlink/?linkid=2128109\nHost: " + (S && S.pathname || "_unknown_") + "\nEndpoint: " + o, parsedStack: [] }], a)), p.push((s = i, t = c, (o = (n = m(r, "Message")).data).baseType = "MessageData", (a = o.baseData).message = 'AI (Internal): 99 message:"' + ("SDK LOAD Failure: Failed to load Application Insights SDK script (See stack for details) (" + s + ")").replace(/\"/g, "") + '"', a.properties = { endpoint: t }, n)), i = p, r = c, JSON && ((o = v.fetch) && !T.useXhr ? o(r, { method: N, body: JSON.stringify(i), mode: "cors" }) : XMLHttpRequest && ((s = new XMLHttpRequest).open(N, r), s.setRequestHeader("Content-type", "application/json"), s.send(JSON.stringify(i))))) } function i(e, t) { d || setTimeout(function () { !t && g.core || a() }, 500) } f && ((n = y.createElement(k)).src = f, !(o = T[w]) && "" !== o || "undefined" == n[w] || (n[w] = o), n.onload = i, n.onerror = a, n.onreadystatechange = function (e, t) { "loaded" !== n.readyState && "complete" !== n.readyState || i(0, t) }, e = n, T.ld < 0 ? y.getElementsByTagName("head")[0].appendChild(e) : setTimeout(function () { y.getElementsByTagName(k)[0].parentNode.appendChild(e) }, T.ld || 0)); try { g.cookie = y.cookie } catch (h) { } function t(e) { for (; e.length;)!function (t) { g[t] = function () { var e = arguments; u || g.queue.push(function () { g[t].apply(g, e) }) } }(e.pop()) } var s, r, o = "track", c = "TrackPage", p = "TrackEvent", o = (t([o + "Event", o + "PageView", o + "Exception", o + "Trace", o + "DependencyData", o + "Metric", o + "PageViewPerformance", "start" + c, "stop" + c, "start" + p, "stop" + p, "addTelemetryInitializer", "setAuthenticatedUserContext", "clearAuthenticatedUserContext", "flush"]), g.SeverityLevel = { Verbose: 0, Information: 1, Warning: 2, Error: 3, Critical: 4 }, (l.extensionConfig || {}).ApplicationInsightsAnalytics || {}); return !0 !== l[I] && !0 !== o[I] && (t(["_" + (s = "onerror")]), r = v[s], v[s] = function (e, t, n, a, i) { var o = r && r(e, t, n, a, i); return !0 !== o && g["_" + s]({ message: e, url: t, lineNumber: n, columnNumber: a, error: i, evt: v.event }), o }, l.autoExceptionInstrumented = !0), g }(T.cfg)); function a() { T.onInit && T.onInit(n) } (v[t] = n).queue && 0 === n.queue.length ? (n.queue.push(a), n.trackPageView({})) : a() }(window, document, {
      src: "https://js.monitor.azure.com/scripts/b/ai.2.min.js",
      ld: -1,
      crossOrigin: "anonymous",
      cfg: {
          connectionString:"InstrumentationKey=00000000-0000-0000-0000-000000000000;"
      }});
    </script>
    ```

## [Example Project](https://github.com/IvanJosipovic/BlazorApplicationInsights/tree/master/src/BlazorApplicationInsights.Sample)

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
  - SetConnectionString # Not recommended

## TrackEvent

```razor
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

## Set Connection String Programmatically (Blazor WASM ASP.NET Core Hosted)

- In the Blazor WASM Project
  - Edit Index.html and use the following tweaked Application Insights JS

  ```html
  <script type="text/javascript">
      const getCookieValue = (name) => (
          document.cookie.match('(^|;)\\s*' + name + '\\s*=\\s*([^;]+)')?.pop() || ''
      )

      !function (T, l, y) { var S = T.location, k = "script", D = "instrumentationKey", C = "ingestionendpoint", I = "disableExceptionTracking", E = "ai.device.", b = "toLowerCase", w = "crossOrigin", N = "POST", e = "appInsightsSDK", t = y.name || "appInsights"; (y.name || T[e]) && (T[e] = t); var n = T[t] || function (d) { var g = !1, f = !1, m = { initialize: !0, queue: [], sv: "5", version: 2, config: d }; function v(e, t) { var n = {}, a = "Browser"; return n[E + "id"] = a[b](), n[E + "type"] = a, n["ai.operation.name"] = S && S.pathname || "_unknown_", n["ai.internal.sdkVersion"] = "javascript:snippet_" + (m.sv || m.version), { time: function () { var e = new Date; function t(e) { var t = "" + e; return 1 === t.length && (t = "0" + t), t } return e.getUTCFullYear() + "-" + t(1 + e.getUTCMonth()) + "-" + t(e.getUTCDate()) + "T" + t(e.getUTCHours()) + ":" + t(e.getUTCMinutes()) + ":" + t(e.getUTCSeconds()) + "." + ((e.getUTCMilliseconds() / 1e3).toFixed(3) + "").slice(2, 5) + "Z" }(), iKey: e, name: "Microsoft.ApplicationInsights." + e.replace(/-/g, "") + "." + t, sampleRate: 100, tags: n, data: { baseData: { ver: 2 } } } } var h = d.url || y.src; if (h) { function a(e) { var t, n, a, i, r, o, s, c, u, p, l; g = !0, m.queue = [], f || (f = !0, t = h, s = function () { var e = {}, t = d.connectionString; if (t) for (var n = t.split(";"), a = 0; a < n.length; a++) { var i = n[a].split("="); 2 === i.length && (e[i[0][b]()] = i[1]) } if (!e[C]) { var r = e.endpointsuffix, o = r ? e.location : null; e[C] = "https://" + (o ? o + "." : "") + "dc." + (r || "services.visualstudio.com") } return e }(), c = s[D] || d[D] || "", u = s[C], p = u ? u + "/v2/track" : d.endpointUrl, (l = []).push((n = "SDK LOAD Failure: Failed to load Application Insights SDK script (See stack for details)", a = t, i = p, (o = (r = v(c, "Exception")).data).baseType = "ExceptionData", o.baseData.exceptions = [{ typeName: "SDKLoadFailed", message: n.replace(/\./g, "-"), hasFullStack: !1, stack: n + "\nSnippet failed to load [" + a + "] -- Telemetry is disabled\nHelp Link: https://go.microsoft.com/fwlink/?linkid=2128109\nHost: " + (S && S.pathname || "_unknown_") + "\nEndpoint: " + i, parsedStack: [] }], r)), l.push(function (e, t, n, a) { var i = v(c, "Message"), r = i.data; r.baseType = "MessageData"; var o = r.baseData; return o.message = 'AI (Internal): 99 message:"' + ("SDK LOAD Failure: Failed to load Application Insights SDK script (See stack for details) (" + n + ")").replace(/\"/g, "") + '"', o.properties = { endpoint: a }, i }(0, 0, t, p)), function (e, t) { if (JSON) { var n = T.fetch; if (n && !y.useXhr) n(t, { method: N, body: JSON.stringify(e), mode: "cors" }); else if (XMLHttpRequest) { var a = new XMLHttpRequest; a.open(N, t), a.setRequestHeader("Content-type", "application/json"), a.send(JSON.stringify(e)) } } }(l, p)) } function i(e, t) { f || setTimeout(function () { !t && m.core || a() }, 500) } var e = function () { var n = l.createElement(k); n.src = h; var e = y[w]; return !e && "" !== e || "undefined" == n[w] || (n[w] = e), n.onload = i, n.onerror = a, n.onreadystatechange = function (e, t) { "loaded" !== n.readyState && "complete" !== n.readyState || i(0, t) }, n }(); y.ld < 0 ? l.getElementsByTagName("head")[0].appendChild(e) : setTimeout(function () { l.getElementsByTagName(k)[0].parentNode.appendChild(e) }, y.ld || 0) } try { m.cookie = l.cookie } catch (p) { } function t(e) { for (; e.length;)!function (t) { m[t] = function () { var e = arguments; g || m.queue.push(function () { m[t].apply(m, e) }) } }(e.pop()) } var n = "track", r = "TrackPage", o = "TrackEvent"; t([n + "Event", n + "PageView", n + "Exception", n + "Trace", n + "DependencyData", n + "Metric", n + "PageViewPerformance", "start" + r, "stop" + r, "start" + o, "stop" + o, "addTelemetryInitializer", "setAuthenticatedUserContext", "clearAuthenticatedUserContext", "flush"]), m.SeverityLevel = { Verbose: 0, Information: 1, Warning: 2, Error: 3, Critical: 4 }; var s = (d.extensionConfig || {}).ApplicationInsightsAnalytics || {}; if (!0 !== d[I] && !0 !== s[I]) { var c = "onerror"; t(["_" + c]); var u = T[c]; T[c] = function (e, t, n, a, i) { var r = u && u(e, t, n, a, i); return !0 !== r && m["_" + c]({ message: e, url: t, lineNumber: n, columnNumber: a, error: i }), r }, d.autoExceptionInstrumented = !0 } return m }(y.cfg); function a() { y.onInit && y.onInit(n) } (T[t] = n).queue && 0 === n.queue.length ? (n.queue.push(a), n.trackPageView({})) : a() }(window, document, {
          src: "https://js.monitor.azure.com/scripts/b/ai.2.min.js",
          ld: -1,
          cfg: {
              connectionString: decodeURIComponent(getCookieValue('ai_connString'))
          }
      });
  </script>
  ```

- In the ASP.NET Server Project
  - Edit Program.cs and replace the app.MapFallbackToFile() with the following

  ```csharp
  app.MapFallbackToFile("index.html", new StaticFileOptions()
  {
      OnPrepareResponse = ctx =>
      {
          ctx.Context.Response.Cookies.Append("ai_connString", app.Configuration["ApplicationInsights:ConnectionString"]);
      }
  });
  ```

## Set Connection String Programmatically (Blazor WASM Self Hosted)

- In the Blazor WASM Project
  - Edit Index.html and use the following tweaked Application Insights JS

  ```html
  <script type="text/javascript">
      const getCookieValue = (name) => (
          document.cookie.match('(^|;)\\s*' + name + '\\s*=\\s*([^;]+)')?.pop() || ''
      )

      !function (T, l, y) { var S = T.location, k = "script", D = "instrumentationKey", C = "ingestionendpoint", I = "disableExceptionTracking", E = "ai.device.", b = "toLowerCase", w = "crossOrigin", N = "POST", e = "appInsightsSDK", t = y.name || "appInsights"; (y.name || T[e]) && (T[e] = t); var n = T[t] || function (d) { var g = !1, f = !1, m = { initialize: !0, queue: [], sv: "5", version: 2, config: d }; function v(e, t) { var n = {}, a = "Browser"; return n[E + "id"] = a[b](), n[E + "type"] = a, n["ai.operation.name"] = S && S.pathname || "_unknown_", n["ai.internal.sdkVersion"] = "javascript:snippet_" + (m.sv || m.version), { time: function () { var e = new Date; function t(e) { var t = "" + e; return 1 === t.length && (t = "0" + t), t } return e.getUTCFullYear() + "-" + t(1 + e.getUTCMonth()) + "-" + t(e.getUTCDate()) + "T" + t(e.getUTCHours()) + ":" + t(e.getUTCMinutes()) + ":" + t(e.getUTCSeconds()) + "." + ((e.getUTCMilliseconds() / 1e3).toFixed(3) + "").slice(2, 5) + "Z" }(), iKey: e, name: "Microsoft.ApplicationInsights." + e.replace(/-/g, "") + "." + t, sampleRate: 100, tags: n, data: { baseData: { ver: 2 } } } } var h = d.url || y.src; if (h) { function a(e) { var t, n, a, i, r, o, s, c, u, p, l; g = !0, m.queue = [], f || (f = !0, t = h, s = function () { var e = {}, t = d.connectionString; if (t) for (var n = t.split(";"), a = 0; a < n.length; a++) { var i = n[a].split("="); 2 === i.length && (e[i[0][b]()] = i[1]) } if (!e[C]) { var r = e.endpointsuffix, o = r ? e.location : null; e[C] = "https://" + (o ? o + "." : "") + "dc." + (r || "services.visualstudio.com") } return e }(), c = s[D] || d[D] || "", u = s[C], p = u ? u + "/v2/track" : d.endpointUrl, (l = []).push((n = "SDK LOAD Failure: Failed to load Application Insights SDK script (See stack for details)", a = t, i = p, (o = (r = v(c, "Exception")).data).baseType = "ExceptionData", o.baseData.exceptions = [{ typeName: "SDKLoadFailed", message: n.replace(/\./g, "-"), hasFullStack: !1, stack: n + "\nSnippet failed to load [" + a + "] -- Telemetry is disabled\nHelp Link: https://go.microsoft.com/fwlink/?linkid=2128109\nHost: " + (S && S.pathname || "_unknown_") + "\nEndpoint: " + i, parsedStack: [] }], r)), l.push(function (e, t, n, a) { var i = v(c, "Message"), r = i.data; r.baseType = "MessageData"; var o = r.baseData; return o.message = 'AI (Internal): 99 message:"' + ("SDK LOAD Failure: Failed to load Application Insights SDK script (See stack for details) (" + n + ")").replace(/\"/g, "") + '"', o.properties = { endpoint: a }, i }(0, 0, t, p)), function (e, t) { if (JSON) { var n = T.fetch; if (n && !y.useXhr) n(t, { method: N, body: JSON.stringify(e), mode: "cors" }); else if (XMLHttpRequest) { var a = new XMLHttpRequest; a.open(N, t), a.setRequestHeader("Content-type", "application/json"), a.send(JSON.stringify(e)) } } }(l, p)) } function i(e, t) { f || setTimeout(function () { !t && m.core || a() }, 500) } var e = function () { var n = l.createElement(k); n.src = h; var e = y[w]; return !e && "" !== e || "undefined" == n[w] || (n[w] = e), n.onload = i, n.onerror = a, n.onreadystatechange = function (e, t) { "loaded" !== n.readyState && "complete" !== n.readyState || i(0, t) }, n }(); y.ld < 0 ? l.getElementsByTagName("head")[0].appendChild(e) : setTimeout(function () { l.getElementsByTagName(k)[0].parentNode.appendChild(e) }, y.ld || 0) } try { m.cookie = l.cookie } catch (p) { } function t(e) { for (; e.length;)!function (t) { m[t] = function () { var e = arguments; g || m.queue.push(function () { m[t].apply(m, e) }) } }(e.pop()) } var n = "track", r = "TrackPage", o = "TrackEvent"; t([n + "Event", n + "PageView", n + "Exception", n + "Trace", n + "DependencyData", n + "Metric", n + "PageViewPerformance", "start" + r, "stop" + r, "start" + o, "stop" + o, "addTelemetryInitializer", "setAuthenticatedUserContext", "clearAuthenticatedUserContext", "flush"]), m.SeverityLevel = { Verbose: 0, Information: 1, Warning: 2, Error: 3, Critical: 4 }; var s = (d.extensionConfig || {}).ApplicationInsightsAnalytics || {}; if (!0 !== d[I] && !0 !== s[I]) { var c = "onerror"; t(["_" + c]); var u = T[c]; T[c] = function (e, t, n, a, i) { var r = u && u(e, t, n, a, i); return !0 !== r && m["_" + c]({ message: e, url: t, lineNumber: n, columnNumber: a, error: i }), r }, d.autoExceptionInstrumented = !0 } return m }(y.cfg); function a() { y.onInit && y.onInit(n) } (T[t] = n).queue && 0 === n.queue.length ? (n.queue.push(a), n.trackPageView({})) : a() }(window, document, {
          src: "https://js.monitor.azure.com/scripts/b/ai.2.min.js",
          ld: -1,
          cfg: {
              connectionString: decodeURIComponent(getCookieValue('ai_connString'))
          }
      });
  </script>
  ```

- In the hosting platform
  - Inject a Cookie called "ai_connString" with the ConnectionString URL Encoded