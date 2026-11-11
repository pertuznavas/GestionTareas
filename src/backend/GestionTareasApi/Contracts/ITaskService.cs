using GestionTareasApi.Dtos.Tasks;
using GestionTareasApi.Enums;

namespace GestionTareasApi.Contracts;

public interface ITaskService
{
    Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto);
    Task<List<TaskDto>> GetTasksAsync(TaskItemStatus? status);
    Task<TaskDto> GetTaskByIdAsync(int id);
    Task<TaskDto> AssignTaskAsync(int taskId, int userId);
    Task<TaskDto> ChangeStatusAsync(int taskId, TaskItemStatus status);
}
