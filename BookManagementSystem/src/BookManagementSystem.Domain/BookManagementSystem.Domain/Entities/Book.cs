namespace BookManagementSystem.Domain.Entities;

/// <summary>
/// Entidade que representa um livro.
/// </summary>
public class Book : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }
    public int Pages { get; set; }
    public decimal Price { get; set; }

    // Foreign Keys
    public Guid AuthorId { get; set; }
    public Guid GenreId { get; set; }

    // Relationships
    public Author? Author { get; set; }
    public Genre? Genre { get; set; }
}
