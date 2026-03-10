namespace BookManagementSystem.Application.DTOs.Author;

/// <summary>
/// DTO para visualização de um autor.
/// </summary>
public record AuthorDto(
    Guid Id,
    string Name,
    string Email,
    DateTime BirthDate,
    string Biography,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    int BookCount
);
