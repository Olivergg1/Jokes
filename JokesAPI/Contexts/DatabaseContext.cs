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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserUpvote>()
            .HasKey(u => new { u.UpvoterId, u.UpvotedUserId });

        modelBuilder.Entity<UserUpvote>()
            .HasOne(u => u.Upvoter)
            .WithMany(u => u.Upvoters)
            .HasForeignKey(u => u.UpvoterId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<UserUpvote>()
            .HasOne(u => u.UpvotedUser)
            .WithMany(u => u.UpvotedUsers)
            .HasForeignKey(u => u.UpvotedUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
