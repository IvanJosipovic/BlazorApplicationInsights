using BlazorApplicationInsights.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorApplicationInsights.Wasm.Sample;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        builder.Services.AddBlazorApplicationInsights(config =>
        {
            config.ConnectionString = "InstrumentationKey=219f9af4-0842-42c8-a5b1-578f09d2ee27;IngestionEndpoint=https://westus2-0.in.applicationinsights.azure.com/;LiveEndpoint=https://westus2.livediagnostics.monitor.azure.com/";
            config.InstrumentationKey = "219f9af4-0842-42c8-a5b1-578f09d2ee27";
        },
        new TelemetryItem()
        {
            Tags = new Dictionary<string, object>()
            {
                { "ai.cloud.role", "SPA" },
                { "ai.cloud.roleInstance", "Blazor Wasm" },
            }
        });

        await builder.Build().RunAsync();
    }
}
