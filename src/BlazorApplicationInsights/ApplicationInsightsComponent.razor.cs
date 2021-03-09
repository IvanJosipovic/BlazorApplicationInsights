using System;
using System.Threading.Tasks;
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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await ApplicationInsights.InitBlazorApplicationInsightsAsync(JSRuntime);
                NavigationManager.LocationChanged += NavigationManager_LocationChanged;
            }
        }

        private async void NavigationManager_LocationChanged(object sender, LocationChangedEventArgs e)
        {
            await ApplicationInsights.TrackPageView();
        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= NavigationManager_LocationChanged;
        }
    }
}
