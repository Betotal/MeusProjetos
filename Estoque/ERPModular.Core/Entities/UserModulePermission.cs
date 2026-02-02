using ERPModular.Core.Enums;

namespace ERPModular.Core.Entities;

/// <summary>
/// Tabela de ligação que define qual usuário tem qual nível de acesso em cada módulo.
/// </summary>
public class UserModulePermission : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ModuleId { get; set; }
    public PermissionLevel PermissionLevel { get; set; } = PermissionLevel.None;
}
