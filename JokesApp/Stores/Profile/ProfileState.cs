using Fluxor;
using JokesApp.Models;
using JokesApp.Stores.Generic;

namespace JokesApp.Stores.Profile;

[FeatureState]
public record ProfileState : ErrorableState
{
    public User? User { get; set; }

    public bool IsLoading = true;
}
