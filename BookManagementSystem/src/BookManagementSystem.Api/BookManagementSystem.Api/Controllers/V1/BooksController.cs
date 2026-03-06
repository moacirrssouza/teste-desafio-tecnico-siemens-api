using BookManagementSystem.Application.DTOs.Book;
using BookManagementSystem.Application.Services.Interfaces;
using BookManagementSystem.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementSystem.Api.Controllers.V1;

/// <summary>
/// Controller para gerenciar Livros.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly ILogger<BooksController> _logger;

    public BooksController(IBookService bookService, ILogger<BooksController> logger)
    {
        _bookService = bookService;
        _logger = logger;
    }

    /// <summary>
    /// Obtém um livro pelo ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation($"Obtendo livro com ID: {id}");
        var response = await _bookService.GetByIdAsync(id);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    /// <summary>
    /// Obtém todos os livros.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Obtendo todos os livros");
        var response = await _bookService.GetAllAsync();
        return Ok(response);
    }

    /// <summary>
    /// Obtém livros por autor.
    /// </summary>
    [HttpGet("author/{authorId}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByAuthor(Guid authorId)
    {
        _logger.LogInformation($"Obtendo livros do autor: {authorId}");
        var response = await _bookService.GetBooksByAuthorAsync(authorId);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    /// <summary>
    /// Obtém livros por gênero.
    /// </summary>
    [HttpGet("genre/{genreId}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByGenre(Guid genreId)
    {
        _logger.LogInformation($"Obtendo livros do gênero: {genreId}");
        var response = await _bookService.GetBooksByGenreAsync(genreId);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }
 
    /// <summary>
    /// Cria um novo livro incluindo autor e gênero.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
    {
        _logger.LogInformation($"Criando livro completo: {dto.Title} / {dto.AuthorName} / {dto.GenreName}");
        var response = await _bookService.CreateWithDetailsAsync(dto);

        if (!response.Success)
            return BadRequest(response);

        return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
    }

    /// <summary>
    /// Atualiza um livro existente.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookDto dto)
    {
        _logger.LogInformation($"Atualizando livro com ID: {id}");
        var response = await _bookService.UpdateAsync(id, dto);

        if (!response.Success)
            return response.Message.Contains("não encontrado") ? NotFound(response) : BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    /// Deleta um livro.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation($"Deletando livro com ID: {id}");
        var response = await _bookService.DeleteAsync(id);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }
}