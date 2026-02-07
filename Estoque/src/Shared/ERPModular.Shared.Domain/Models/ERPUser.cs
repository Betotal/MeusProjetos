using Microsoft.AspNetCore.Identity;

namespace ERPModular.Shared.Domain.Models;

/// <summary>
/// Usuário customizado do sistema ERPModular.
/// </summary>
public class ERPUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Vínculos Multi-Tenant
    public string? DomainId { get; set; }
    public string? TenantId { get; set; }
    public string? EmpresaId { get; set; }
}
