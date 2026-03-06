namespace BookManagementSystem.Application.DTOs.Genre;

/// <summary>
/// DTO para visualização de um gênero.
/// </summary>
public class GenreDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int BookCount { get; set; }
}
