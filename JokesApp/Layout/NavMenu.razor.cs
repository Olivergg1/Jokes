using Fluxor;
using JokesApp.Models;
using JokesApp.Stores.Users;
using Microsoft.AspNetCore.Components;

namespace JokesApp.Layout;

public partial class NavMenu
{
    [Inject]
    public NavigationManager? NavigationManager { get; set; }

    [Inject]
    public IState<UsersState>? UsersState { get; set; }

    private User? _user => UsersState?.Value.User;

    public bool isAuthenticated => _user != null;

    private bool collapseNavMenu = true;

    private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

    private void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }

    public void RedirectToLogin()
    {
        NavigationManager?.NavigateTo("/login");
    }
}
