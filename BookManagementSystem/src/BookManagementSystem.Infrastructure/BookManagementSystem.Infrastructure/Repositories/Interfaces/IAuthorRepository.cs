using BookManagementSystem.Domain.Entities;

namespace BookManagementSystem.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Interface específica para o repositório de Autores.
/// </summary>
public interface IAuthorRepository : IRepository<Author>
{
    Task<Author?> GetByNameAsync(string name);
    Task<Author?> GetByEmailAsync(string email);
    Task<IEnumerable<Author>> GetAuthorsWithBooksAsync();
}
