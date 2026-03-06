using BookManagementSystem.Application.DTOs.Book;
using BookManagementSystem.Application.ViewModels;

namespace BookManagementSystem.Application.Services.Interfaces;

/// <summary>
/// Interface para o serviço de Livros.
/// </summary>
public interface IBookService
{
    Task<ApiResponse<BookDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<IEnumerable<BookDto>>> GetAllAsync();
    Task<ApiResponse<BookDto>> CreateAsync(CreateBookDto dto);
    Task<ApiResponse<BookDto>> CreateWithDetailsAsync(CreateBookDto dto);
    Task<ApiResponse<BookDto>> UpdateAsync(Guid id, UpdateBookDto dto);
    Task<ApiResponse<bool>> DeleteAsync(Guid id);
    Task<ApiResponse<IEnumerable<BookDto>>> GetBooksByAuthorAsync(Guid authorId);
    Task<ApiResponse<IEnumerable<BookDto>>> GetBooksByGenreAsync(Guid genreId);
}
