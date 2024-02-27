using Fluxor;
using JokesApp.Models;

namespace JokesApp.Stores.Profile;

[FeatureState]
public record ProfileState
{
    public User? User { get; set; }

    public bool isLoading = true;

    public bool hasErrored = false;

    private ProfileState() { }

    public ProfileState(User user)
    {
        User = user;
    }
}
