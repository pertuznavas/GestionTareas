using System.ComponentModel.DataAnnotations;

namespace GestionTareasApi.Dtos.Tasks
{
    public record CreateTaskDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public required string Title { get; set; }
        public string? Description { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
