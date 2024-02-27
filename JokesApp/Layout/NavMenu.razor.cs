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

    public void RedirectToLogin()
    {
        NavigationManager?.NavigateTo("/login");
    }

    public string UserButtonImageStyle()
    {
        return $"background-image: url('{_user?.Image}');";
    }
}
