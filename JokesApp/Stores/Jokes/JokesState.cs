using Fluxor;
using JokesApp.Models;

namespace JokesApp.Stores.Jokes;

[FeatureState]
public record JokesState
{	 
	public Joke? Joke { get; set; }
}