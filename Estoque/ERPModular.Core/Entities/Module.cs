using System;

namespace ERPModular.Core.Entities;

/// <summary>
/// Define os módulos disponíveis no sistema (Ex: Compras, Estoque, Vendas).
/// </summary>
public class Module : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty; // Nome do ícone MudBlazor
}
