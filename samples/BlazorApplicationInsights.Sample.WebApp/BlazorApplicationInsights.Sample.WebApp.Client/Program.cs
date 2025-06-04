using BlazorApplicationInsights;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazorApplicationInsights(config =>
{
    config.ConnectionString = "InstrumentationKey=4f8d37b3-0d6e-4c1a-80a2-035a0e832299;IngestionEndpoint=https://westus-0.in.applicationinsights.azure.com/;LiveEndpoint=https://westus.livediagnostics.monitor.azure.com/;ApplicationId=5eaf113f-fc6a-407d-bdc5-d626b71d22a7";
});

await builder.Build().RunAsync();
