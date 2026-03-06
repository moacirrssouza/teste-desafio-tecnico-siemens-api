using BookManagementSystem.Application.DTOs.Genre;
using BookManagementSystem.Application.ViewModels;

namespace BookManagementSystem.Application.Services.Interfaces;

/// <summary>
/// Interface para o serviço de Gêneros.
/// </summary>
public interface IGenreService
{
    Task<ApiResponse<GenreDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<IEnumerable<GenreDto>>> GetAllAsync();
    Task<ApiResponse<GenreDto>> CreateAsync(CreateGenreDto dto);
    Task<ApiResponse<GenreDto>> UpdateAsync(Guid id, UpdateGenreDto dto);
    Task<ApiResponse<bool>> DeleteAsync(Guid id);
}
