using GestionTareasApi.Dtos.Users;
using GestionTareasApi.Entities;

namespace GestionTareasApi.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(this User entity) => new(entity.Id, entity.Name, entity.Email, entity.CreatedAt);

    public static User ToEntity(this CreateUserDto dto) => new()
    {
        Name = dto.Name,
        Email = dto.Email,
        CreatedAt = DateTime.UtcNow
    };
}
