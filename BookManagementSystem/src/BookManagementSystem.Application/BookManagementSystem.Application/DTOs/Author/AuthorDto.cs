namespace BookManagementSystem.Application.DTOs.Author;

/// <summary>
/// DTO para visualização de um autor.
/// </summary>
public class AuthorDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Biography { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int BookCount { get; set; }
}
