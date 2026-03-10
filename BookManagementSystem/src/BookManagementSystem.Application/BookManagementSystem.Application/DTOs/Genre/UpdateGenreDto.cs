namespace BookManagementSystem.Application.DTOs.Genre;

/// <summary>
/// DTO para atualização de um gênero existente.
/// </summary>
public record UpdateGenreDto(
    string Name,
    string Description
);
