using BookManagementSystem.Domain.Entities;
using BookManagementSystem.Infrastructure.Data;
using BookManagementSystem.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookManagementSystem.Infrastructure.Repositories;

/// <summary>
/// Implementação base de um repositório genérico.
/// </summary>
public abstract class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly BookManagementContext _context;

    protected Repository(BookManagementContext context)
    {
        _context = context;
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> GetActiveAsync()
    {
        return await _context.Set<T>().Where(e => e.IsActive).ToListAsync();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.IsActive = true;

        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Set<T>().Update(entity);
        return await Task.FromResult(entity);
    }

    public virtual async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            return false;

        entity.IsActive = false;
        _context.Set<T>().Update(entity);
        return true;
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
