using GestionTareasApi.Configurations.Database;
using GestionTareasApi.Contracts;
using GestionTareasApi.Dtos.Users;
using GestionTareasApi.Mappers;
using GestionTareasApi.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GestionTareasApi.Services;

public class UserService(AppDbContext db) : IUserService
{
    public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        if (await db.Users.AnyAsync(u => u.Email == createUserDto.Email))
            throw new AppException(409, "There is a user with the provided email");

        var user = createUserDto.ToEntity();

        db.Users.Add(user);
        await db.SaveChangesAsync();
        return user.ToDto();
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        var users = await db.Users.AsNoTracking().ToListAsync();
        return [.. users.Select(u => u.ToDto())];
    }
}
