namespace BookManagementSystem.Application.DTOs.Author;

/// <summary>
/// DTO para atualização de um autor existente.
/// </summary>
public record UpdateAuthorDto(
    string Name,
    string Email,
    DateTime BirthDate,
    string Biography
);
