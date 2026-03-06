namespace BookManagementSystem.Domain.Entities;

/// <summary>
/// Entidade que representa um gênero de livro.
/// </summary>
public class Genre : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // Relationship
    public ICollection<Book> Books { get; set; } = new List<Book>();
}
