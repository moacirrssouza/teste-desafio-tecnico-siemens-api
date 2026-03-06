using BookManagementSystem.Application.DTOs.Book;
using BookManagementSystem.Application.Services;
using BookManagementSystem.Domain.Entities;
using BookManagementSystem.Infrastructure.Repositories.Interfaces;
using Moq;
using Xunit;

namespace BookManagementSystem.Tests.Services;

/// <summary>
/// Testes unitários para o serviço de Livros.
/// </summary>
public class BookServiceTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly Mock<IGenreRepository> _genreRepositoryMock;
    private readonly BookService _bookService;

    public BookServiceTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _genreRepositoryMock = new Mock<IGenreRepository>();
        _bookService = new BookService(
            _bookRepositoryMock.Object,
            _authorRepositoryMock.Object,
            _genreRepositoryMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsSuccessResponse()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var author = new Author { Id = Guid.NewGuid(), Name = "Author", IsActive = true };
        var genre = new Genre { Id = Guid.NewGuid(), Name = "Fiction", IsActive = true };
        var book = new Book
        {
            Id = bookId,
            Title = "Test Book",
            Isbn = "1234567890123",
            Author = author,
            Genre = genre,
            IsActive = true
        };
        _bookRepositoryMock.Setup(r => r.GetByIdWithDetailsAsync(bookId)).ReturnsAsync(book);

        // Act
        var result = await _bookService.GetByIdAsync(bookId);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal("Test Book", result.Data.Title);
    }

    [Fact]
    public async Task CreateAsync_WithValidData_ReturnsSuccessResponse()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var genreId = Guid.NewGuid();
        var author = new Author { Id = authorId, Name = "Author", IsActive = true, Books = new List<Book>() };
        var genre = new Genre { Id = genreId, Name = "Fiction", IsActive = true, Books = new List<Book>() };
        var createDto = new CreateBookDto
        {
            Title = "New Book",
            Isbn = "1234567890123",
            Description = "Description",
            PublishedDate = DateTime.Now.AddYears(-1),
            Pages = 300,
            //Price = 29.99m,
            //AuthorId = authorId,
            //GenreId = genreId
        };

        _bookRepositoryMock.Setup(r => r.GetByIsbnAsync("1234567890123")).ReturnsAsync((Book)null!);
        _authorRepositoryMock.Setup(r => r.GetByIdAsync(authorId)).ReturnsAsync(author);
        _genreRepositoryMock.Setup(r => r.GetByIdAsync(genreId)).ReturnsAsync(genre);
        _bookRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Book>())).ReturnsAsync((Book book) => book);
        _bookRepositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _bookService.CreateAsync(createDto);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal("New Book", result.Data.Title);
    }

    [Fact]
    public async Task CreateAsync_WithInvalidIsbn_ReturnsFailureResponse()
    {
        // Arrange
        var createDto = new CreateBookDto
        {
            Title = "New Book",
            Isbn = "123",
            Description = "Description",
            PublishedDate = DateTime.Now,
            Pages = 300,
            Price = 29.99m,
            AuthorId = Guid.NewGuid(),
            GenreId = Guid.NewGuid()
        };

        // Act
        var result = await _bookService.CreateAsync(createDto);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("ISBN", result.Message);
    }

    [Fact]
    public async Task CreateAsync_WithDuplicateIsbn_ReturnsFailureResponse()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var genreId = Guid.NewGuid();
        var isbn = "1234567890123";
        var createDto = new CreateBookDto
        {
            Title = "New Book",
            Isbn = isbn,
            Description = "Description",
            PublishedDate = DateTime.Now,
            Pages = 300,
            
        };
        var existingBook = new Book { Id = Guid.NewGuid(), Isbn = isbn, IsActive = true };
        _bookRepositoryMock.Setup(r => r.GetByIsbnAsync(isbn)).ReturnsAsync(existingBook);

        // Act
        var result = await _bookService.CreateAsync(createDto);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("ISBN", result.Message);
    }

    [Fact]
    public async Task CreateAsync_WithFuturePublishDate_ReturnsFailureResponse()
    {
        // Arrange
        var createDto = new CreateBookDto
        {
            Title = "Future Book",
            Isbn = "1234567890123",
            Description = "Description",
            PublishedDate = DateTime.Now.AddDays(1),
            Pages = 300,
            Price = 29.99m,
            AuthorId = Guid.NewGuid(),
            GenreId = Guid.NewGuid()
        };

        // Act
        var result = await _bookService.CreateAsync(createDto);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("publicação", result.Message);
    }

    [Fact]
    public async Task CreateAsync_WithInvalidPages_ReturnsFailureResponse()
    {
        // Arrange
        var createDto = new CreateBookDto
        {
            Title = "Book",
            Isbn = "1234567890123",
            Description = "Description",
            PublishedDate = DateTime.Now,
            Pages = 0,
            Price = 29.99m,
            AuthorId = Guid.NewGuid(),
            GenreId = Guid.NewGuid()
        };

        // Act
        var result = await _bookService.CreateAsync(createDto);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("páginas", result.Message);
    }

    [Fact]
    public async Task CreateAsync_WithNegativePrice_ReturnsFailureResponse()
    {
        // Arrange
        var createDto = new CreateBookDto
        {
            Title = "Book",
            Isbn = "1234567890123",
            Description = "Description",
            PublishedDate = DateTime.Now,
            Pages = 300,
            Price = -10m,
            AuthorId = Guid.NewGuid(),
            GenreId = Guid.NewGuid()
        };

        // Act
        var result = await _bookService.CreateAsync(createDto);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("Preço", result.Message);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_ReturnsSuccessResponse()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        _bookRepositoryMock.Setup(r => r.DeleteAsync(bookId)).ReturnsAsync(true);
        _bookRepositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _bookService.DeleteAsync(bookId);

        // Assert
        Assert.True(result.Success);
        Assert.True(result.Data);
    }
}
