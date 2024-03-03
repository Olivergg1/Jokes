using Fluxor;
using Microsoft.AspNetCore.Components;
using JokesApp.Stores.Profile;
using JokesApp.Models;
using JokesApp.Constants;
using JokesApp.Stores.Users;
using Microsoft.AspNetCore.Components.Routing;
using JokesApp.Stores.ModalStore;
using JokesApp.Modals;

namespace JokesApp.Pages;

public partial class Profile : IDisposable
{
    [Parameter]
    public int? Id { get; set; }

    [Inject]
    public IDispatcher? Dispatcher { get; set; }

    [Inject]
    public NavigationManager? NavigationManager { get; set; }

    [Inject]
    public IState<ProfileState>? ProfileState { get; set; }

    [Inject]
    public IState<UsersState>? UsersState { get; set; }

    public User? LoggedInUser => UsersState?.Value.User;
    public bool IsLoggedIn => LoggedInUser != null;
    public bool IsSelf => LoggedInUser?.Id == Id;

    public User? User => ProfileState?.Value?.User;
    public int JokesCount => User?.Jokes?.Count ?? 0;

    public string? ErrorMessage => ProfileState?.Value.ErrorMessage;

    public const int MaxNumberOfJokes = 6;

    private int _offset = 0;
    public int MaxPaginations => JokesCount / MaxNumberOfJokes;
    public int GetJokesOffset() => _offset * MaxNumberOfJokes;

    public int GetPaginationUpperLimit => int.Min(GetJokesOffset() + MaxNumberOfJokes, JokesCount);
    public string GetPaginationText => $"Showing {GetJokesOffset() + 1} - {GetPaginationUpperLimit} of total {JokesCount}";

    private bool upvoteButtonDisabled = false;
    private string upvoteButtonText => User!.HasUpvoted ? "❌ remove upvote" : "⬆️ upvote";

    protected override void OnInitialized()
    {
        base.OnInitialized();

        FetchUser();

        NavigationManager!.LocationChanged += HandleLocationChanged;

        SubscribeToAction<ToggleUpvoteSuccededAction>(OnToggleUpvoteSucceeded);
        SubscribeToAction<UserLogoutSucceededAction>(OnUserLogoutSucceededAction);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (User!.Id == Id)
        {
            NavigationManager!.LocationChanged -= HandleLocationChanged;
        }
    }

    public void HandleLocationChanged(object? sender, LocationChangedEventArgs args)
    {
        FetchUser();
    }

    public void FetchUser()
    {
        if (Id.HasValue)
        {
            Dispatcher?.Dispatch(new FetchProfileByIdAction(Id.Value, LoggedInUser?.Id ?? 0));
        }
        else
        {
            Dispatcher?.Dispatch(new FetchProfileFailedAction { Reason = ErrorMessages.UserIdNotProvided });
        }
    }

    public void ToggleUpvote()
    {
        upvoteButtonDisabled = true;
        Dispatcher?.Dispatch(new ToggleProfileUpvoteAction(new Upvote { UpvoterId = LoggedInUser!.Id, UpvotedUserId = Id!.Value }));
    }

    public void OnToggleUpvoteSucceeded(ToggleUpvoteSuccededAction action)
    {
        upvoteButtonDisabled = false;
    }

    public void OnUserLogoutSucceededAction(UserLogoutSucceededAction action)
    {
        NavigationManager?.NavigateTo("");
    }

    private void increasePagination()
    {
        _offset = Math.Clamp(_offset + 1, 0, MaxPaginations);
    }

    private void decreasePagination()
    {
        _offset = Math.Clamp(_offset - 1, 0, MaxPaginations);
    }

    public void OpenLogoutModal()
    {
        Dispatcher?.Dispatch(new ShowModalAction(typeof(LogoutModal)));
    }
}