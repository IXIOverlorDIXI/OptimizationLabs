using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using OptimizationLabs.UI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddScoped(_ =>
{
    var baseUrl = builder.Configuration["APIs:API:Url"] ?? string.Empty;

    var client = new HttpClient();
    client.BaseAddress = new Uri(baseUrl);

    return client;
});

await builder.Build().RunAsync();