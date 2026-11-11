using System.ComponentModel.DataAnnotations;

namespace GestionTareasApi.Dtos.Tasks;

public record CreateTaskDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string Title { get; init; }
    
    [StringLength(150)]
    public string? Description { get; init; }

    [Range(1, int.MaxValue)]
    public int UserId { get; init; }
    public TaskAdditionalInfoDto? AdditionalInfo { get; init; }
}
