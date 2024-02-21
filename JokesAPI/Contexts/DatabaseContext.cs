using JokesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JokesAPI.Contexts;

public class DatabaseContext : DbContext
{
    private  IConfiguration _configuration { get; }

    public DatabaseContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Joke> Jokes { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var ConnectionString = _configuration["ConnectionStrings:database"];
        optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
    }
}
