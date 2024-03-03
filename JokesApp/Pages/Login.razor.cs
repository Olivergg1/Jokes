using Fluxor;
using JokesApp.Models;
using JokesApp.Stores.Users;
using Microsoft.AspNetCore.Components;

namespace JokesApp.Pages;

public partial class Login
{
    [Inject]
    public IDispatcher? Dispatcher { get; set; }

    [Inject]
    public IState<UsersState>? UsersState { get; set; }

    [Inject]
    public NavigationManager? NavigationManager { get; set; }

    [SupplyParameterFromForm]
    public Credentials? CredentialsModel { get; set; }

    public User? User => UsersState?.Value.User;

    public string ErrorMessage => UsersState!.Value.ErrorMessage;

    public bool LoginButtonDisabled => UsersState!.Value.IsLoading;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        CredentialsModel ??= new Credentials();
    }

    public void HandleSubmit()
    {
        Dispatcher?.Dispatch(new UserLoginAction(CredentialsModel));
    }

    public void RedirectToHome()
    {
        NavigationManager?.NavigateTo("");
    }

    public void RedirectToProfile()
    {
        NavigationManager?.NavigateTo($"/users/{User!.Id}");
    }

    public void HandleLogout()
    {
        Dispatcher?.Dispatch(new UserLogoutAction());
    }
}
