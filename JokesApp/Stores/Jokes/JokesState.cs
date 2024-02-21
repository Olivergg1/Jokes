using Fluxor;
using JokesApp.Models;

namespace JokesApp.Stores.Jokes;

[FeatureState]
public record JokesState
{	 
	public Joke? Joke { get; set; }

	public User? JokeAuthor { get; set; }

	private JokesState() { }

	public JokesState(Joke joke)
	{
		Joke = joke;
	}
}