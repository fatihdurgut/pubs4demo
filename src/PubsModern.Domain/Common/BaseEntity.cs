namespace PubsModern.Domain.Common;

/// <summary>
/// Base entity class with common properties for all domain entities
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }
    public string? CreatedBy { get; protected set; }
    public string? UpdatedBy { get; protected set; }
    public bool IsDeleted { get; protected set; }
    public DateTime? DeletedAt { get; protected set; }
    
    /// <summary>
    /// Concurrency token for optimistic locking
    /// </summary>
    public byte[] RowVersion { get; protected set; } = Array.Empty<byte>();

    public void MarkAsDeleted(string deletedBy)
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        UpdatedBy = deletedBy;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateAuditInfo(string updatedBy)
    {
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.UtcNow;
    }
}
