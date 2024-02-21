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
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var user = await _usersService.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }
        
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        await _usersService.CreateUserAsync(user);

        return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
    }

    [HttpPost("auth")]
    public async Task<ActionResult<User>> AuthenticateUser(Credentials credentials)
    {
        var user = await _usersService.AuthenticateUserAsync(credentials);

        if (user == null)
        {
            return BadRequest();
        }
        
        return Ok(user);
    }
}
