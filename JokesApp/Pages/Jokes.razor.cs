using Fluxor;
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

    private string? _errorMessage => _jokesState?.Value.ErrorMessage;

    public Joke? Joke => _jokesState?.Value.Joke;

    protected override void OnInitialized()
	{
		base.OnInitialized();
        UpdateJoke();

        SubscribeToAction<FetchJokeSucceededAction>(OnFetchJokeSucceededAction);
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

    public void OnFetchJokeSucceededAction(FetchJokeSucceededAction action)
    {
        _navigationManager?.NavigateTo(action.Joke.Id.ToString());
    }
}