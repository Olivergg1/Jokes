using JokesAPI.Contexts;
using JokesAPI.Models;
using JokesApp.Models;
using Microsoft.EntityFrameworkCore;

namespace JokesAPI.Services;

public interface IUsersService
{
    Task<UserDetailDto?> GetUserByIdAsync(int id, int sender);
    Task<UserDto?> CreateUserAsync(User user);
    Task<User?> AuthenticateUserAsync(Credentials credentials);
    Task<bool> ToggleUpvoteAsync(UpvoteDto upvoteDto);
}

public class UsersService : IUsersService
{
    private DatabaseContext? _databaseContext { get; set; }

    public UsersService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    // TODO: Implement authorized requests and remove sender
    public async Task<UserDetailDto?> GetUserByIdAsync(int id, int sender)
    {
        return await _databaseContext.Users
            .Include(user => user.Jokes)
            .Include(user => user.Upvoters)
            .Select(user => new UserDetailDto 
            {
                Id = user.Id,
                Username = user.Username,
                Name = user.Name,
                Image = user.Image,
                Jokes = user.Jokes.ToList(),
                Upvotes = user.Upvoters.ToList().Count,
                HasUpvoted = user.Upvoters.Any(upvote => upvote.UpvoterId == sender && upvote.UpvotedUserId == id),
            })
            .SingleOrDefaultAsync(user => user.Id == id);
    }

    public async Task<bool> ToggleUpvoteAsync(UpvoteDto upvoteDto)
    {
        // Cannot upvote yourself
        if (upvoteDto.UpvoterId == upvoteDto.UpvotedUserId)
        {
            return false;
        }

        var upvoter = await _databaseContext.Users.FindAsync(upvoteDto.UpvoterId);
        var targetUser = await _databaseContext.Users.FindAsync(upvoteDto.UpvotedUserId);

        if (upvoter == null || targetUser == null)
        {
            return false;
        }

        var existingUpvote = await _databaseContext.UsersUpvote
            .FirstOrDefaultAsync(upvote => upvote.UpvoterId == upvoteDto.UpvoterId && upvote.UpvotedUserId == upvoteDto.UpvotedUserId);

        if (existingUpvote == null)
        {
            _databaseContext.UsersUpvote.Add(new UserUpvote
            {
                UpvoterId = upvoteDto.UpvoterId,
                UpvotedUserId = upvoteDto.UpvotedUserId,
            });
        }
        else
        {
            _databaseContext.UsersUpvote.Remove(existingUpvote);
        }

        await _databaseContext.SaveChangesAsync();

        return true;
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
