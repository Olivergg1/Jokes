using JokesAPI.Models;
using JokesAPI.Services;
using JokesApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JokesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private IUsersService _usersService;
    private UserManager<User> _userManager;

    public UsersController(IUsersService usersService, UserManager<User> userManager)
    {
        _usersService = usersService;
        _userManager = userManager;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDetailDto?>> GetUserById(int id)
    {
        var sender = await _usersService.GetUserByUsername(User.Identity?.Name ?? string.Empty);
        var user = await _usersService.GetUserByIdAsync(id, sender?.Id);

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

    [Authorize]
    [HttpPost("upvote")]
    public async Task<ActionResult> UpvoteUser([FromBody] UpvoteDto upvoteDto)
    {
        var sender = await _usersService.GetUserByUsername(User.Identity?.Name ?? string.Empty);
        var success = await _usersService.ToggleUpvoteAsync(upvoteDto, sender.Id);

        if (!success)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpPost("auth")]
    public async Task<ActionResult> AuthenticateUser()
    {
        var res = User?.Identity?.IsAuthenticated;

        if (res.HasValue && !res.Value)
        {
            return BadRequest();
        }
        
        return Ok();
    }
}
