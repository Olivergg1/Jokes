using Fluxor;
using Microsoft.AspNetCore.Components;
using JokesApp.Stores.Profile;
using JokesApp.Models;

namespace JokesApp.Pages;

public partial class Profile
{
    [Parameter]
    public int? Id { get; set; }

    [Inject]
    public IDispatcher? Dispatcher { get; set; }

    [Inject]
    public IState<ProfileState>? ProfileState { get; set; }

    public User? User => ProfileState?.Value?.User;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (Id.HasValue)
        {
            Dispatcher?.Dispatch(new FetchProfileByIdAction(Id.Value));
        }
    }
}