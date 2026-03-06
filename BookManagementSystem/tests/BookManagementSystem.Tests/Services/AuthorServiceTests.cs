using BookManagementSystem.Application.DTOs.Author;
using BookManagementSystem.Application.Services;
using BookManagementSystem.Domain.Entities;
using BookManagementSystem.Infrastructure.Repositories.Interfaces;
using Moq;
using Xunit;

namespace BookManagementSystem.Tests.Services;

/// <summary>
/// Testes unitários para o serviço de Autores.
/// </summary>
public class AuthorServiceTests
{
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly AuthorService _authorService;

    public AuthorServiceTests()
    {
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _authorService = new AuthorService(_authorRepositoryMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsSuccessResponse()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var author = new Author
        {
            Id = authorId,
            Name = "John Doe",
            Email = "john@example.com",
            BirthDate = new DateTime(1980, 1, 1),
            Biography = "Author biography",
            IsActive = true
        };
        _authorRepositoryMock.Setup(r => r.GetByIdAsync(authorId)).ReturnsAsync(author);

        // Act
        var result = await _authorService.GetByIdAsync(authorId);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal("John Doe", result.Data.Name);
    }

    [Fact]
    public async Task CreateAsync_WithValidData_ReturnsSuccessResponse()
    {
        // Arrange
        var createDto = new CreateAuthorDto
        {
            Name = "Jane Doe",
            Email = "jane@example.com",
            BirthDate = new DateTime(1990, 5, 15),
            Biography = "Jane's biography"
        };
        _authorRepositoryMock.Setup(r => r.GetByEmailAsync("jane@example.com")).ReturnsAsync((Author)null!);
        _authorRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Author>())).ReturnsAsync((Author author) => author);
        _authorRepositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _authorService.CreateAsync(createDto);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal("Jane Doe", result.Data.Name);
    }

    [Fact]
    public async Task CreateAsync_WithDuplicateEmail_ReturnsFailureResponse()
    {
        // Arrange
        var createDto = new CreateAuthorDto
        {
            Name = "Jane Doe",
            Email = "existing@example.com",
            BirthDate = new DateTime(1990, 5, 15),
            Biography = "Jane's biography"
        };
        var existingAuthor = new Author
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            Email = "existing@example.com",
            IsActive = true
        };
        _authorRepositoryMock.Setup(r => r.GetByEmailAsync("existing@example.com")).ReturnsAsync(existingAuthor);

        // Act
        var result = await _authorService.CreateAsync(createDto);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("email", result.Message);
    }

    [Fact]
    public async Task CreateAsync_WithFutureBirthDate_ReturnsFailureResponse()
    {
        // Arrange
        var createDto = new CreateAuthorDto
        {
            Name = "Future Author",
            Email = "future@example.com",
            BirthDate = DateTime.Now.AddDays(1),
            Biography = "Future author"
        };

        // Act
        var result = await _authorService.CreateAsync(createDto);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("nascimento inválida", result.Message);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_ReturnsSuccessResponse()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        _authorRepositoryMock.Setup(r => r.DeleteAsync(authorId)).ReturnsAsync(true);
        _authorRepositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _authorService.DeleteAsync(authorId);

        // Assert
        Assert.True(result.Success);
        Assert.True(result.Data);
    }
}
