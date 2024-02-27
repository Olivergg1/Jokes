﻿using Fluxor;
using JokesApp.Stores.Jokes;
using Microsoft.AspNetCore.Components;
using JokesApp.Models;
using System;

namespace JokesApp.Pages;

public partial class Jokes 
{
	[Inject]
	private IState<JokesState>? _jokesState { get; set; }

	[Inject]
	private IDispatcher? _dispatcher { get; set; }

    [Inject]
    private NavigationManager? _navigationManager { get; set; }

    [Parameter]
    public int? Id { get; set; }

    private bool isInitial = true;

    private bool isFetchErrored = false;
    private string? errorReason;

    public Joke? Joke => _jokesState?.Value.Joke;

    protected override void OnInitialized()
	{
        Console.WriteLine(Id ?? -1);
		base.OnInitialized();
        UpdateJoke();

        SubscribeToAction<JokeFetchedAction>(OnJokeFetchedAction);
        SubscribeToAction<FetchJokeFailedAction>(OnFetchJokeFailedAction);
        SubscribeToAction<FetchJokeTimeoutAction>(OnFetchJokeTimeoutAction);
    }

    public void UpdateJoke()
    {
        if (Id.HasValue && isInitial)
        {
            isInitial = false;

            _dispatcher?.Dispatch(new FetchJokeByIdAction(Id.Value));
            return;
        }

        // Fetch random joke if Id is not set
        isInitial = false;
        _dispatcher?.Dispatch(new FetchRandomJokeAction());
    }

    public void OnJokeFetchedAction(JokeFetchedAction action)
    {
        _navigationManager?.NavigateTo(action.Joke.Id.ToString());
    }

    public void OnFetchJokeFailedAction(FetchJokeFailedAction action)
    {
        errorReason = action.Reason;
        isFetchErrored = true;
    }

    public void OnFetchJokeTimeoutAction(FetchJokeTimeoutAction action)
    {
        errorReason = action.Reason;
        isFetchErrored = true;
    }
}