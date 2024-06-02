using BlazorAppFrontend;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;



var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuration des services HttpClient pour chaque API
builder.Services.AddHttpClient("UserRegistrationAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5080/api/");
});


// Ajout d'un HttpClient par défaut
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();