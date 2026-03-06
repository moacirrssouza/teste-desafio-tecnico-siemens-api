namespace BookManagementSystem.Application.DTOs.Book;

/// <summary>
/// DTO para atualização de um livro existente.
/// </summary>
public record UpdateBookDto(
    string Isbn, 
    string Title, 
    string Description, 
    DateTime PublishedDate, 
    int Pages, 
    string AuthorName,
    string GenreName
    );