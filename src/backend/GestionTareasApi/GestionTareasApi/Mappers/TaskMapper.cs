using GestionTareasApi.Dtos.Tasks;
using GestionTareasApi.Entities;

namespace GestionTareasApi.Mappers;

public static class TaskMapper
{
    public static TaskDto ToDto(this TaskItem entity) => new(entity.Id, entity.Title, entity.Description, entity.Status, entity.UserId,entity.CreatedAt);

    public static TaskItem ToEntity(this CreateTaskDto dto) => new()
    {
        Title = dto.Title,
        Description = dto.Description,
        UserId = dto.UserId,
        CreatedAt = DateTime.UtcNow
    };
}
