using System;
using ERPModular.Core.Interfaces;

namespace ERPModular.Web.Services;

/// <summary>
/// Implementação que busca o TenantId.
/// No início do projeto, usamos um ID fixo para testes.
/// Depois, buscaremos do ClaimsPrincipal (Usuário logado).
/// </summary>
public class TenantProvider : ITenantProvider
{
    // Id fixo para a "Empresa Exemplo" durante o desenvolvimento inicial.
    private static readonly Guid DefaultTenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");

    public Guid GetTenantId()
    {
        return DefaultTenantId;
    }

    public Guid GetCompanyId()
    {
        // Por enquanto, retorna o mesmo Guid do Tenant para a "Matriz"
        return DefaultTenantId;
    }

    public string GetDomain()
    {
        return "Confeccao";
    }
}
