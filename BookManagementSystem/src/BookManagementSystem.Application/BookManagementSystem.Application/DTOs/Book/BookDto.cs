namespace BookManagementSystem.Application.DTOs.Book;

/// <summary>
/// DTO para visualização de um livro.
/// </summary>
public record BookDto
(
    Guid Id,
    string Title,
    string Isbn,
    string Description,
    DateTime PublishedDate,
    int Pages,
    decimal Price,
    Guid AuthorId,
    Guid GenreId,
    String AuthorName,
    string GenreName,
    DateTime CreatedAt,
    DateTime UpdatedAt
);