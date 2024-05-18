namespace Identity.Domain.Common.Entities;

/// <summary>
/// Base class for entities that require auditing of creation and modification times.
/// </summary>
public abstract class AuditableEntity : SoftDeletedEntity, IAuditableEntity
{
    /// <summary>
    /// Gets the name of the entity
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when the entity was created
    /// </summary>
    public DateTimeOffset  CreatedTime { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when the entity was updated
    /// Can be null if the entity has never been modified
    /// </summary>
    public DateTimeOffset? ModifiedTime { get; set; }

   
}