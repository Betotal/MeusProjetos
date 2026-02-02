using System;

namespace ERPModular.Core.Entities;

/// <summary>
/// Representa uma Empresa (Tenant) no sistema.
/// </summary>
public class Tenant : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty; // CNPJ ou CPF
    
    // Controle de Licença
    public DateTime SubscriptionEndDate { get; set; }
    public bool IsTrial { get; set; } = true;
}
