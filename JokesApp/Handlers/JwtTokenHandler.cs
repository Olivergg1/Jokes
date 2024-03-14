
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Net.Http.Headers;

namespace JokesApp.Handlers;

public interface IJwtTokenHandler { }

public class JwtTokenHandler : DelegatingHandler, IJwtTokenHandler
{
    private readonly ILocalStorageService _localStorageService;

    public JwtTokenHandler(ILocalStorageService localStorageService)
    {
        InnerHandler = new HttpClientHandler();
        _localStorageService = localStorageService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        
        var token = await GetJwtTokenAsync();
        
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<string?> GetJwtTokenAsync()
    {
        return await _localStorageService.GetItemAsStringAsync("Token");
    }
}