using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using PocketMate.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

//builder.Services
//    .AddHttpClient(Configuration.HTTP_CLIENT_NAME, options => {
//        options.BaseAddress = new Uri(Configuration.BACKEND_URL);
//    })
//    .AddHttpMessageHandler<CookieHandler>();

await builder.Build().RunAsync();
