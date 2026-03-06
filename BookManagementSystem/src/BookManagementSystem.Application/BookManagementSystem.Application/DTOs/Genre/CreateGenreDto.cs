namespace BookManagementSystem.Application.DTOs.Genre;

/// <summary>
/// DTO para criação de um novo gênero.
/// </summary>
public class CreateGenreDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
