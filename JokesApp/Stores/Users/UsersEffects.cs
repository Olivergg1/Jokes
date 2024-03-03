using Blazored.LocalStorage;
using Fluxor;
using JokesApp.Constants;
using JokesApp.Models;
using JokesApp.Stores.Profile;
using System.Net;
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
        try
        {
            var cts = new CancellationTokenSource();
            var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "api/users/auth", action.Credentials, cts.Token);

            response.EnsureSuccessStatusCode();

            var user = await response.Content.ReadFromJsonAsync<User>();
            await _localStorage.SetItemAsync("UserKey", user);

            dispatcher.Dispatch(new UserLoginSucceededAction(user));
        }
        catch (HttpRequestException exception)
        {
            switch (exception.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    dispatcher.Dispatch(new UserLoginFailedAction { Reason = "Incorrect username or password☠️" });
                    break;
                default:
                    // Server timeout (not reachable)
                    dispatcher.Dispatch(new UserLoginFailedAction { Reason = "Server did not respond in time😴" });
                    break;
            }
        }
    }

    [EffectMethod(typeof(TryLoadUserFromLocalstorageAction))]
    public async Task TryLoadUserFromLocalstorage(IDispatcher dispatcher)
    {
        var user = await _localStorage.GetItemAsync<User>("UserKey");

        if (user != null)
        {
            dispatcher.Dispatch(new SetUserAction(user));
        }
    }

    [EffectMethod(typeof(UserLogoutAction))]
    public async Task AttemptUserLogout(IDispatcher dispatcher)
    {
        await _localStorage.RemoveItemAsync("UserKey");
            
        dispatcher.Dispatch(new UserLogoutSuccessAction());
    }
}