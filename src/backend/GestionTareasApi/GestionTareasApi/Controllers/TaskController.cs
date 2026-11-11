using GestionTareasApi.Contracts;
using GestionTareasApi.Dtos.Tasks;
using GestionTareasApi.Enums;
using Microsoft.AspNetCore.Mvc;

namespace GestionTareasApi.Controllers;

[Route("api/tasks")]
[ApiController]
public class TaskController(ITaskService taskService, ILogger<TaskController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TaskDto>>> GetAsync(TaskItemStatus? status = null)
    {
        logger.LogInformation("Getting tasks with status: {Status}", status);
        return await taskService.GetTasksAsync(status);
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> PostAsync(CreateTaskDto createTaskDto)
    {
        return await taskService.CreateTaskAync(createTaskDto);
    }

    [HttpPut("{id:int}/status")]
    public async Task<ActionResult<TaskDto>> PutAsync(int id, TaskItemStatus status)
    {
        logger.LogInformation("Changing status to task with ID: {Id} to Status: {Status}", id, status);
        return await taskService.ChangeStatusAsync(id, status);
    }
}
