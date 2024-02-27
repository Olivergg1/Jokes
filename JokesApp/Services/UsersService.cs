using JokesApp.Models;
using JokesApp.Constants;
using JokesApp.Stores.Jokes;
using System.Net;
using System.Net.Http.Json;
using Fluxor;
using JokesApp.Stores.Profile;

namespace JokesApp.Services;

public interface IUsersService
{
    Task<User?> GetUserById(int id);
}

public class UsersService : BaseService, IUsersService
{
    public UsersService(HttpClient httpClient, IDispatcher dispatcher) : base(httpClient, dispatcher) { }

    public async Task<User?> GetUserById(int id)
    {
        try
        {
            var cts = new CancellationTokenSource();
            var response = await HttpClient.GetAsync($"{HttpClient.BaseAddress}api/users/{id}", cts.Token);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<User>();
        }
        catch (HttpRequestException exception)
        {
            switch (exception.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    Dispatcher.Dispatch(new FetchProfileFailedAction { Reason = ServerErrorMessages.ServerUserNotFound });
                    break;
                default:
                    // Server timeout (not reachable)
                    Dispatcher.Dispatch(new FetchProfileFailedAction { Reason = ServerErrorMessages.ServerTimedOut });
                    break;
            }
            return null;
        }
    }
}
