namespace BookManagementSystem.Application.DTOs.Book;

/// <summary>
/// DTO para visualização de um livro.
/// </summary>
public class BookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }
    public int Pages { get; set; }
    public decimal Price { get; set; }
    public Guid AuthorId { get; set; }
    public Guid GenreId { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string GenreName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
