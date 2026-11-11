using System.ComponentModel.DataAnnotations;

namespace GestionTareasApi.Dtos.Tasks;

public record TaskAdditionalInfoDto
{
    [AllowedValues("Low", "Medium", "High", null)]
    public string? Priority { get; init; }

    public DateOnly? EstimatedEndDate { get; init; }

    public List<string>? Tags { get; init; }
}
