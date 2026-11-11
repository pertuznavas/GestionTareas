namespace GestionTareasApi.Enums;

[Flags]
public enum TaskItemStatus
{
    Pending = 1,
    InProgress = 2,
    Done = 3,

    All = Pending | InProgress | Done
}
