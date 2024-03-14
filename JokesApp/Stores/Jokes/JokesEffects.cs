using Fluxor;
using JokesApp.Constants;
using JokesApp.Models;
using System.Net.Http.Json;
using System.Net;
using JokesApp.Services;

namespace JokesApp.Stores.Jokes;

public class JokesEffects
{
    private readonly ApiService _httpClient;

    public JokesEffects(ApiService httpClient)
    {
        _httpClient = httpClient;
    }

    [EffectMethod(typeof(FetchRandomJokeAction))]
    public async Task FetchRandomJoke(IDispatcher dispatcher)
    {
        var cts = new CancellationTokenSource();

        try
        {
            // Fetch random joke
            var jokeResponse = await _httpClient.GetAsync("Jokes/random", cts.Token);
            jokeResponse.EnsureSuccessStatusCode();

            var joke = await jokeResponse.Content.ReadFromJsonAsync<Joke>();

            dispatcher.Dispatch(new FetchJokeSucceededAction(joke));
        }
        catch (HttpRequestException exception)
        {
            switch (exception.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    dispatcher.Dispatch(new FetchJokeFailedAction { Reason = ServerErrorMessages.ServerJokeFetchError });
                    break;
                default:
                    // Server timeout (not reachable)
                    dispatcher.Dispatch(new FetchJokeFailedAction { Reason = ServerErrorMessages.ServerTimedOut });
                    break;
            }
        }
    }

    [EffectMethod]
    public async Task FetchJokeById(FetchJokeByIdAction action, IDispatcher dispatcher)
    {
        var cts = new CancellationTokenSource();
        
        try
        {
            // Fetch joke
            var jokeResponse = await _httpClient.GetAsync($"Jokes/{action.Id}", cts.Token);
            jokeResponse.EnsureSuccessStatusCode();

            var joke = await jokeResponse.Content.ReadFromJsonAsync<Joke>();

            dispatcher.Dispatch(new FetchJokeSucceededAction(joke));
        }
        catch (HttpRequestException exception)
        {
            switch (exception.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    dispatcher.Dispatch(new FetchJokeFailedAction { Reason = ServerErrorMessages.ServerJokeFetchError });
                    break;
                default:
                    // Server timeout (not reachable)
                    dispatcher.Dispatch(new FetchJokeFailedAction { Reason = ServerErrorMessages.ServerTimedOut });
                    break;
            }
        }
    }

    [EffectMethod]
    public async Task AddJoke(AddJokeAction action, IDispatcher dispatcher)
    {
        var response = await _httpClient.PostAsJsonAsync("Jokes", action.Joke);

        if (response.IsSuccessStatusCode)
        {
            var joke = await response.Content.ReadFromJsonAsync<Joke>();
            dispatcher.Dispatch(new JokeAddedAction(joke));
        }
        else
        {
            dispatcher.Dispatch(new JokeAddFailedAction());
        }
    }
}
