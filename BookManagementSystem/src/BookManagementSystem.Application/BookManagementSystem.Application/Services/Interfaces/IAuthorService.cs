using BookManagementSystem.Application.DTOs.Author;
using BookManagementSystem.Application.ViewModels;

namespace BookManagementSystem.Application.Services.Interfaces;

/// <summary>
/// Interface para o serviço de Autores.
/// </summary>
public interface IAuthorService
{
    Task<ApiResponse<AuthorDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<IEnumerable<AuthorDto>>> GetAllAsync();
    Task<ApiResponse<AuthorDto>> CreateAsync(CreateAuthorDto dto);
    Task<ApiResponse<AuthorDto>> UpdateAsync(Guid id, UpdateAuthorDto dto);
    Task<ApiResponse<bool>> DeleteAsync(Guid id);
}
