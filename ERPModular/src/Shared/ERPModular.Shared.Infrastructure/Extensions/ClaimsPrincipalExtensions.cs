using System.Security.Claims;
using ERPModular.Shared.Domain.Constants;

namespace ERPModular.Shared.Infrastructure.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetDomainId(this ClaimsPrincipal principal) => 
        principal.FindFirstValue(ERPClaims.DomainId);
    
    public static string? GetTenantId(this ClaimsPrincipal principal) => 
        principal.FindFirstValue(ERPClaims.TenantId);
        
    public static string? GetEmpresaId(this ClaimsPrincipal principal) => 
        principal.FindFirstValue(ERPClaims.EmpresaId);

    public static string? GetFullName(this ClaimsPrincipal principal) => 
        principal.FindFirstValue(ERPClaims.FullName);

    public static string? GetDisplayName(this ClaimsPrincipal principal) => 
        principal.FindFirstValue(ERPClaims.DisplayName);
}
