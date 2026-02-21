using System.ComponentModel.DataAnnotations;

namespace ERPModular.Shared.Domain.Models;

/// <summary>
/// Representa a empresa (Tenant) no sistema.
/// Responsável pelo isolamento de dados e configurações específicas do cliente.
/// </summary>
public class Tenant
{
    [Key]
    public string Id { get; set; } = string.Empty; // ex: "tenant-conf-01"

    [Required]
    public string NomeFantasia { get; set; } = string.Empty;

    [Required]
    public string DomainId { get; set; } = string.Empty;

    public bool Ativo { get; set; } = true;

    public bool SomenteLeitura { get; set; } = false;

    public string? LogoUrl { get; set; }

    public DateTime DataAdesao { get; set; } = DateTime.UtcNow;

    // Relacionamento com o Domínio (Informacional)
    public virtual Dominio? Dominio { get; set; }
}
