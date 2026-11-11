using GestionTareasApi.Configurations.Database;
using GestionTareasApi.Contracts;
using GestionTareasApi.Dtos.Tasks;
using GestionTareasApi.Entities;
using GestionTareasApi.Enums;
using GestionTareasApi.Mappers;
using GestionTareasApi.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GestionTareasApi.Services;

public class TaskService(AppDbContext db) : ITaskService
{
    public async Task<List<TaskDto>> GetTasksAsync(TaskItemStatus? status)
    {
        var query = db.Tasks.AsNoTracking();

        if (status is not null)
            query = query.Where(t => t.Status == status);

        return await query.OrderByDescending(t => t.CreatedAt).Select(t => t.ToDto()).ToListAsync();
    }

    public async Task<TaskDto> GetTaskByIdAsync(int id) => (await GetTask(id)).ToDto();

    public async Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto)
    {
        if (!await db.Users.AnyAsync(u => u.Id == createTaskDto.UserId))
            throw new NotFoundException(404, $"User with Id: {createTaskDto.UserId} does not exist");

        var task = createTaskDto.ToEntity();

        db.Tasks.Add(task);
        await db.SaveChangesAsync();

        return task.ToDto();
    }

    public async Task<TaskDto> AssignTaskAsync(int taskId, int userId)
    {
        var task = await GetTask(taskId);

        if (!await db.Users.AnyAsync(u => u.Id == userId))
            throw new NotFoundException(404, $"User with Id: {userId} does not exist");

        task.UserId = userId;
        await db.SaveChangesAsync();

        return task.ToDto();


    }

    public async Task<TaskDto> ChangeStatusAsync(int taskId, TaskItemStatus status)
    {
        var task = await GetTask(taskId);
        ValidateStatusTransition(task.Status, status);

        task.Status = status;
        await db.SaveChangesAsync();

        return task.ToDto();
    }

    private async Task<TaskItem> GetTask(int taskId) => await db.Tasks.FindAsync(taskId) ?? throw new NotFoundException(404, $"Task with Id: {taskId} does not exist");

    private static void ValidateStatusTransition(TaskItemStatus current, TaskItemStatus next)
    {
        var allowed = (current, next) switch
        {
            (TaskItemStatus.Pending, TaskItemStatus.InProgress) => true,
            (TaskItemStatus.InProgress, TaskItemStatus.Done) => true,
            _ => false
        };

        if (!allowed)
            throw new BadRequestException(400, $"Status transition not allowed: from {current} to {next}");
    }
}
