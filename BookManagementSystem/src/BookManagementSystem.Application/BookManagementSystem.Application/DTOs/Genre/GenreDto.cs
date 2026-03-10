namespace BookManagementSystem.Application.DTOs.Genre;

/// <summary>
/// DTO para visualização de um gênero.
/// </summary>
public record GenreDto(
    Guid Id,
    string Name,
    string Description,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    int BookCount
);
