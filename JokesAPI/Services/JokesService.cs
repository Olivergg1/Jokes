using JokesAPI.Contexts;
using JokesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JokesAPI.Services;

public interface IJokesService
{
    Task<JokeDetailDto?> GetJokeByIdAsync(int id);
    Task<List<JokeDto>?> GetJokesAsync();
    Task<JokeDetailDto?> GetRandomJokeAsync();

    Task<Joke?> AddJokeAsync(Joke joke);
}

public class JokesService : IJokesService
{
    private DatabaseContext? _databaseContext { get; set; }

    public JokesService(DatabaseContext databaseContext) 
    { 
        _databaseContext = databaseContext;
    }

    public async Task<JokeDetailDto?> GetJokeByIdAsync(int id)
    {
        return await _databaseContext.Jokes
            .Include(joke => joke.Author)
            .Select(joke => new JokeDetailDto
            {
                Id = joke.Id,
                Content = joke.Content,
                AuthorId = joke.AuthorId,
                AuthorName = joke.Author.Name
            })
            .SingleOrDefaultAsync(joke => joke.Id == id);
    }

    public async Task<List<JokeDto>?> GetJokesAsync()
    {
        return await _databaseContext.Jokes
            .Include(joke => joke.Author)
            .Select(joke => new JokeDto
            {
                Id = joke.Id,
                Content = joke.Content,
                AuhtorId = joke.AuthorId
            })
            .ToListAsync();
    }

    public async Task<JokeDetailDto?> GetRandomJokeAsync()
    {
        var count = await _databaseContext.Jokes.CountAsync();
        var offset = new Random(Guid.NewGuid().GetHashCode()).Next(0, count);

        return await _databaseContext.Jokes
            .Include(joke => joke.Author)
            .Skip(offset)
            .Select(joke => new JokeDetailDto
            {
                Id = joke.Id,
                Content = joke.Content,
                AuthorId = joke.AuthorId,
                AuthorName = joke.Author.Name
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Joke?> AddJokeAsync(Joke joke)
    {
        await _databaseContext.Jokes.AddAsync(joke);
        await _databaseContext.SaveChangesAsync();

        return joke;
    }
}
