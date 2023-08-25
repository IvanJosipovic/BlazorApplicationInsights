using System;
using System.Threading.Tasks;
using BlazorApplicationInsights.Interfaces;
using BlazorApplicationInsights.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace BlazorApplicationInsights
{
    public partial class ApplicationInsightsComponent : IDisposable
    {
        [Inject] private IApplicationInsights ApplicationInsights { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }
        //[Inject]
        private Config Config { get; set; } = new();

        public bool IsWebAssembly { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            IsWebAssembly = JSRuntime is IJSInProcessRuntime;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorApplicationInsights/JsInterop.js");

                await ApplicationInsights.InitBlazorApplicationInsightsAsync(JSRuntime);

                //todo
                if (Config.EnableDebug.HasValue)
                {
                    NavigationManager.LocationChanged += NavigationManager_LocationChanged;
                }
            }
        }

        private async void NavigationManager_LocationChanged(object? sender, LocationChangedEventArgs e)
        {
            await ApplicationInsights.TrackPageView();
        }

        public void Dispose()
        {
            //todo
            if (Config.EnableDebug.HasValue)
            {
                NavigationManager.LocationChanged -= NavigationManager_LocationChanged;
            }
        }
    }
}
