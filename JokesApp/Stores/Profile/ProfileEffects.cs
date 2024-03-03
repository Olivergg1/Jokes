using Fluxor;
using JokesApp.Services;

namespace JokesApp.Stores.Profile;

public class ProfileEffects
{
    public IUsersService UsersService { get; set; }

    public ProfileEffects(IUsersService usersService)
    {
        UsersService = usersService;
    }

    [EffectMethod]
    public async Task FetchUserById(FetchProfileByIdAction action, IDispatcher dispatcher)
    {
        var user = await UsersService.GetUserById(action.Id, action.SenderId);

        if (user != null)
        {
            dispatcher.Dispatch(new FetchProfileSucceededAction(user));
        }
    }

    [EffectMethod]
    public async Task ToggleUpvote(ToggleProfileUpvoteAction action, IDispatcher dispatcher)
    {
        var result = await UsersService.ToggleUpvoteAsync(action.Upvote);

        if (result)
        {
            dispatcher.Dispatch(new ToggleUpvoteSuccededAction());
        }
    }
}