using System.Security.Claims;
using ERPModular.Shared.Domain.Constants;
using ERPModular.Shared.Domain.Interfaces;
using ERPModular.Shared.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace ERPModular.Shared.Infrastructure.Context;

/// <summary>
/// Implementação do acessor que extrai o contexto das Claims do usuário logado no HttpContext.
/// </summary>
public class ExecutionContextAccessor : IExecutionContextAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ERPExecutionContext GetContext()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null || user.Identity?.IsAuthenticated != true)
        {
            // Retorna um contexto vazio (útil para operações anônimas ou seed)
            return new ERPExecutionContext(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        // Extrai os valores das claims personalizadas
        var domainId = user.FindFirst(ERPClaims.DomainId)?.Value ?? string.Empty;
        
        // Normalização: Garante compatibilidade caso o usuário tenha um cookie antigo (confeccao -> confecao)
        if (domainId.Equals("confeccao", StringComparison.OrdinalIgnoreCase))
        {
            domainId = "confecao";
        }

        var tenantId = user.FindFirst(ERPClaims.TenantId)?.Value ?? string.Empty;
        var empresaId = user.FindFirst(ERPClaims.EmpresaId)?.Value ?? string.Empty;
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        return new ERPExecutionContext(domainId, tenantId, empresaId, userId);
    }
}
