using BookManagementSystem.Domain.Entities;
using BookManagementSystem.Infrastructure.Data;
using BookManagementSystem.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookManagementSystem.Infrastructure.Repositories;

/// <summary>
/// Implementação do repositório de Autores.
/// </summary>
public class AuthorRepository : Repository<Author>, IAuthorRepository
{
    public AuthorRepository(BookManagementContext context) : base(context)
    {
    }

    public async Task<Author?> GetByNameAsync(string name)
    {
        return await _context.Authors.FirstOrDefaultAsync(a => a.Name == name && a.IsActive);
    }

    public async Task<Author?> GetByEmailAsync(string email)
    {
        return await _context.Authors.FirstOrDefaultAsync(a => a.Email == email && a.IsActive);
    }

    public async Task<IEnumerable<Author>> GetAuthorsWithBooksAsync()
    {
        return await _context.Authors
            .Where(a => a.IsActive)
            .Include(a => a.Books)
            .ToListAsync();
    }
}
