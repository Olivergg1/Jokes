using Fluxor;
using JokesApp.Stores.ModalStore;
using JokesApp.Stores.Users;
using Microsoft.AspNetCore.Components;

namespace JokesApp.Modals;

public partial class LogoutModal
{
    [Inject]
    public IDispatcher? Dispatcher { get; set; }

    [Inject]
    public NavigationManager? NavigationManager { get; set; }

    private bool optionButtonsDisabled = false;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        SubscribeToAction<UserLogoutSucceededAction>(OnUserLogoutSucceededAction);
    }

    public void OnUserLogoutSucceededAction(UserLogoutSucceededAction _)
    {
        optionButtonsDisabled = false;
        CloseModal();
        NavigationManager?.NavigateTo("");
    }

    public void CloseModal()
    {
        Dispatcher?.Dispatch(new CloseModalAction());
    }

    public void HandleLogout()
    {
        optionButtonsDisabled = true;
        Dispatcher?.Dispatch(new UserLogoutAction());
    }
}
