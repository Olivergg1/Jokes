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
}
