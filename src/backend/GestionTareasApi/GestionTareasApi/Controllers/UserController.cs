using GestionTareasApi.Contracts;
using GestionTareasApi.Dtos.Users;
using Microsoft.AspNetCore.Mvc;

namespace GestionTareasApi.Controllers;

[Route("api/users")]
[ApiController]
public class UserController(IUserService userService, ILogger<UserController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAsync()
    {
        logger.LogInformation("Getting all users");
        return await userService.GetUsersAsync();
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> PostAsync(CreateUserDto createUserDto)
    {
        logger.LogInformation("Creating User with Name: {Name} and Email: {Email}", createUserDto.Name, createUserDto.Email);
        return await userService.CreateUserAsync(createUserDto);
    }
}
