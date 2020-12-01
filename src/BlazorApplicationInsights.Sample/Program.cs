using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorApplicationInsights.Sample
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

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
        }
    }
}
