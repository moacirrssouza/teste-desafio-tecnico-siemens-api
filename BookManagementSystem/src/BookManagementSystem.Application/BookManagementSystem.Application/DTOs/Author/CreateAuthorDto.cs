namespace BookManagementSystem.Application.DTOs.Author;

/// <summary>
/// DTO para criação de um novo autor.
/// </summary>
public record CreateAuthorDto(
    string Name,
    string Email,
    DateTime BirthDate,
    string Biography
);
