using BookManagementSystem.Domain.Entities;

namespace BookManagementSystem.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Interface específica para o repositório de Gêneros.
/// </summary>
public interface IGenreRepository : IRepository<Genre>
{
    Task<Genre?> GetByNameAsync(string name);
    Task<IEnumerable<Genre>> GetGenresWithBooksAsync();
}
