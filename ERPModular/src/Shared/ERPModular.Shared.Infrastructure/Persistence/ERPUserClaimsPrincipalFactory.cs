using System.Security.Claims;
using ERPModular.Shared.Domain.Constants;
using ERPModular.Shared.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ERPModular.Shared.Infrastructure.Persistence;

/// <summary>
/// Fábrica de claims personalizada que injeta DomainId, TenantId e EmpresaId 
/// no ClaimsPrincipal do usuário logado.
/// </summary>
public class ERPUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ERPUser, IdentityRole>
{
    public ERPUserClaimsPrincipalFactory(
        UserManager<ERPUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ERPUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        
        // Injeta as claims personalizadas para que o ExecutionContext possa lê-las
        identity.AddClaim(new Claim(ERPClaims.FullName, user.FullName ?? ""));
        identity.AddClaim(new Claim(ERPClaims.DisplayName, user.DisplayName ?? user.FullName ?? ""));
        
        if (!string.IsNullOrEmpty(user.DomainId))
            identity.AddClaim(new Claim(ERPClaims.DomainId, user.DomainId));
            
        if (!string.IsNullOrEmpty(user.TenantId))
            identity.AddClaim(new Claim(ERPClaims.TenantId, user.TenantId));
            
        if (!string.IsNullOrEmpty(user.EmpresaId))
            identity.AddClaim(new Claim(ERPClaims.EmpresaId, user.EmpresaId));

        identity.AddClaim(new Claim("IsActive", user.IsActive.ToString().ToLower()));

        return identity;
    }
}
