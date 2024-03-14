using JokesApp.Models;
using JokesApp.Constants;
using System.Net;
using System.Net.Http.Json;
using Fluxor;
using JokesApp.Stores.Profile;

namespace JokesApp.Services;

public interface IUsersService
{
    Task<User?> GetUserById(int id, int senderId);
    Task<bool> ToggleUpvoteAsync(Upvote upvote);
}

public class UsersService : BaseService, IUsersService
{
    public UsersService(ApiService apiService, IDispatcher dispatcher) : base(apiService, dispatcher) { }

    public async Task<User?> GetUserById(int id, int senderId = 0)
    {
        try
        {
            var cts = new CancellationTokenSource();
            var response = await ApiService.GetAsync($"users/{id}", cts.Token);

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

    public async Task<bool> ToggleUpvoteAsync(Upvote upvote)
    {
        try
        {
            var cts = new CancellationTokenSource();
            var response = await ApiService.PostAsJsonAsync($"users/upvote", upvote, cts.Token);

            response.EnsureSuccessStatusCode();

            return true;
        }
        catch (HttpRequestException exception)
        {
            switch (exception.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    Dispatcher.Dispatch(new ToggleUpvoteFailedAction());
                    break;
                default:
                    // Server timeout (not reachable)
                    Dispatcher.Dispatch(new ToggleUpvoteFailedAction());
                    break;
            }

            return false;
        }
    }
}