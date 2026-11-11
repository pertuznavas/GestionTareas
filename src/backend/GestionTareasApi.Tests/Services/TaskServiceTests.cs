using GestionTareasApi.Configurations.Database;
using GestionTareasApi.Entities;
using GestionTareasApi.Enums;
using GestionTareasApi.Services;
using GestionTareasApi.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GestionTareasApi.Tests.Services;

public class TaskServiceTests
{
    private readonly AppDbContext _db;
    private readonly TaskService _service;

    public TaskServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        _db = new AppDbContext(options);
        _service = new TaskService(_db);
    }

    private async Task<TaskItem> AddTaskAsync(TaskItemStatus status)
    {
        var task = new TaskItem { Title = "Prueba", Status = status, UserId = 1, CreatedAt = DateTime.UtcNow };
        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();
        return task;
    }

    [Theory]
    [InlineData(TaskItemStatus.Pending, TaskItemStatus.InProgress)]
    [InlineData(TaskItemStatus.InProgress, TaskItemStatus.Done)]
    public async Task ChangeStatusAsync_AllowedTransition_UpdatesStatus(TaskItemStatus current, TaskItemStatus next)
    {
        var task = await AddTaskAsync(current);

        var result = await _service.ChangeStatusAsync(task.Id, next);

        Assert.Equal(next, result.Status);
    }

    [Theory]
    [InlineData(TaskItemStatus.Pending, TaskItemStatus.Done)]
    [InlineData(TaskItemStatus.InProgress, TaskItemStatus.Pending)]
    [InlineData(TaskItemStatus.Done, TaskItemStatus.Pending)]
    [InlineData(TaskItemStatus.Done, TaskItemStatus.InProgress)]
    public async Task ChangeStatusAsync_NotAllowedTransition_ThrowsBadRequest(TaskItemStatus current, TaskItemStatus next)
    {
        var task = await AddTaskAsync(current);

        await Assert.ThrowsAsync<BadRequestException>(() => _service.ChangeStatusAsync(task.Id, next));
    }

    [Fact]
    public async Task ChangeStatusAsync_NonExistingTask_ThrowsNotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(() => _service.ChangeStatusAsync(999, TaskItemStatus.Done));
    }
}
