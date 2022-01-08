namespace Ordering.Domain.Common;

public abstract class EntityBase
{
    public int Id { get; set; }

    /// <summary>
    /// The UserName whom created the entity.
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// The Time this entity was created in UTC
    /// </summary>
    public DateTime CreatedUtcDate { get; set; }

    /// <summary>
    /// The UserName whom created the entity.
    /// </summary>
    public string? LastModifiedBy { get; set; }

    /// <summary>
    /// The Time this entity was modified in UTC
    /// </summary>
    public DateTime? LastModifiedUtcDate { get; set; }
}