using GestionTareasApi.Shared.Bases;

namespace GestionTareasApi.Entities;

public class User : EntityBase
{
    public required string Name { get; set; }
    public required string Email { get; set; }

    // Relations
    public ICollection<Task> Tasks { get; set; } = [];
}
