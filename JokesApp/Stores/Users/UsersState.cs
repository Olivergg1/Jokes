using Fluxor;
using JokesApp.Models;
using JokesApp.Stores.Generic;

namespace JokesApp.Stores.Users;

[FeatureState]
public record UsersState : ErrorableState
{
    public User? User { get; set; }

    public bool IsLoading { get; set; } = false;
}
