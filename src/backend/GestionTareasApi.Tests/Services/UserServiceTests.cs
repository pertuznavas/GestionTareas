using GestionTareasApi.Configurations.Database;
using GestionTareasApi.Dtos.Users;
using GestionTareasApi.Entities;
using GestionTareasApi.Services;
using GestionTareasApi.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GestionTareasApi.Tests.Services;

public class UserServiceTests
{
    private readonly AppDbContext _db;
    private readonly UserService _service;

    public UserServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        _db = new AppDbContext(options);
        _service = new UserService(_db);
    }

    private async Task<User> AddUserAsync(string name, string email)
    {
        var user = new User { Name = name, Email = email, CreatedAt = DateTime.UtcNow };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    [Fact]
    public async Task CreateUserAsync_RepeatedEmail_ThrowsAppException()
    {
        await AddUserAsync("User 1", "correo@correo.com");

        var dto = new CreateUserDto { Name = "User 2", Email = "correo@correo.com" };

        await Assert.ThrowsAsync<AppException>(() => _service.CreateUserAsync(dto));
    }

    [Fact]
    public async Task GetUserByIdAsync_NonExistingUser_ThrowsNotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetUserByIdAsync(999));
    }

    [Fact]
    public async Task CreateUserAsync_ValidUser_CreatesUser()
    {
        var dto = new CreateUserDto { Name = "Usuario Prueba", Email = "usuario@prueba.com" };
        var createdUser = await _service.CreateUserAsync(dto);

        Assert.Equal(dto.Email, createdUser.Email);
        Assert.Equal(1, await _db.Users.CountAsync());
    }
}
