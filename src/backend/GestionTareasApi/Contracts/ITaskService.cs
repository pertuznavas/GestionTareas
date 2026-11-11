using GestionTareasApi.Dtos.Tasks;
using GestionTareasApi.Enums;
using GestionTareasApi.Shared.Exceptions;

namespace GestionTareasApi.Contracts;

/// <summary>
/// Task management operations.
/// </summary>
public interface ITaskService
{
    /// <summary>
    /// Creates a task in <see cref="TaskItemStatus.Pending"/> status.
    /// </summary>
    /// <param name="createTaskDto">Task data.</param>
    /// <returns>The created task.</returns>
    /// <exception cref="NotFoundException">The user does not exist.</exception>
    Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto);

    /// <summary>
    /// Gets the tasks, optionally filtered by status.
    /// </summary>
    /// <param name="status">Status to filter by; <c>null</c> returns all tasks.</param>
    /// <returns>The matching tasks.</returns>
    Task<List<TaskDto>> GetTasksAsync(TaskItemStatus? status);

    /// <summary>
    /// Gets a task by its identifier.
    /// </summary>
    /// <param name="id">Task identifier.</param>
    /// <returns>The task.</returns>
    /// <exception cref="NotFoundException">The task does not exist.</exception>
    Task<TaskDto> GetTaskByIdAsync(int id);

    /// <summary>
    /// Assigns a task to another user.
    /// </summary>
    /// <param name="taskId">Task identifier.</param>
    /// <param name="userId">Identifier of the new user.</param>
    /// <returns>The updated task.</returns>
    /// <exception cref="NotFoundException">The task or the user does not exist.</exception>
    Task<TaskDto> AssignTaskAsync(int taskId, int userId);

    /// <summary>
    /// Changes the status of a task. Only Pending → InProgress → Done is allowed.
    /// </summary>
    /// <param name="taskId">Task identifier.</param>
    /// <param name="status">New status.</param>
    /// <returns>The updated task.</returns>
    /// <exception cref="NotFoundException">The task does not exist.</exception>
    /// <exception cref="BadRequestException">The status transition is not allowed.</exception>
    Task<TaskDto> ChangeStatusAsync(int taskId, TaskItemStatus status);
}
