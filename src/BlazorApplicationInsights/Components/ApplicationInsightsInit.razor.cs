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
public partial class ApplicationInsightsInit
{
    [Inject] IApplicationInsights ApplicationInsights { get; set; }
    [Inject] private IJSRuntime JSRuntime { get; set; }
    [Inject] private ApplicationInsightsInitConfig Config { get; set; }

    /// <summary>
    /// Must be enabled when running in Blazor Wasm Standalone
    /// </summary>
    [Parameter]
    public bool IsWasmStandalone { get; set; }

    private static readonly JsonSerializerOptions SerializerOptions = new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

    /// <inheritdoc/>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            ApplicationInsights.InitJSRuntime(JSRuntime);
        }

        if (firstRender && IsWasmStandalone)
        {
            await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorApplicationInsights/JsInterop.js");

            if (Config.Config != null)
            {
                await ApplicationInsights.UpdateCfg(Config.Config);

                await ApplicationInsights.TrackPageView();
            }
        }

        if (firstRender && Config.OnAppInsightsInit != null)
        {
            await Config.OnAppInsightsInit(ApplicationInsights);
        }
    }
}