using Fluxor;
using JokesApp.Stores.Users;
using Microsoft.AspNetCore.Components;

namespace JokesApp.Layout;

public partial class MainLayout
{
    [Inject]
    public IDispatcher? Dispatcher { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Dispatcher?.Dispatch(new TryLoadUserFromLocalstorageAction());
    }
}