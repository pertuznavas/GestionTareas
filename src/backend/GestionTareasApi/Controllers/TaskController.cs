using GestionTareasApi.Contracts;
using GestionTareasApi.Dtos.Tasks;
using GestionTareasApi.Dtos.Users;
using GestionTareasApi.Enums;
using GestionTareasApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionTareasApi.Controllers;

/// <summary>
/// Task endpoints.
/// </summary>
[Route("api/tasks")]
[ApiController]
public class TaskController(ITaskService taskService, ILogger<TaskController> logger) : ControllerBase
{
    /// <summary>
    /// Lists the tasks, optionally filtered by status.
    /// </summary>
    /// <param name="status">Status to filter by: 1 Pending, 2 InProgress, 3 Done.</param>
    /// <response code="200">The matching tasks.</response>
    [HttpGet]
    public async Task<ActionResult<List<TaskDto>>> GetAsync(TaskItemStatus? status = null)
    {
        logger.LogInformation("Getting tasks with status: {Status}", status);
        return await taskService.GetTasksAsync(status);
    }

    /// <summary>
    /// Gets a task by its identifier.
    /// </summary>
    /// <param name="id">Task identifier.</param>
    /// <response code="200">The task.</response>
    /// <response code="404">The task does not exist.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType<TaskDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDto>> GetByIdAsync(int id)
    {
        logger.LogInformation("Getting task with Id: {Id}", id);
        return await taskService.GetTaskByIdAsync(id);
    }

    /// <summary>
    /// Creates a task in Pending status.
    /// </summary>
    /// <param name="createTaskDto">Task data.</param>
    /// <response code="201">The created task.</response>
    /// <response code="400">The data is not valid.</response>
    /// <response code="404">The user does not exist.</response>
    [HttpPost]
    [ProducesResponseType<TaskDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDto>> PostAsync(CreateTaskDto createTaskDto)
    {
        var task = await taskService.CreateTaskAsync(createTaskDto);
        return Created($"/api/tasks/{task.Id}", task);
    }

    /// <summary>
    /// Changes the status of a task. Only Pending → InProgress → Done is allowed.
    /// </summary>
    /// <param name="id">Task identifier.</param>
    /// <param name="updateTaskStatusDto">New status.</param>
    /// <response code="200">The updated task.</response>
    /// <response code="400">The status transition is not allowed.</response>
    /// <response code="404">The task does not exist.</response>
    [HttpPut("{id:int}/status")]
    [ProducesResponseType<TaskDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDto>> PutAsync(int id, UpdateTaskStatusDto updateTaskStatusDto)
    {
        logger.LogInformation("Changing status to task with ID: {Id} to Status: {Status}", id, updateTaskStatusDto.Status);
        return await taskService.ChangeStatusAsync(id, updateTaskStatusDto.Status);
    }

    /// <summary>
    /// Assigns a task to another user.
    /// </summary>
    /// <param name="id">Task identifier.</param>
    /// <param name="assignTaskDto">Identifier of the new user.</param>
    /// <response code="200">The updated task.</response>
    /// <response code="404">The task or the user does not exist.</response>
    [HttpPut("{id:int}/user")]
    [ProducesResponseType<TaskDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDto>> AssignTaskAsync(int id, AssignTaskDto assignTaskDto)
    {
        logger.LogInformation("Assigning task with ID: {Id} to User with Id: {UserId}", id, assignTaskDto.UserId);
        return await taskService.AssignTaskAsync(id,assignTaskDto.UserId);
    }
}
