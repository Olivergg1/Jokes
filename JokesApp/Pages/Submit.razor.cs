using Fluxor;
using JokesApp.Models;
using JokesApp.Stores.Jokes;
using JokesApp.Stores.Users;
using Microsoft.AspNetCore.Components;

namespace JokesApp.Pages;

public partial class Submit
{
    [Inject]
    public NavigationManager? NavigationManager { get; set; }

    [Inject]
    public IDispatcher? Dispatcher { get; set; }

    [Inject]
    public IState<UsersState>? UsersState { get; set; }

    [SupplyParameterFromForm]
    public Joke? JokeModel { get; set; }

    public User? User => UsersState?.Value.User;

    protected override void OnInitialized() {
        base.OnInitialized();

        if (User == null)
        {
            NavigationManager?.NavigateTo("/login");
        }

        JokeModel ??= new Joke();
        SubscribeToAction<JokeAddedAction>(OnJokeAddedAction);
    }

    public bool MaySubmit() => !string.IsNullOrEmpty(JokeModel?.Content);

    public void HandleSubmit()
    {
        JokeModel!.Id = 0;
        JokeModel!.AuthorId = User.Id;
        Dispatcher?.Dispatch(new AddJokeAction(JokeModel));
    }

    public void OnJokeAddedAction(JokeAddedAction action)
    {
        var path = String.Format("/{0}", action.Joke.Id);

        NavigationManager?.NavigateTo(path);
    }
}
