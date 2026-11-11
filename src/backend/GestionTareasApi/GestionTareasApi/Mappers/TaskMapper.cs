using GestionTareasApi.Dtos.Tasks;
using GestionTareasApi.Entities;
using System.Text.Json;

namespace GestionTareasApi.Mappers;

public static class TaskMapper
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    public static TaskDto ToDto(this TaskItem entity) => new(entity.Id, entity.Title, entity.Description, entity.Status, entity.UserId, entity.CreatedAt,
        entity.AdditionalInfo is null ? null : JsonSerializer.Deserialize<TaskAdditionalInfoDto>(entity.AdditionalInfo, JsonOptions));

    public static TaskItem ToEntity(this CreateTaskDto dto) => new()
    {
        Title = dto.Title,
        Description = dto.Description,
        UserId = dto.UserId,
        CreatedAt = DateTime.UtcNow,
        AdditionalInfo = dto.AdditionalInfo is null ? null : JsonSerializer.Serialize(dto.AdditionalInfo, JsonOptions)
    };
}
