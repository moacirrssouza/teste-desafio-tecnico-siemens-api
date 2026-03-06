using BookManagementSystem.Domain.Entities;

namespace BookManagementSystem.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Interface específica para o repositório de Livros.
/// </summary>
public interface IBookRepository : IRepository<Book>
{
    Task<Book?> GetByIsbnAsync(string isbn);
    Task<Book?> GetByIdWithDetailsAsync(Guid id);
    Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId);
    Task<IEnumerable<Book>> GetBooksByGenreAsync(Guid genreId);
    Task<IEnumerable<Book>> GetBooksWithDetailsAsync();
}
