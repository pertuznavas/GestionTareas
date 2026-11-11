namespace GestionTareasApi.Shared.Bases;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TId"></typeparam>
public abstract class EntityBase<TId>
{
    public TId? Id { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// 
/// </summary>
public abstract class EntityBase : EntityBase<int>;
