namespace ERPModular.Shared.Domain.Models;

/// <summary>
/// Registro de auditoria para ações administrativas críticas.
/// </summary>
public class AdminAuditLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime DataHora { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// ID do administrador que realizou a ação.
    /// </summary>
    public string UsuarioId { get; set; } = string.Empty;
    
    /// <summary>
    /// Nome do usuário para facilitar consultas (desnormalizado).
    /// </summary>
    public string UsuarioNome { get; set; } = string.Empty;
    
    /// <summary>
    /// Tipo de ação realizada (ex: "Bloqueio de Usuário", "Provisionamento de Tenant").
    /// </summary>
    public string Acao { get; set; } = string.Empty;
    
    /// <summary>
    /// Detalhes adicionais da ação em formato JSON ou texto.
    /// </summary>
    public string? Detalhes { get; set; }
    
    /// <summary>
    /// Endereço IP de onde a ação foi realizada (opcional).
    /// </summary>
    public string? IpAddress { get; set; }
    
    /// <summary>
    /// ID do tenant afetado (se aplicável).
    /// </summary>
    public string? TenantId { get; set; }
}
