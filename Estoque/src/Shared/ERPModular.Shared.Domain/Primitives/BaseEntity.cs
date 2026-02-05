using ERPModular.Shared.Domain.Interfaces;

namespace ERPModular.Shared.Domain.Primitives;

/// <summary>
/// Classe base para todas as entidades do sistema.
/// Garante isolamento por Tenant/Empresa e auditoria automática.
/// </summary>
public abstract class BaseEntity : IAuditEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    
    // Isolamento
    public string TenantId { get; set; } = string.Empty;
    public string EmpresaId { get; set; } = string.Empty;
    
    // Auditoria
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}
