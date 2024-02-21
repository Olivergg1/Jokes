using JokesAPI.Contexts;
using JokesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JokesAPI.Services;

public interface IJokesService
{
    Task<Joke?> GetJokeByIdAsync(int id);
    Task<List<Joke>?> GetJokesAsync();
    Task<Joke?> GetRandomJokeAsync();

    Task<Joke?> AddJokeAsync(Joke joke);
}

public class JokesService : IJokesService
{
    private DatabaseContext? _databaseContext { get; set; }

    public JokesService(DatabaseContext databaseContext) 
    { 
        _databaseContext = databaseContext;
    }

    public async Task<Joke?> GetJokeByIdAsync(int id)
    {
        return await _databaseContext.Jokes.FindAsync(id);
    }

    public async Task<List<Joke>?> GetJokesAsync()
    {
        return await _databaseContext.Jokes.ToListAsync();
    }

    public async Task<Joke?> GetRandomJokeAsync()
    {
        var count = await _databaseContext.Jokes.CountAsync();
        var offset = new Random(Guid.NewGuid().GetHashCode()).Next(0, count);

        return await _databaseContext.Jokes.Skip(offset).FirstOrDefaultAsync();
    }

    public async Task<Joke?> AddJokeAsync(Joke joke)
    {
        await _databaseContext.Jokes.AddAsync(joke);
        await _databaseContext.SaveChangesAsync();

        return joke;
    }
}
