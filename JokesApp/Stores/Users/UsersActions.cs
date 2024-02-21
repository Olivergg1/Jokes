using JokesApp.Models;

namespace JokesApp.Stores.Users;

public record UserLoginAction(Credentials credentilas);
public record UserLoginSuccessAction(User user);

public record UserLogoutAction();
public record UserLogoutSuccessAction();
