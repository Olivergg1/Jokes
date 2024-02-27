using JokesAPI.Contexts;
using JokesAPI.Models;
using JokesApp.Models;
using Microsoft.EntityFrameworkCore;

namespace JokesAPI.Services;

public interface IUsersService
{
    Task<UserDetailDto?> GetUserByIdAsync(int id);
    Task<UserDto?> CreateUserAsync(User user);
    Task<User?> AuthenticateUserAsync(Credentials credentials);
}

public class UsersService : IUsersService
{
    private DatabaseContext? _databaseContext { get; set; }

    public UsersService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<UserDetailDto?> GetUserByIdAsync(int id)
    {
        return await _databaseContext.Users
            .Include(user => user.Jokes)
            .Select(user => new UserDetailDto 
            {
                Id = user.Id,
                Username = user.Username,
                Name = user.Name,
                Image = user.Image,
                Jokes = user.Jokes.ToList()
            })
            .SingleOrDefaultAsync(user => user.Id == id);
    }

    public async Task<UserDto?> CreateUserAsync(User user)
    {
        var result = await _databaseContext.Users.AddAsync(user);
        await _databaseContext.SaveChangesAsync();

        var entity = result.Entity;
        var createdUser = new UserDto
        {
            Id = entity.Id,
            Username = entity.Username,
            Name = entity.Name,
            Image = entity.Image
        };

        return createdUser;
    }

    public async Task<User?> AuthenticateUserAsync(Credentials credentials)
    {
        var user = await _databaseContext.Users.Where(u => u.Username == credentials.Username).FirstOrDefaultAsync();

        return user;
    }
}
