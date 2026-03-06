namespace BookManagementSystem.Application.DTOs.Genre;

/// <summary>
/// DTO para atualização de um gênero existente.
/// </summary>
public class UpdateGenreDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
