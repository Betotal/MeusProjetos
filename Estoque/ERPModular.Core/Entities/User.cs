using System;

namespace ERPModular.Core.Entities;

/// <summary>
/// Representa um usuário que pode acessar o sistema.
/// </summary>
public class User : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsAdminGlobal { get; set; } = false; // Admin do sistema (Antigravity/Suporte)
}
