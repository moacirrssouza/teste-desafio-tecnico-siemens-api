using BookManagementSystem.Application.DTOs.Book;
using BookManagementSystem.Application.Services.Interfaces;
using BookManagementSystem.Application.ViewModels;
using BookManagementSystem.Domain.Entities;
using BookManagementSystem.Infrastructure.Repositories.Interfaces;

namespace BookManagementSystem.Application.Services;

/// <summary>
/// Serviço de negócio para Livros.
/// </summary>
public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IGenreRepository _genreRepository;

    public BookService(
        IBookRepository bookRepository,
        IAuthorRepository authorRepository,
        IGenreRepository genreRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _genreRepository = genreRepository;
    }

    public async Task<ApiResponse<BookDto>> GetByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdWithDetailsAsync(id);
        if (book == null)
            return new ApiResponse<BookDto> { Success = false, Message = "Livro não encontrado." };

        return new ApiResponse<BookDto>
        {
            Success = true,
            Message = "Livro recuperado com sucesso.",
            Data = MapToDto(book)
        };
    }

    public async Task<ApiResponse<IEnumerable<BookDto>>> GetAllAsync()
    {
        var books = await _bookRepository.GetBooksWithDetailsAsync();
        return new ApiResponse<IEnumerable<BookDto>>
        {
            Success = true,
            Message = "Livros recuperados com sucesso.",
            Data = books.Select(MapToDto)
        };
    }

    public async Task<ApiResponse<BookDto>> CreateAsync(CreateBookDto dto)
    {
        // Validação de negócio
        if (string.IsNullOrWhiteSpace(dto.Title))
            return new ApiResponse<BookDto> { Success = false, Message = "Título é obrigatório." };

        if (string.IsNullOrWhiteSpace(dto.Isbn))
            return new ApiResponse<BookDto> { Success = false, Message = "ISBN é obrigatório." };

        if (dto.Isbn.Length != 13)
            return new ApiResponse<BookDto> { Success = false, Message = "ISBN deve ter exatamente 13 caracteres." };

        var existingBook = await _bookRepository.GetByIsbnAsync(dto.Isbn);
        if (existingBook != null)
            return new ApiResponse<BookDto> { Success = false, Message = "Um livro com este ISBN já existe." };

        if (dto.PublishedDate > DateTime.Now)
            return new ApiResponse<BookDto> { Success = false, Message = "Data de publicação não pode ser no futuro." };

        if (dto.Pages <= 0)
            return new ApiResponse<BookDto> { Success = false, Message = "Número de páginas deve ser maior que zero." };

        var book = new Book
        {
            Title = dto.Title,
            Isbn = dto.Isbn,
            Description = dto.Description,
            PublishedDate = dto.PublishedDate,
            Pages = dto.Pages,
        };

        await _bookRepository.AddAsync(book);
        await _bookRepository.SaveChangesAsync();

        return new ApiResponse<BookDto>
        {
            Success = true,
            Message = "Livro criado com sucesso.",
            Data = MapToDto(book)
        };
    }

    public async Task<ApiResponse<BookDto>> CreateWithDetailsAsync(CreateBookDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            return new ApiResponse<BookDto> { Success = false, Message = "Título é obrigatório." };
        if (string.IsNullOrWhiteSpace(dto.Isbn))
            return new ApiResponse<BookDto> { Success = false, Message = "ISBN é obrigatório." };
        if (dto.Isbn.Length != 13)
            return new ApiResponse<BookDto> { Success = false, Message = "ISBN deve ter exatamente 13 caracteres." };
        var existingBook = await _bookRepository.GetByIsbnAsync(dto.Isbn);
        if (existingBook != null)
            return new ApiResponse<BookDto> { Success = false, Message = "Um livro com este ISBN já existe." };
        if (dto.PublishedDate > DateTime.Now)
            return new ApiResponse<BookDto> { Success = false, Message = "Data de publicação não pode ser no futuro." };
        if (dto.Pages <= 0)
            return new ApiResponse<BookDto> { Success = false, Message = "Número de páginas deve ser maior que zero." };

        if (string.IsNullOrWhiteSpace(dto.AuthorName))
            return new ApiResponse<BookDto> { Success = false, Message = "Nome do autor é obrigatório." };

        if (string.IsNullOrWhiteSpace(dto.GenreName))
            return new ApiResponse<BookDto> { Success = false, Message = "Nome do gênero é obrigatório." };

        var author = await _authorRepository.GetByNameAsync(dto.AuthorName);
        if (author == null)
        {
            author = new Author
            {
                Name = dto.AuthorName,
            };
            await _authorRepository.AddAsync(author);
        }

        var genre = await _genreRepository.GetByNameAsync(dto.GenreName);
        if (genre == null)
        {
            genre = new Genre
            {
                Name = dto.GenreName,
                Description = string.Empty
            };
            await _genreRepository.AddAsync(genre);
        }

        var book = new Book
        {
            Title = dto.Title,
            Isbn = dto.Isbn,
            Description = dto.Description,
            PublishedDate = dto.PublishedDate,
            Pages = dto.Pages,
            Price = 0m,
            AuthorId = author.Id,
            GenreId = genre.Id
        };

        await _bookRepository.AddAsync(book);
        await _bookRepository.SaveChangesAsync();

        book.Author = author;
        book.Genre = genre;

        return new ApiResponse<BookDto>
        {
            Success = true,
            Message = "Livro, autor e gênero criados/associados com sucesso.",
            Data = MapToDto(book)
        };
    }

    public async Task<ApiResponse<BookDto>> UpdateAsync(Guid id, UpdateBookDto dto)
    {
        var book = await _bookRepository.GetByIdWithDetailsAsync(id);
        if (book == null)
            return new ApiResponse<BookDto> { Success = false, Message = "Livro não encontrado." };

        if (string.IsNullOrWhiteSpace(dto.Title))
            return new ApiResponse<BookDto> { Success = false, Message = "Título é obrigatório." };

        if (string.IsNullOrWhiteSpace(dto.Isbn) || dto.Isbn.Length != 13)
            return new ApiResponse<BookDto> { Success = false, Message = "ISBN deve ter exatamente 13 caracteres." };

        if (dto.Isbn != book.Isbn)
        {
            var existingBook = await _bookRepository.GetByIsbnAsync(dto.Isbn);
            if (existingBook != null)
                return new ApiResponse<BookDto> { Success = false, Message = "Um livro com este ISBN já existe." };
        }

        var author = await _authorRepository.GetByNameAsync(dto.AuthorName);
        if (author == null)
        {
            author = new Author { Name = dto.AuthorName };
            await _authorRepository.AddAsync(author);
        }

        var genre = await _genreRepository.GetByNameAsync(dto.GenreName);
        if (genre == null)
        {
            genre = new Genre { Name = dto.GenreName, Description = string.Empty };
            await _genreRepository.AddAsync(genre);
        }

        if (dto.PublishedDate > DateTime.Now)
            return new ApiResponse<BookDto> { Success = false, Message = "Data de publicação não pode ser no futuro." };

        if (dto.Pages <= 0)
            return new ApiResponse<BookDto> { Success = false, Message = "Número de páginas deve ser maior que zero." };

        book.Title = dto.Title;
        book.Isbn = dto.Isbn;
        book.Description = dto.Description;
        book.PublishedDate = dto.PublishedDate;
        book.Pages = dto.Pages;
        book.AuthorId = author.Id;
        book.GenreId = genre.Id;
        book.Author = author;
        book.Genre = genre;

        await _bookRepository.UpdateAsync(book);
        await _bookRepository.SaveChangesAsync();

        return new ApiResponse<BookDto>
        {
            Success = true,
            Message = "Livro atualizado com sucesso.",
            Data = MapToDto(book)
        };
    }

    public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
    {
        var result = await _bookRepository.DeleteAsync(id);
        if (!result)
            return new ApiResponse<bool> { Success = false, Message = "Livro não encontrado." };

        await _bookRepository.SaveChangesAsync();
        return new ApiResponse<bool>
        {
            Success = true,
            Message = "Livro deletado com sucesso.",
            Data = true
        };
    }

    public async Task<ApiResponse<IEnumerable<BookDto>>> GetBooksByAuthorAsync(Guid authorId)
    {
        var author = await _authorRepository.GetByIdAsync(authorId);
        if (author == null)
            return new ApiResponse<IEnumerable<BookDto>> { Success = false, Message = "Autor não encontrado." };

        var books = await _bookRepository.GetBooksByAuthorAsync(authorId);
        return new ApiResponse<IEnumerable<BookDto>>
        {
            Success = true,
            Message = "Livros do autor recuperados com sucesso.",
            Data = books.Select(MapToDto)
        };
    }

    public async Task<ApiResponse<IEnumerable<BookDto>>> GetBooksByGenreAsync(Guid genreId)
    {
        var genre = await _genreRepository.GetByIdAsync(genreId);
        if (genre == null)
            return new ApiResponse<IEnumerable<BookDto>> { Success = false, Message = "Gênero não encontrado." };

        var books = await _bookRepository.GetBooksByGenreAsync(genreId);
        return new ApiResponse<IEnumerable<BookDto>>
        {
            Success = true,
            Message = "Livros do gênero recuperados com sucesso.",
            Data = books.Select(MapToDto)
        };
    }

    private BookDto MapToDto(Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Isbn = book.Isbn,
            Description = book.Description,
            PublishedDate = book.PublishedDate,
            Pages = book.Pages,
            Price = book.Price,
            AuthorId = book.AuthorId,
            GenreId = book.GenreId,
            AuthorName = book.Author?.Name ?? string.Empty,
            GenreName = book.Genre?.Name ?? string.Empty,
            CreatedAt = book.CreatedAt,
            UpdatedAt = book.UpdatedAt
        };
    }
}