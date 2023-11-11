using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BlazorApplicationInsights.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace BlazorApplicationInsights;

public partial class ApplicationInsightsInit
{
    [Inject] IApplicationInsights ApplicationInsights { get; set; }
    [Inject] private IJSRuntime JSRuntime { get; set; }
    [Inject] private ApplicationInsightsInitConfig Config { get; set; }
    [Inject] private ILogger<ApplicationInsightsInit> Logger { get; set; }

    public bool IsWebAssembly { get; set; }

    private static readonly JsonSerializerOptions SerializerOptions = new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

    protected override void OnInitialized()
    {
        base.OnInitialized();

        IsWebAssembly = OperatingSystem.IsBrowser();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && IsWebAssembly)
        {
            await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorApplicationInsights/JsInterop.js");

            if (Config.Config != null)
            {
                await ApplicationInsights.UpdateCfg(Config.Config);

                await ApplicationInsights.TrackPageView();
            }
        }

        if (Config.TelemetryInitializer != null)
        {
            await ApplicationInsights.AddTelemetryInitializer(Config.TelemetryInitializer);
        }
    }
}