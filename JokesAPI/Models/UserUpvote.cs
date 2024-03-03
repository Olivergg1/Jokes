namespace JokesAPI.Models;

public class UserUpvote
{
    public int UpvoterId { get; set; }
    public int UpvotedUserId { get; set; }

    public User Upvoter { get; set; }
    public User UpvotedUser { get; set; }
}

public class UpvoteDto()
{
    public int UpvoterId { get; set; }
    public int UpvotedUserId { get; set; }
}