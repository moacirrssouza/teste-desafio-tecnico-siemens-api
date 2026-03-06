namespace BookManagementSystem.Application.ViewModels;

/// <summary>
/// ViewModel para respostas padronizadas da API.
/// </summary>
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }
}
