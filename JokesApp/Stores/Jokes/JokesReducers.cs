using Fluxor;

namespace JokesApp.Stores.Jokes;

public static class JokesReducers
{
	[ReducerMethod(typeof(FetchJokeAction))]
	public static JokesState ReduceFetchJokeById(JokesState state)
	{
		// Set IsLoading to true only if no joke has been fetched before
		var shouldSetLoading = state.Joke == null;
		
		return state with
		{
			IsLoading = shouldSetLoading,
            HasErrored = false,
			Joke = shouldSetLoading ? null : state.Joke,
        };
	}

    [ReducerMethod]
	public static JokesState ReduceFetchJokeSucceeded(JokesState state, FetchJokeSucceededAction action)
	{
		return state with
		{
			HasErrored = false,
			IsLoading = false,
			Joke = action.Joke
		};
	}

	[ReducerMethod]
	public static JokesState ReduceFetchJokeFailed(JokesState state, FetchJokeFailedAction action)
	{
		return state with
		{
            IsLoading = false,
            HasErrored = true,
			ErrorMessage = action.Reason
		};
	}
}
