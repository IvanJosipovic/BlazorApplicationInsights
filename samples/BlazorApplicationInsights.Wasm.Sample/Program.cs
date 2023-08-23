using BlazorApplicationInsights;
using BlazorApplicationInsights.Wasm.Sample;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

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
