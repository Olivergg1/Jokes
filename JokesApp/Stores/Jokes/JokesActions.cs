using JokesApp.Models;
using JokesApp.Stores.Generic;

namespace JokesApp.Stores.Jokes;

public record FetchJokeAction();
public record FetchRandomJokeAction() : FetchJokeAction();
public record FetchJokeByIdAction(int Id) : FetchJokeAction();

public record FetchJokeSucceededAction(Joke Joke);
public record FetchJokeFailedAction() : ErrorAction();

public record AddJokeAction(Joke Joke);
public record JokeAddedAction(Joke Joke);
public record JokeAddFailedAction();