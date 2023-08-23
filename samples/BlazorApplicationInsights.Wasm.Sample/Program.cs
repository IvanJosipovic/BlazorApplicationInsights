using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json;

namespace BlazorApplicationInsights.Wasm.Sample
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddBlazorApplicationInsights(async applicationInsights =>
            {
                await applicationInsights.AddTelemetryInitializer(telemetryItem =>
                {
                    telemetryItem.Tags.Add("ai.cloud.role", "SPA");
                    telemetryItem.Tags.Add("ai.cloud.roleInstance", "Blazor Wasm");
                    Console.WriteLine(JsonSerializer.Serialize(telemetryItem));
                    return true;
                });
            });

            await builder.Build().RunAsync();
        }
    }
}
