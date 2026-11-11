using GestionTareasApi.Dtos.Users;

namespace GestionTareasApi.Contracts;

public interface IUserService
{
    Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
    Task<List<UserDto>> GetUsersAsync();
}
