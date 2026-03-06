namespace BookManagementSystem.Domain.Entities;

/// <summary>
/// Entidade que representa um autor.
/// </summary>
public class Author : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Biography { get; set; } = string.Empty;

    // Relationship
    public ICollection<Book> Books { get; set; } = new List<Book>();
}
