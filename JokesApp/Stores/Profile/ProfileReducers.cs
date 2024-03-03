using Fluxor;

namespace JokesApp.Stores.Profile;

public class ProfileReducers
{
    [ReducerMethod(typeof(FetchProfileByIdAction))]
    public static ProfileState ReduceFetchProfileById(ProfileState state)
    {
        return state with
        {
            IsLoading = true,
            HasErrored = false,
        };
    }

    [ReducerMethod]
    public static ProfileState ReduceFetchProfileSucceeded(ProfileState state, FetchProfileSucceededAction action)
    {
        return state with
        {
            IsLoading = false,
            HasErrored = false,
            User = action.User,
        };
    }

    [ReducerMethod]
    public static ProfileState ReduceFetchProfileFailed(ProfileState state, FetchProfileFailedAction action)
    {
        return state with
        {
            IsLoading = false,
            HasErrored = true,
            ErrorMessage = action.Reason
        };
    }

    [ReducerMethod(typeof(ToggleUpvoteSuccededAction))]
    public static ProfileState ReduceToggleUpvoteSucceeded(ProfileState state)
    {
        var user = state.User;

        user!.Upvotes = user.HasUpvoted ? user.Upvotes - 1 : user.Upvotes + 1;
        user!.HasUpvoted = !user.HasUpvoted;

        return state with
        {
            User = user
        };
    }
}