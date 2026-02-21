using Microsoft.AspNetCore.Identity;

namespace ERPModular.Shared.Domain.Models;

/// <summary>
/// Usuário customizado do sistema ERPModular.
/// </summary>
public class ERPUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public string? DisplayName { get; set; } // Nome curto para exibição (ex: "Beto")
    public string? AvatarUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Funcao { get; set; } // O que o usuário faz (ex: "Vendedor")
    public string? NivelAcesso { get; set; } // O que o usuário acessa (ex: "Consulta")
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Controle de Onboarding
    public bool InvitationAccepted { get; set; } = false;
    public string? InvitationToken { get; set; }
    
    // Recuperação de Senha via Admin + Pergunta de Segurança
    public string? SecurityQuestion { get; set; }
    public string? SecurityAnswer { get; set; }
    public bool ResetPasswordRequested { get; set; } = false;
    public bool ResetPasswordAllowed { get; set; } = false;

    // Vínculos Multi-Tenant
    public string? DomainId { get; set; }
    public string? TenantId { get; set; }
    public string? EmpresaId { get; set; }
}
