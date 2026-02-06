namespace ERPModular.Shared.Domain.Models;

/// <summary>
/// Contexto de execução que identifica o domínio, tenant, empresa e usuário
/// de cada operação no sistema.
/// </summary>
public record ERPExecutionContext(
    string DomainId,
    string TenantId,
    string EmpresaId,
    string UserId);
