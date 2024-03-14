using Fluxor;

namespace JokesApp.Stores.Users;

public class UsersReducers
{
    [ReducerMethod(typeof(UserLoginAction))]
    public static UsersState ReduceUserLoginAction(UsersState state)
    {
        return state with
        {
            IsLoading = true,
            HasErrored = false,
        };
    }

    [ReducerMethod]
    public static UsersState ReduceUserLoginSuccessAction(UsersState state, UserLoginSucceededAction action)
    {
        return state with
        {
            User = action.User,
            HasErrored = false,
            IsLoading = false,
        };
    }

    [ReducerMethod]
    public static UsersState ReduceUserAuthenticateAction(UsersState state, UserAuthenticateSucceededAction action)
    {
        return state with
        {
            User = action.User,
            HasErrored = false,
            IsLoading = false
        };
    }

    [ReducerMethod]
    public static UsersState ReduceUserLoginFailedAction(UsersState state, UserLoginFailedAction action)
    {
        return state with
        {
            ErrorMessage = action.Reason,
            HasErrored = true,
            IsLoading = false,
        };
    }

    [ReducerMethod]
    public static UsersState ReduceSetUserAction(UsersState state, SetUserAction action)
    {
        return state with
        {
            User = action.User
        };
    }

    [ReducerMethod]
    public static UsersState ReduceUserLogoutSuccessAction(UsersState state, UserLogoutSucceededAction action)
    {
        return state with
        {
            User = null
        };
    }
}
