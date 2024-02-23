using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using TechnicalEvaluation.Infrastructure;
using TechnicalEvaluation.Presentation.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new ApiClient(builder.HostEnvironment.BaseAddress, new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }));

await builder.Build().RunAsync();
