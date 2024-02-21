using JokesApp.Models;

namespace JokesApp.Stores.Jokes;

public record FetchRandomJokeAction();
public record FetchJokeByIdAction(int id);

public record JokeFetchedAction(Joke joke, User user);
public record FetchJokeFailedAction() : ErrorAction();
public record FetchJokeTimeoutAction() : ErrorAction();

public record AddJokeAction(Joke joke);
public record JokeAddedAction(Joke joke);
public record JokeAddFailedAction();

public abstract record ErrorAction
{
    public string? Reason { get; init; }
};