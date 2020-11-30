using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace BlazorApplicationInsights
{
    public partial class ApplicationInsightsComponent
    {
        [Inject] private IApplicationInsights ApplicationInsights { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }

        protected override void OnInitialized()
        {
            ApplicationInsights.InitJSRuntime(JSRuntime);

            NavigationManager.LocationChanged += NavigationManager_LocationChanged;
        }

        private async void NavigationManager_LocationChanged(object sender, LocationChangedEventArgs e)
        {
            await ApplicationInsights.TrackPageView();
        }
    }
}
