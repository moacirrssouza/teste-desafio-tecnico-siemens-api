namespace BookManagementSystem.Domain.Entities;

/// <summary>
/// Entidade base para todas as entidades do domínio.
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}
