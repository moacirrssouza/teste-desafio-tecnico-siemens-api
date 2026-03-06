using BookManagementSystem.Application.DTOs.Genre;
using BookManagementSystem.Application.Services.Interfaces;
using BookManagementSystem.Application.ViewModels;
using BookManagementSystem.Domain.Entities;
using BookManagementSystem.Infrastructure.Repositories.Interfaces;

namespace BookManagementSystem.Application.Services;

/// <summary>
/// Serviço de negócio para Gêneros.
/// </summary>
public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;

    public GenreService(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<ApiResponse<GenreDto>> GetByIdAsync(Guid id)
    {
        var genre = await _genreRepository.GetByIdAsync(id);
        if (genre == null)
            return new ApiResponse<GenreDto> { Success = false, Message = "Gênero não encontrado." };

        return new ApiResponse<GenreDto>
        {
            Success = true,
            Message = "Gênero recuperado com sucesso.",
            Data = MapToDto(genre)
        };
    }

    public async Task<ApiResponse<IEnumerable<GenreDto>>> GetAllAsync()
    {
        var genres = await _genreRepository.GetGenresWithBooksAsync();
        return new ApiResponse<IEnumerable<GenreDto>>
        {
            Success = true,
            Message = "Gêneros recuperados com sucesso.",
            Data = genres.Select(MapToDto)
        };
    }

    public async Task<ApiResponse<GenreDto>> CreateAsync(CreateGenreDto dto)
    {
        // Validação de negócio
        if (string.IsNullOrWhiteSpace(dto.Name))
            return new ApiResponse<GenreDto> { Success = false, Message = "Nome é obrigatório." };

        var existingGenre = await _genreRepository.GetByNameAsync(dto.Name);
        if (existingGenre != null)
            return new ApiResponse<GenreDto> { Success = false, Message = "Este gênero já existe." };

        var genre = new Genre
        {
            Name = dto.Name,
            Description = dto.Description
        };

        await _genreRepository.AddAsync(genre);
        await _genreRepository.SaveChangesAsync();

        return new ApiResponse<GenreDto>
        {
            Success = true,
            Message = "Gênero criado com sucesso.",
            Data = MapToDto(genre)
        };
    }

    public async Task<ApiResponse<GenreDto>> UpdateAsync(Guid id, UpdateGenreDto dto)
    {
        var genre = await _genreRepository.GetByIdAsync(id);
        if (genre == null)
            return new ApiResponse<GenreDto> { Success = false, Message = "Gênero não encontrado." };

        if (string.IsNullOrWhiteSpace(dto.Name))
            return new ApiResponse<GenreDto> { Success = false, Message = "Nome é obrigatório." };

        genre.Name = dto.Name;
        genre.Description = dto.Description;

        await _genreRepository.UpdateAsync(genre);
        await _genreRepository.SaveChangesAsync();

        return new ApiResponse<GenreDto>
        {
            Success = true,
            Message = "Gênero atualizado com sucesso.",
            Data = MapToDto(genre)
        };
    }

    public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
    {
        var result = await _genreRepository.DeleteAsync(id);
        if (!result)
            return new ApiResponse<bool> { Success = false, Message = "Gênero não encontrado." };

        await _genreRepository.SaveChangesAsync();
        return new ApiResponse<bool>
        {
            Success = true,
            Message = "Gênero deletado com sucesso.",
            Data = true
        };
    }

    private GenreDto MapToDto(Genre genre)
    {
        return new GenreDto
        {
            Id = genre.Id,
            Name = genre.Name,
            Description = genre.Description,
            CreatedAt = genre.CreatedAt,
            UpdatedAt = genre.UpdatedAt,
            BookCount = genre.Books?.Count ?? 0
        };
    }
}
