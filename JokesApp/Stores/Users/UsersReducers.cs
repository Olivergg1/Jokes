using Fluxor;

namespace JokesApp.Stores.Users;

public class UsersReducers
{
    [ReducerMethod]
    public static UsersState ReduceUserLoginSuccessAction(UsersState state, UserLoginSuccessAction action)
    {
        return state with
        {
            User = action.user,
        };
    }

    [ReducerMethod]
    public static UsersState ReduceUserLogoutSuccessAction(UsersState state, UserLogoutSuccessAction action)
    {
        return state with
        {
            User = null
        };
    }
}
