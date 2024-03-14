using Fluxor;
using JokesApp.Models;
using JokesApp.Providers;
using JokesApp.Stores.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;

namespace JokesApp.Pages;

public partial class Login
{
    [Inject]
    public IDispatcher? Dispatcher { get; set; }

    [Inject]
    public IState<UsersState>? UsersState { get; set; }

    [Inject]
    public NavigationManager? NavigationManager { get; set; }

    [CascadingParameter]
    private Task<AuthenticationState>? AuthState { get; set; }

    [SupplyParameterFromForm]
    public Credentials? CredentialsModel { get; set; }

    public User? User => UsersState?.Value.User;

    public string ErrorMessage => UsersState!.Value.ErrorMessage;

    public bool LoginButtonDisabled => UsersState!.Value.IsLoading;

    public EditContext EditContext { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        CredentialsModel ??= new Credentials();
        EditContext = new EditContext(CredentialsModel);

        await AuthState;
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
