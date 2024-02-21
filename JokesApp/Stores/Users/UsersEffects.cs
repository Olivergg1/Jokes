using Blazored.LocalStorage;
using Fluxor;
using JokesApp.Models;
using System.Net.Http.Json;

namespace JokesApp.Stores.Users;

public class UsersEffects
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public UsersEffects(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _localStorage = localStorageService;
    }

    [EffectMethod]
    public async Task AttemptUserLogin(UserLoginAction action, IDispatcher dispatcher)
    {
        var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "api/users/auth", action.credentilas);

        if (response.IsSuccessStatusCode)
        {
            // LOGIN
            var user = await response.Content.ReadFromJsonAsync<User>();
            await _localStorage.SetItemAsync("UserKey", user);

            dispatcher.Dispatch(new UserLoginSuccessAction(user));
        }
        else
        {
            // TODO: Failed to login
        }
    }

    [EffectMethod(typeof(UserLogoutAction))]
    public async Task AttemptUserLogout(IDispatcher dispatcher)
    {
        var response = true; // TODO: Implement and replace with actual logout logic

        if (response)
        {
            dispatcher.Dispatch(new UserLogoutSuccessAction());
        }
        else
        {
            // TODO: Failed logout action
        }
    }
}