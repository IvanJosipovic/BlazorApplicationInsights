using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BlazorApplicationInsights.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorApplicationInsights;

public partial class ApplicationInsightsInit
{
    [Inject] private IJSRuntime JSRuntime { get; set; }
    [Inject] private BlazorApplicationInsightsConfig Config { get; set; } = new();

    public bool IsWebAssembly { get; set; }

    private static readonly JsonSerializerOptions SerializerOptions = new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull};

    protected override void OnInitialized()
    {
        base.OnInitialized();

        IsWebAssembly = JSRuntime is IJSInProcessRuntime;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && IsWebAssembly)
        {
            await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorApplicationInsights/JsInterop.js");
        }
    }
}
