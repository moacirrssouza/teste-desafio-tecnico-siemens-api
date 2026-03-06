namespace BookManagementSystem.Application.DTOs.Author;

/// <summary>
/// DTO para atualização de um autor existente.
/// </summary>
public class UpdateAuthorDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Biography { get; set; } = string.Empty;
}
