using Fluxor;
using JokesApp.Providers;
using JokesApp.Services;

namespace JokesApp.Stores.Users;

public class UsersEffects
{
    private readonly ApiService _httpClient;
    private readonly JwtAuthenticationStateProvider _authState;

    public UsersEffects(ApiService httpClient, JwtAuthenticationStateProvider authState)
    {
        _httpClient = httpClient;
        _authState = authState;
    }

    [EffectMethod]
    public async Task AttemptUserLogin(UserLoginAction action, IDispatcher dispatcher)
    {
        var success = await _authState.LoginAsync(action.Credentials.Username, action.Credentials.Password);

        if (success)
        {
            dispatcher.Dispatch(new UserAuthenticateAction());
        }
        else
        {
            dispatcher.Dispatch(new UserLoginFailedAction { Reason = "Incorrect username or password☠️" });
        }
    }

    [EffectMethod(typeof(UserAuthenticateAction))]
    public async Task UserAuthenticate(IDispatcher dispatcher)
    {
        await _authState.CheckAuthenticationAsync();
    }

    [EffectMethod(typeof(UserLogoutAction))]
    public async Task AttemptUserLogout(IDispatcher dispatcher)
    {
        await _authState.LogoutAsync();
            
        dispatcher.Dispatch(new UserLogoutSucceededAction());
    }
}