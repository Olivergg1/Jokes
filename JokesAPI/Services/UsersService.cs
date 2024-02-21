using JokesAPI.Contexts;
using JokesAPI.Models;
using JokesApp.Models;
using Microsoft.EntityFrameworkCore;

namespace JokesAPI.Services;

public interface IUsersService
{
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> CreateUserAsync(User user);
    Task<User?> AuthenticateUserAsync(Credentials credentials);
}

public class UsersService : IUsersService
{
    private DatabaseContext? _databaseContext { get; set; }

    public UsersService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _databaseContext.Users.FindAsync(id);
    }

    public async Task<User?> CreateUserAsync(User user)
    {
        var result = await _databaseContext.Users.AddAsync(user);
        await _databaseContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<User?> AuthenticateUserAsync(Credentials credentials)
    {
        var user = await _databaseContext.Users.Where(u => u.Username == credentials.Username).FirstOrDefaultAsync();

        return user;
    }
}
