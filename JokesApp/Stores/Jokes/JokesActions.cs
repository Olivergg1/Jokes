using JokesApp.Models;
using JokesApp.Stores.Generic;

namespace JokesApp.Stores.Jokes;

public record FetchRandomJokeAction();
public record FetchJokeByIdAction(int id);

public record JokeFetchedAction(Joke Joke);
public record FetchJokeFailedAction() : ErrorAction();
public record FetchJokeTimeoutAction() : ErrorAction();

public record AddJokeAction(Joke joke);
public record JokeAddedAction(Joke joke);
public record JokeAddFailedAction();