using GestionTareasApi.Dtos.Users;
using GestionTareasApi.Shared.Exceptions;

namespace GestionTareasApi.Contracts;

/// <summary>
/// User management operations.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Creates a user.
    /// </summary>
    /// <param name="createUserDto">User data.</param>
    /// <returns>The created user.</returns>
    /// <exception cref="ConflictException">A user with the same email already exists.</exception>
    Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);

    /// <summary>
    /// Gets all users.
    /// </summary>
    /// <returns>The users.</returns>
    Task<List<UserDto>> GetUsersAsync();

    /// <summary>
    /// Gets a user by its identifier.
    /// </summary>
    /// <param name="id">User identifier.</param>
    /// <returns>The user.</returns>
    /// <exception cref="NotFoundException">The user does not exist.</exception>
    Task<UserDto> GetUserByIdAsync(int id);
}
