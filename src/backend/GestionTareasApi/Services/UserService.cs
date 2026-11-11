using GestionTareasApi.Configurations.Database;
using GestionTareasApi.Contracts;
using GestionTareasApi.Dtos.Users;
using GestionTareasApi.Entities;
using GestionTareasApi.Mappers;
using GestionTareasApi.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GestionTareasApi.Services;

public class UserService(AppDbContext db) : IUserService
{
    public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        if (await db.Users.AnyAsync(u => u.Email == createUserDto.Email))
            throw new AppException("There is a user with the provided email");

        var user = createUserDto.ToEntity();

        db.Users.Add(user);
        await db.SaveChangesAsync();
        return user.ToDto();
    }

    public async Task<UserDto> GetUserByIdAsync(int id) => (await GetUser(id)).ToDto();

    public async Task<List<UserDto>> GetUsersAsync()
    {
        return await db.Users.Select(u => u.ToDto()).ToListAsync();
    }

    private async Task<User> GetUser(int userId) => await db.Users.FindAsync(userId) ?? throw new NotFoundException(404, $"User with Id: {userId} does not exist");
}
