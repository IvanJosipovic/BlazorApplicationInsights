using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorApplicationInsights
{
    public class ApplicationInsights : IApplicationInsights
    {
        private readonly IJSRuntime JSRuntime;

        public ApplicationInsights(IJSRuntime jsRuntime)
        {
            JSRuntime = jsRuntime;
        }

        public async Task TrackPageView()
        {
            await JSRuntime.InvokeVoidAsync("appInsights.trackPageView");
        }

        public async Task TrackEvent(string name)
        {
            await JSRuntime.InvokeVoidAsync("appInsights.trackEvent", new { name });
        }

        public async Task TrackTrace(string message)
        {
            await JSRuntime.InvokeVoidAsync("appInsights.trackTrace", new { message });
        }
    }
}