using BlazorApplicationInsights.Models;
using BlazorApplicationInsights.Sample.Server.Data;

namespace BlazorApplicationInsights.Sample.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();

            builder.Services.AddHttpClient();
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient());

            builder.Services.AddBlazorApplicationInsights(x =>
            {
                x.ConnectionString = "InstrumentationKey=4f8d37b3-0d6e-4c1a-80a2-035a0e832299;IngestionEndpoint=https://westus-0.in.applicationinsights.azure.com/;LiveEndpoint=https://westus.livediagnostics.monitor.azure.com/;ApplicationId=5eaf113f-fc6a-407d-bdc5-d626b71d22a7";
            },
            async applicationInsights =>
            {
                var telemetryItem = new TelemetryItem()
                {
                    Tags = new Dictionary<string, object?>()
                    {
                        { "ai.cloud.role", "SPA" },
                        { "ai.cloud.roleInstance", "Blazor Server" },
                    }
                };

                await applicationInsights.AddTelemetryInitializer(telemetryItem);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}