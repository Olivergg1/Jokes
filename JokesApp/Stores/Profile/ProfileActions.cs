using JokesApp.Models;
using JokesApp.Stores.Generic;
using JokesApp.Stores.Jokes;

namespace JokesApp.Stores.Profile;

public record FetchProfileByIdAction(int Id);
public record FetchProfileSucceededAction(User User);
public record FetchProfileFailedAction() : ErrorAction();
