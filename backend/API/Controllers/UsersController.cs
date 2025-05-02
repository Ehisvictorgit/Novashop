using API.DTOs;
using API.Responses;
using API.Services;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Serilog;

namespace API.Controllers;

public class UsersController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly UserDTOService _userDTOService;
    private readonly IPasswordHasher _passwordHasher;

    public UsersController(IUserRepository userRepository, IMapper mapper, UserDTOService userDTOService, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userDTOService = userDTOService;
        _passwordHasher = passwordHasher;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
    {
        var users = await _userDTOService.GetAllAsync();

        if (users is null || !users.Any())
            return NotFound();

        var response = users.Select(user => new
        {
            id = user.Id.ToString(), 
            user.User
        }).ToList();

        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDTO>> Get([FromBody] ObjectId id)
    {
        var user = await _userDTOService.GetByIdAsync(id);

        if (user is null)
            return NotFound();

        var response = new
        {
            id = user.Id.ToString(),
            user.User
        };

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<User> Post([FromBody] User user)
    {
        if (user is null)
            return BadRequest();

        user.Psw = _passwordHasher.HashPassword(user.Psw);

        _userRepository.Add(user);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<User>> Post([FromBody] LoginRequest request)
    {
        var usr = request.Usr.ToUpper();
        var psw = request.Psw;

        try
        {
            var user = await _userRepository.GetByUserAsync(request.Usr);

            if (user is null || !_passwordHasher.VerifyPassword(psw, user.Psw))
            {
                Log.Logger.Information($"Login attempt failed for user: {usr}");
                return Unauthorized(new { Code = 401, Message = "Invalid username or password" });
            }

            Log.Logger.Information($"User '{usr}' authenticated successfully.");

            return Ok(ApiResponseFactory.Success<object>(null, "User authenticated successfully"));
        }
        catch (Exception ex)
        {
            Log.Logger.Error("Authentication error", ex);
            return StatusCode(500, ApiResponseFactory.Fail<object>($"There was an issue authenticating the user. Details ${ex.Message}"));
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<User> Put(string id, [FromBody] User user)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return BadRequest("Invalid ObjectId format.");

        user.Id = objectId;

        if (user is null)
            return NotFound();

        user.Psw = _passwordHasher.HashPassword(user.Psw);

        _userRepository.Update(user);
        return Ok(user);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(ObjectId id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
            return NotFound();

        await _userRepository.Remove(id);
        return NoContent();
    }
}
