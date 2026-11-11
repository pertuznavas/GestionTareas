using System.ComponentModel.DataAnnotations;

namespace GestionTareasApi.Dtos.Tasks;

public record AssignTaskDto
{
    [Range(1, int.MaxValue)]
    public int UserId { get; init; }
}
