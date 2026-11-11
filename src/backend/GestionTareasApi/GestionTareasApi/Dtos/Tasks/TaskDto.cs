using GestionTareasApi.Enums;

namespace GestionTareasApi.Dtos.Tasks;

public record TaskDto(int Id, string Title, string? Description, TaskItemStatus Status, int UserId, DateTime CreatedAt, TaskAdditionalInfoDto? AdditionalInfo);
