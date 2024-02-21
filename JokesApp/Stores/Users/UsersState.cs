using Fluxor;
using JokesApp.Models;

namespace JokesApp.Stores.Users;

[FeatureState]
public record UsersState
{
    public User? User { get; set; }
}
