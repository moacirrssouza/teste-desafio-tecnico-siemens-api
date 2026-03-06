using BookManagementSystem.Domain.Entities;
using BookManagementSystem.Infrastructure.Data;
using BookManagementSystem.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookManagementSystem.Infrastructure.Repositories;

/// <summary>
/// Implementação do repositório de Gêneros.
/// </summary>
public class GenreRepository : Repository<Genre>, IGenreRepository
{
    public GenreRepository(BookManagementContext context) : base(context)
    {
    }

    public async Task<Genre?> GetByNameAsync(string name)
    {
        return await _context.Genres.FirstOrDefaultAsync(g => g.Name == name && g.IsActive);
    }

    public async Task<IEnumerable<Genre>> GetGenresWithBooksAsync()
    {
        return await _context.Genres
            .Where(g => g.IsActive)
            .Include(g => g.Books)
            .ToListAsync();
    }
}
