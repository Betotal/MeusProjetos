using System.ComponentModel.DataAnnotations;

namespace ERPModular.Shared.Domain.Models;


public enum TipoLicenca
{
    Normal,
    Degustacao,
    Cortesia
}

public enum StatusLicenca
{
    Ativa,
    Suspensa,
    Cancelada
}

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

    [Required]
    public TipoLicenca Tipo { get; set; } = TipoLicenca.Normal;

    [Required]
    public StatusLicenca Status { get; set; } = StatusLicenca.Ativa;

    // Limites (Opcional, mas útil para o futuro)
    public int MaxUsuarios { get; set; } = 0; // 0 = sem limite
    public int MaxEmpresas { get; set; } = 0; // 0 = sem limite
    
    public string? Observacoes { get; set; }
}
