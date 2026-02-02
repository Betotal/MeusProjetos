using System;

namespace ERPModular.Core.Entities;

/// <summary>
/// Classe base para todas as entidades do sistema.
/// Garante que todos os registros tenham rastreabilidade e pertençam a um Tenant.
/// </summary>
public abstract class BaseEntity
{
    // O Identificador único do registro. Usamos Guid por segurança.
    public Guid Id { get; set; } = Guid.NewGuid();

    // O Domínio identifica o segmento de negócio (ex: Confeccao, Farmacia).
    public string Domain { get; set; } = string.Empty;

    // O TenantId vincula este dado a uma empresa assinante específica.
    public Guid TenantId { get; set; }

    // O CompanyId vincula este dado a uma unidade/filial específica.
    public Guid CompanyId { get; set; }

    // Rastreabilidade: Quando foi criado/alterado.
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Soft Delete: Em vez de excluir do banco, desativamos o registro.
    public bool IsActive { get; set; } = true;
}
