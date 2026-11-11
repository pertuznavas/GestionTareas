using GestionTareasApi.Dtos.Tasks;
using GestionTareasApi.Entities;
using GestionTareasApi.Enums;

namespace GestionTareasApi.Contracts;

public interface ITaskService
{
    Task<TaskDto> CreateTaskAync(CreateTaskDto CreateTaskDto);
    Task<List<TaskDto>> GetTasksAsync(TaskItemStatus? status);
    Task AsignTaskAsync(int taskId, int userId);
    Task<TaskDto> ChangeStatusAsync(int taskId, TaskItemStatus status);
}
