using JokesAPI.Contexts;
using JokesAPI.Models;
using JokesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JokesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JokesController : ControllerBase
{
    private readonly IJokesService _jokesService;

    public JokesController(IJokesService jokesService) 
    {
        _jokesService = jokesService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Joke>>> GetJokes()
    {
        var jokes = await _jokesService.GetJokesAsync();

        if (jokes == null)
        {
            return NotFound(jokes);
        }

        return Ok(jokes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Joke>> GetJoke(int id)
    {
        var joke = await _jokesService.GetJokeByIdAsync(id);

        if (joke == null)
        {
            return NotFound();
        }

        return Ok(joke);
    }

    [HttpGet("random")]
    public async Task<ActionResult<List<Joke>>> GetRandomJoke()
    {
        var joke = await _jokesService.GetRandomJokeAsync();

        if (joke == null)
        {
            return BadRequest(); // NOTE TO FUTURE ME: Should this be NotFound instead??
        }

        return Ok(joke);
    }

    [HttpPost]
    public async Task<ActionResult<Joke>> AddNewJoke(Joke joke)
    {
        await _jokesService.AddJokeAsync(joke);

        return CreatedAtAction(nameof(GetJoke), new { id = joke.Id }, joke);
    }
}
