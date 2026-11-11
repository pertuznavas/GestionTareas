using GestionTareasApi.Enums;
using GestionTareasApi.Shared.Bases;

namespace GestionTareasApi.Entities;

public class TaskItem : EntityBase
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public TaskItemStatus Status { get; set; } = TaskItemStatus.Pending;
    public string? AdditionalInfo { get; set; }

    // Relations
    public int UserId { get; set; }
    public User? User { get; set; }
}
