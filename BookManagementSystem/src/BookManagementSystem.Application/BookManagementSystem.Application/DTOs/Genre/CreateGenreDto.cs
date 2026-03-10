namespace BookManagementSystem.Application.DTOs.Genre;

/// <summary>
/// DTO para criação de um novo gênero.
/// </summary>
public record CreateGenreDto(
    string Name,
    string Description
);
