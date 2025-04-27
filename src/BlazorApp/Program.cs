using BlazorApp;
using BlazorApp.Services;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using RazorSCLibrary;

using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

if(builder.HostEnvironment.IsDevelopment())
{
    Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(builder.Configuration["SyncfusionApiKey"]);
    builder.Services.AddSyncfusionBlazor();
}
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<HeroImageService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<LogoService>();

builder.Services.AddScoped<JsMethods>();

await builder.Build().RunAsync();
