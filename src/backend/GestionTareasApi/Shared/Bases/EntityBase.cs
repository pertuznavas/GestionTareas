namespace GestionTareasApi.Shared.Bases;

/// <summary>
/// Base class for entities, with the identifier and audit dates they share.
/// </summary>
/// <typeparam name="TId">Type of the entity identifier.</typeparam>
public abstract class EntityBase<TId>
{
    public TId? Id { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Base class for entities with an <see cref="int"/> identifier.
/// </summary>
public abstract class EntityBase : EntityBase<int>;
