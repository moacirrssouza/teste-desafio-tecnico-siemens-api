using BookManagementSystem.Domain.Entities;
using BookManagementSystem.Infrastructure.Data;
using BookManagementSystem.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookManagementSystem.Infrastructure.Repositories;

/// <summary>
/// Implementação do repositório de Livros.
/// </summary>
public class BookRepository : Repository<Book>, IBookRepository
{
    public BookRepository(BookManagementContext context) : base(context)
    {
    }

    public async Task<Book?> GetByIsbnAsync(string isbn)
    {
        return await _context.Books.FirstOrDefaultAsync(b => b.Isbn == isbn && b.IsActive);
    }

    public async Task<Book?> GetByIdWithDetailsAsync(Guid id)
    {
        return await _context.Books
            .Where(b => b.Id == id && b.IsActive)
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId)
    {
        return await _context.Books
            .Where(b => b.AuthorId == authorId && b.IsActive)
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetBooksByGenreAsync(Guid genreId)
    {
        return await _context.Books
            .Where(b => b.GenreId == genreId && b.IsActive)
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetBooksWithDetailsAsync()
    {
        return await _context.Books
            .Where(b => b.IsActive)
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .ToListAsync();
    }
}
