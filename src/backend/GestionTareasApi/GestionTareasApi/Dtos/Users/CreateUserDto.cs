using System.ComponentModel.DataAnnotations;

namespace GestionTareasApi.Dtos.Users;

public record CreateUserDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(50, MinimumLength = 5)]
    public required string Email { get; set; }
}
