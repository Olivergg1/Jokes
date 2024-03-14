using JokesApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Fluxor;
using Blazored.LocalStorage;
using JokesApp.Services;
using JokesApp.Handlers;
using Microsoft.AspNetCore.Components.Authorization;
using JokesApp.Providers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddScoped<JwtTokenHandler>();
builder.Services.AddScoped<ApiService>();

builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
builder.Services.AddScoped(sp => (JwtAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddFluxor(o => o.ScanAssemblies(typeof(Program).Assembly));

builder.Services.AddScoped<IUsersService, UsersService>();

await builder.Build().RunAsync();
