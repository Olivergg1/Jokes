using JokesApp.Models;
using JokesApp.Stores.Generic;

namespace JokesApp.Stores.Users;

public record UserLoginAction(Credentials Credentials);
public record UserLoginSucceededAction(User User);
public record UserLoginFailedAction() : ErrorAction();

public record UserLogoutAction();
public record UserLogoutSucceededAction();

public record TryLoadUserFromLocalstorageAction();
public record SetUserAction(User User);

public record UserAuthenticateAction();
public record UserAuthenticateSucceededAction(User User);