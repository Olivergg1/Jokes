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
    public DbSet<UserUpvote> UsersUpvote { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketType> TicketTypes { get; set; }

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
            .WithMany(u => u.UpvotedUsers)
            .HasForeignKey(u => u.UpvoterId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<UserUpvote>()
            .HasOne(u => u.UpvotedUser)
            .WithMany(u => u.Upvoters)
            .HasForeignKey(u => u.UpvotedUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.TicketType)
            .WithMany(t => t.Tickets)
            .HasForeignKey(t => t.TycketTypeId)
            .IsRequired();

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Joke)
            .WithMany(j => j.Tickets)
            .HasForeignKey(t => t.JokeId)
            .IsRequired();
    }
}
