using JokesApp.Models;
using JokesApp.Stores.Generic;

namespace JokesApp.Stores.Profile;

public record FetchProfileByIdAction(int Id, int SenderId);
public record FetchProfileSucceededAction(User User);
public record FetchProfileFailedAction() : ErrorAction();

public record ToggleProfileUpvoteAction(Upvote Upvote);
public record ToggleUpvoteSuccededAction();
public record ToggleUpvoteFailedAction() : ErrorAction();