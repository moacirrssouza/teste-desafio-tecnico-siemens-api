using BookManagementSystem.Application.DTOs.Author;
using BookManagementSystem.Application.Services.Interfaces;
using BookManagementSystem.Application.ViewModels;
using BookManagementSystem.Domain.Entities;
using BookManagementSystem.Infrastructure.Repositories.Interfaces;

namespace BookManagementSystem.Application.Services;

/// <summary>
/// Serviço de negócio para Autores.
/// </summary>
public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<ApiResponse<AuthorDto>> GetByIdAsync(Guid id)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author == null)
            return new ApiResponse<AuthorDto> { Success = false, Message = "Autor não encontrado." };

        return new ApiResponse<AuthorDto>
        {
            Success = true,
            Message = "Autor recuperado com sucesso.",
            Data = MapToDto(author)
        };
    }

    public async Task<ApiResponse<IEnumerable<AuthorDto>>> GetAllAsync()
    {
        var authors = await _authorRepository.GetAuthorsWithBooksAsync();
        return new ApiResponse<IEnumerable<AuthorDto>>
        {
            Success = true,
            Message = "Autores recuperados com sucesso.",
            Data = authors.Select(MapToDto)
        };
    }

    public async Task<ApiResponse<AuthorDto>> CreateAsync(CreateAuthorDto dto)
    {
        // Validação de negócio
        if (string.IsNullOrWhiteSpace(dto.Name))
            return new ApiResponse<AuthorDto> { Success = false, Message = "Nome é obrigatório." };

        if (string.IsNullOrWhiteSpace(dto.Email))
            return new ApiResponse<AuthorDto> { Success = false, Message = "Email é obrigatório." };

        var existingAuthor = await _authorRepository.GetByEmailAsync(dto.Email);
        if (existingAuthor != null)
            return new ApiResponse<AuthorDto> { Success = false, Message = "Este email já está sendo utilizado." };

        if (dto.BirthDate >= DateTime.Now)
            return new ApiResponse<AuthorDto> { Success = false, Message = "Data de nascimento inválida." };

        var author = new Author
        {
            Name = dto.Name,
            Email = dto.Email,
            BirthDate = dto.BirthDate,
            Biography = dto.Biography
        };

        await _authorRepository.AddAsync(author);
        await _authorRepository.SaveChangesAsync();

        return new ApiResponse<AuthorDto>
        {
            Success = true,
            Message = "Autor criado com sucesso.",
            Data = MapToDto(author)
        };
    }

    public async Task<ApiResponse<AuthorDto>> UpdateAsync(Guid id, UpdateAuthorDto dto)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author == null)
            return new ApiResponse<AuthorDto> { Success = false, Message = "Autor não encontrado." };

        if (string.IsNullOrWhiteSpace(dto.Name))
            return new ApiResponse<AuthorDto> { Success = false, Message = "Nome é obrigatório." };

        if (string.IsNullOrWhiteSpace(dto.Email))
            return new ApiResponse<AuthorDto> { Success = false, Message = "Email é obrigatório." };

        if (dto.BirthDate >= DateTime.Now)
            return new ApiResponse<AuthorDto> { Success = false, Message = "Data de nascimento inválida." };

        author.Name = dto.Name;
        author.Email = dto.Email;
        author.BirthDate = dto.BirthDate;
        author.Biography = dto.Biography;

        await _authorRepository.UpdateAsync(author);
        await _authorRepository.SaveChangesAsync();

        return new ApiResponse<AuthorDto>
        {
            Success = true,
            Message = "Autor atualizado com sucesso.",
            Data = MapToDto(author)
        };
    }

    public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
    {
        var result = await _authorRepository.DeleteAsync(id);
        if (!result)
            return new ApiResponse<bool> { Success = false, Message = "Autor não encontrado." };

        await _authorRepository.SaveChangesAsync();
        return new ApiResponse<bool>
        {
            Success = true,
            Message = "Autor deletado com sucesso.",
            Data = true
        };
    }

    private AuthorDto MapToDto(Author author)
    {
        return new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
            Email = author.Email,
            BirthDate = author.BirthDate,
            Biography = author.Biography,
            CreatedAt = author.CreatedAt,
            UpdatedAt = author.UpdatedAt,
            BookCount = author.Books?.Count ?? 0
        };
    }
}
