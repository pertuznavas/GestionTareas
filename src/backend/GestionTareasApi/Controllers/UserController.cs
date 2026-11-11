using GestionTareasApi.Contracts;
using GestionTareasApi.Dtos.Users;
using Microsoft.AspNetCore.Mvc;

namespace GestionTareasApi.Controllers;

/// <summary>
/// User endpoints.
/// </summary>
[Route("api/users")]
[ApiController]
public class UserController(IUserService userService, ILogger<UserController> logger) : ControllerBase
{
    /// <summary>
    /// Lists all users.
    /// </summary>
    /// <response code="200">The users.</response>
    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAsync()
    {
        logger.LogInformation("Getting all users");
        return await userService.GetUsersAsync();
    }

    /// <summary>
    /// Gets a user by its identifier.
    /// </summary>
    /// <param name="id">User identifier.</param>
    /// <response code="200">The user.</response>
    /// <response code="404">The user does not exist.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> GetByIdAsync(int id)
    {
        logger.LogInformation("Getting user with Id: {Id}", id);
        return await userService.GetUserByIdAsync(id);
    }

    /// <summary>
    /// Creates a user.
    /// </summary>
    /// <param name="createUserDto">User data.</param>
    /// <response code="201">The created user.</response>
    /// <response code="400">The data is not valid.</response>
    /// <response code="409">A user with the same email already exists.</response>
    [HttpPost]
    [ProducesResponseType<UserDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserDto>> PostAsync(CreateUserDto createUserDto)
    {
        logger.LogInformation("Creating User with Name: {Name} and Email: {Email}", createUserDto.Name, createUserDto.Email);
        var user = await userService.CreateUserAsync(createUserDto);
        return Created($"/api/users/{user.Id}", user);
    }
}
