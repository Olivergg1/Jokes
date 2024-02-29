using Fluxor;
using JokesApp.Models;
using JokesApp.Stores.Generic;

namespace JokesApp.Stores.Jokes;

[FeatureState]
public record JokesState : ErrorableState
{
	public Joke? Joke { get; set; }

	public bool IsLoading { get; set; } = true;
}