using Fluxor;

namespace JokesApp.Stores.Profile;

public class ProfileReducers
{
    [ReducerMethod]
    public static ProfileState ReduceFetchProfileSucceeded(ProfileState state, FetchProfileSucceededAction action)
    {
        return state with
        {
            isLoading = false,
            hasErrored = false,
            User = action.User,
        };
    }

    [ReducerMethod(typeof(FetchProfileByIdAction))]
    public static ProfileState ReduceFetchProfileById(ProfileState state)
    {
        return state with
        {
            isLoading = true,
            hasErrored = false,
        };
    }

    [ReducerMethod(typeof(FetchProfileFailedAction))]
    public static ProfileState ReduceFetchProfileFailed(ProfileState state)
    {
        return state with
        {
            isLoading = false,
            hasErrored = true
        };
    }
}
