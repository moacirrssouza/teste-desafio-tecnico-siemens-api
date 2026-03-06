using BookManagementSystem.Application.DTOs.Genre;
using BookManagementSystem.Application.Services;
using BookManagementSystem.Domain.Entities;
using BookManagementSystem.Infrastructure.Repositories.Interfaces;
using Moq;
using Xunit;

namespace BookManagementSystem.Tests.Services;

/// <summary>
/// Testes unitários para o serviço de Gêneros.
/// </summary>
public class GenreServiceTests
{
    private readonly Mock<IGenreRepository> _genreRepositoryMock;
    private readonly GenreService _genreService;

    public GenreServiceTests()
    {
        _genreRepositoryMock = new Mock<IGenreRepository>();
        _genreService = new GenreService(_genreRepositoryMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsSuccessResponse()
    {
        // Arrange
        var genreId = Guid.NewGuid();
        var genre = new Genre { Id = genreId, Name = "Fiction", Description = "Fiction books", IsActive = true };
        _genreRepositoryMock.Setup(r => r.GetByIdAsync(genreId)).ReturnsAsync(genre);

        // Act
        var result = await _genreService.GetByIdAsync(genreId);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal("Fiction", result.Data.Name);
        _genreRepositoryMock.Verify(r => r.GetByIdAsync(genreId), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsFailureResponse()
    {
        // Arrange
        var genreId = Guid.NewGuid();
        _genreRepositoryMock.Setup(r => r.GetByIdAsync(genreId)).ReturnsAsync((Genre)null!);

        // Act
        var result = await _genreService.GetByIdAsync(genreId);

        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllGenres()
    {
        // Arrange
        var genres = new List<Genre>
        {
            new Genre { Id = Guid.NewGuid(), Name = "Fiction", IsActive = true, Books = new List<Book>() },
            new Genre { Id = Guid.NewGuid(), Name = "Non-Fiction", IsActive = true, Books = new List<Book>() }
        };
        _genreRepositoryMock.Setup(r => r.GetGenresWithBooksAsync()).ReturnsAsync(genres);

        // Act
        var result = await _genreService.GetAllAsync();

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(2, result.Data.Count());
        _genreRepositoryMock.Verify(r => r.GetGenresWithBooksAsync(), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_WithValidData_ReturnsSuccessResponse()
    {
        // Arrange
        var createDto = new CreateGenreDto { Name = "Mystery", Description = "Mystery books" };
        _genreRepositoryMock.Setup(r => r.GetByNameAsync("Mystery")).ReturnsAsync((Genre)null!);
        _genreRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Genre>())).ReturnsAsync((Genre genre) => genre);
        _genreRepositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _genreService.CreateAsync(createDto);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal("Mystery", result.Data.Name);
        _genreRepositoryMock.Verify(r => r.GetByNameAsync("Mystery"), Times.Once);
        _genreRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Genre>()), Times.Once);
        _genreRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_WithDuplicateName_ReturnsFailureResponse()
    {
        // Arrange
        var createDto = new CreateGenreDto { Name = "Fiction", Description = "Fiction books" };
        var existingGenre = new Genre { Id = Guid.NewGuid(), Name = "Fiction", IsActive = true };
        _genreRepositoryMock.Setup(r => r.GetByNameAsync("Fiction")).ReturnsAsync(existingGenre);

        // Act
        var result = await _genreService.CreateAsync(createDto);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("já existe", result.Message);
    }

    [Fact]
    public async Task CreateAsync_WithEmptyName_ReturnsFailureResponse()
    {
        // Arrange
        var createDto = new CreateGenreDto { Name = "", Description = "Fiction books" };

        // Act
        var result = await _genreService.CreateAsync(createDto);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("obrigatório", result.Message);
    }

    [Fact]
    public async Task UpdateAsync_WithValidData_ReturnsSuccessResponse()
    {
        // Arrange
        var genreId = Guid.NewGuid();
        var genre = new Genre { Id = genreId, Name = "Fiction", Description = "Old description", IsActive = true };
        var updateDto = new UpdateGenreDto { Name = "Updated Fiction", Description = "New description" };
        
        _genreRepositoryMock.Setup(r => r.GetByIdAsync(genreId)).ReturnsAsync(genre);
        _genreRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Genre>())).ReturnsAsync((Genre g) => g);
        _genreRepositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _genreService.UpdateAsync(genreId, updateDto);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal("Updated Fiction", result.Data.Name);
        _genreRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Genre>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_ReturnsSuccessResponse()
    {
        // Arrange
        var genreId = Guid.NewGuid();
        _genreRepositoryMock.Setup(r => r.DeleteAsync(genreId)).ReturnsAsync(true);
        _genreRepositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _genreService.DeleteAsync(genreId);

        // Assert
        Assert.True(result.Success);
        Assert.True(result.Data);
        _genreRepositoryMock.Verify(r => r.DeleteAsync(genreId), Times.Once);
        _genreRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithInvalidId_ReturnsFailureResponse()
    {
        // Arrange
        var genreId = Guid.NewGuid();
        _genreRepositoryMock.Setup(r => r.DeleteAsync(genreId)).ReturnsAsync(false);

        // Act
        var result = await _genreService.DeleteAsync(genreId);

        // Assert
        Assert.False(result.Success);
        _genreRepositoryMock.Verify(r => r.DeleteAsync(genreId), Times.Once);
    }
}
