﻿using Fluxor;
using JokesApp.Constants;
using JokesApp.Models;
using System.Net.Http.Json;
using System.Net;

namespace JokesApp.Stores.Jokes;

public class JokesEffects
{
    private readonly HttpClient _httpClient;

    public JokesEffects(HttpClient httpClient)
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
            var jokeResponse = await _httpClient.GetAsync(_httpClient.BaseAddress + "api/Jokes/random", cts.Token);
            jokeResponse.EnsureSuccessStatusCode();

            var joke = await jokeResponse.Content.ReadFromJsonAsync<Joke>();

            // Fetch author of joke
            var userResponse = await _httpClient.GetAsync(_httpClient.BaseAddress + "api/Users/" + joke.AuthorId);
            userResponse.EnsureSuccessStatusCode();

            var user = await userResponse.Content.ReadFromJsonAsync<User>();

            dispatcher.Dispatch(new JokeFetchedAction(joke, user));
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
                    dispatcher.Dispatch(new FetchJokeTimeoutAction { Reason = ServerErrorMessages.ServerTimedOut });
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
            var jokeResponse = await _httpClient.GetAsync(_httpClient.BaseAddress + "api/Jokes/" + action.id, cts.Token);
            jokeResponse.EnsureSuccessStatusCode();

            var joke = await jokeResponse.Content.ReadFromJsonAsync<Joke>();

            // Fetch author of joke
            var userResponse = await _httpClient.GetAsync(_httpClient.BaseAddress + "api/Users/" + joke.AuthorId);
            userResponse.EnsureSuccessStatusCode();
            
            var user = await userResponse.Content.ReadFromJsonAsync<User>();

            dispatcher.Dispatch(new JokeFetchedAction(joke, user));
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
                    dispatcher.Dispatch(new FetchJokeTimeoutAction { Reason = ServerErrorMessages.ServerTimedOut });
                    break;
            }
        }
    }

    [EffectMethod]
    public async Task AddJoke(AddJokeAction action, IDispatcher dispatcher)
    {
        var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "api/Jokes/", action.joke);

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
