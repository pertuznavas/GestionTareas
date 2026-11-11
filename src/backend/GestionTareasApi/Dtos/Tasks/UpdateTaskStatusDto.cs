using GestionTareasApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace GestionTareasApi.Dtos.Tasks;

public record UpdateTaskStatusDto
{
    [EnumDataType(typeof(TaskItemStatus))]
    public TaskItemStatus Status { get; init; }
}
