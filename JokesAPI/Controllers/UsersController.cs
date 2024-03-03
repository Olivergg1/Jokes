using JokesAPI.Contexts;
using JokesAPI.Models;
using JokesAPI.Services;
using JokesApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JokesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDetailDto?>> GetUserById(int id, [FromQuery(Name = "sender")] int senderId)
    {
        var user = await _usersService.GetUserByIdAsync(id, senderId);

        if (user == null)
        {
            return NotFound();
        }
        
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto?>> CreateUser(User user)
    {
        await _usersService.CreateUserAsync(user);

        return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
    }

    [HttpPost("upvote")]
    public async Task<ActionResult> UpvoteUser([FromBody] UpvoteDto upvoteDto)
    {
        var success = await _usersService.ToggleUpvoteAsync(upvoteDto);

        if (!success)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpPost("auth")]
    public async Task<ActionResult<UserDto?>> AuthenticateUser(Credentials credentials)
    {
        var user = await _usersService.AuthenticateUserAsync(credentials);

        if (user == null)
        {
            return BadRequest();
        }
        
        return Ok(user);
    }
}
