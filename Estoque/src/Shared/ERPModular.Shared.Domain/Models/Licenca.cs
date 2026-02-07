using System.ComponentModel.DataAnnotations;

namespace ERPModular.Shared.Domain.Models;

/// <summary>
/// Controla o acesso de um Tenant a um determinado Domínio.
/// </summary>
public class Licenca
{
    public Guid Id { get; set; }
    
    [Required]
    public string TenantId { get; set; } = string.Empty;
    
    [Required]
    public string DomainId { get; set; } = string.Empty;
    
    public DateTime InicioValidade { get; set; }
    
    public DateTime? FimValidade { get; set; } // Null = Vitalícia (para testes)
    
    public bool Ativa { get; set; } = true;
    
    public string? Observacoes { get; set; }
}
