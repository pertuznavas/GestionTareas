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
        return await taskService.CreateTaskAsync(createTaskDto);
    }

    [HttpPut("{id:int}/status")]
    public async Task<ActionResult<TaskDto>> PutAsync(int id, UpdateTaskStatusDto updateTaskStatusDto)
    {
        logger.LogInformation("Changing status to task with ID: {Id} to Status: {Status}", id, updateTaskStatusDto.Status);
        return await taskService.ChangeStatusAsync(id, updateTaskStatusDto.Status);
    }

    [HttpPut("{id:int}/user")]
    public async Task<ActionResult<TaskDto>> AssignTaskAsync(int id, AssignTaskDto assignTaskDto)
    {
        logger.LogInformation("Assigning task with ID: {Id} to User with Id: {UserId}", id, assignTaskDto.UserId);
        return await taskService.AssignTaskAsync(id,assignTaskDto.UserId);
    }
}
