namespace BookManagementSystem.Application.DTOs.Book;

public record CreateBookDto(
    string Isbn,
    string Title, 
    string AuthorName, 
    string GenreName, 
    int Pages, 
    string Description, 
    DateTime PublishedDate
    );