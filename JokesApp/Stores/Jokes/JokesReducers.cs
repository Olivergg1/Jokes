using Fluxor;

namespace JokesApp.Stores.Jokes;

public static class JokesReducers
{
	[ReducerMethod]
	public static JokesState OnJokeFetched(JokesState state, JokeFetchedAction action)
	{
		return state with
		{
			Joke = action.joke,
			JokeAuthor = action.user
		};
	}
}
