namespace ERPModular.Shared.Domain.Models;

/// <summary>
/// Objeto que carrega a identidade e o contexto de isolamento da operação atual.
/// </summary>
/// <param name="DomainId">Segmento de negócio (ex: Confecção, Farmácia)</param>
/// <param name="TenantId">Identificador do Cliente (Empresa contratante)</param>
/// <param name="EmpresaId">Identificador da Unidade/Filial específica</param>
/// <param name="UserId">Usuário que está realizando a operação</param>
public record ExecutionContext(
    string DomainId,
    string TenantId,
    string EmpresaId,
    string UserId);
