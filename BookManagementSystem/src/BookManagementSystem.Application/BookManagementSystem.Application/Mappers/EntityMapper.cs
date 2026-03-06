using BookManagementSystem.Application.Services.Interfaces;
using BookManagementSystem.Domain.Entities;

namespace BookManagementSystem.Application.Mappers;

/// <summary>
/// Mapeador para conversão entre entidades e DTOs.
/// Implementa o padrão de mapeamento entre camadas.
/// </summary>
public static class EntityMapper
{
    /// <summary>
    /// Configura os mapeamentos automáticos entre entidades e DTOs.
    /// Pode ser usado com AutoMapper em projetos futuros.
    /// </summary>
    public static void ConfigureMappings()
    {
        // Os mapeamentos estão sendo feitos manualmente nos services por simplicidade
        // Para projetos maiores, considere usar AutoMapper com profiles
    }
}
