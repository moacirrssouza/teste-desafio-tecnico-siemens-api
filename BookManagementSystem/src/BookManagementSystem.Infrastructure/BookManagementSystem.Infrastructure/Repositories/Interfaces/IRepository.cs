using BookManagementSystem.Domain.Entities;

namespace BookManagementSystem.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Interface base para repositórios genéricos.
/// </summary>
public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetActiveAsync();
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid id);
    Task<int> SaveChangesAsync();
}
