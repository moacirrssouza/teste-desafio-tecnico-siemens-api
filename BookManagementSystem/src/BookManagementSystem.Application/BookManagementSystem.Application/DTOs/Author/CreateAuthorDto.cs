namespace BookManagementSystem.Application.DTOs.Author;

/// <summary>
/// DTO para criação de um novo autor.
/// </summary>
public class CreateAuthorDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Biography { get; set; } = string.Empty;
}
