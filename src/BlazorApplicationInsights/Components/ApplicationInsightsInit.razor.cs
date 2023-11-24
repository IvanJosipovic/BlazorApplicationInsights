using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BlazorApplicationInsights.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorApplicationInsights;

/// <summary>
/// BlazorApplicationInsights initialization component
/// </summary>
public partial class ApplicationInsightsInit : IDisposable
{
    [Inject] IApplicationInsights ApplicationInsights { get; set; }
    [Inject] private IJSRuntime JSRuntime { get; set; }
    [Inject] private ApplicationInsightsInitConfig Config { get; set; }

    [Parameter]
    public bool IsWasmStandalone { get; set; }

    private bool WasPreRendered { get; set; } = true;

    private static readonly JsonSerializerOptions SerializerOptions = new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && IsWasmStandalone)
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

    protected override async Task OnInitializedAsync()
    {
    }

    public void Dispose()
    {
    }
}