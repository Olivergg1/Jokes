using Blazored.LocalStorage;

namespace JokesApp.Services;

public class JwtTokenService
{
    private ILocalStorageService _localStorageService;

    public JwtTokenService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task SaveTokenAsync(string token)
    {
        await _localStorageService.SetItemAsStringAsync("Token", token);
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _localStorageService.GetItemAsStringAsync("Token");
    }

    public async Task ClearTokenAsync()
    {
        await _localStorageService.RemoveItemAsync("Token");
    }
}
