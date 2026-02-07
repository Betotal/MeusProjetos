using System.ComponentModel.DataAnnotations;

namespace ERPModular.Shared.Domain.Models;

/// <summary>
/// Representa um segmento de negócio no SaaS (ex: Confecção, Farmácia).
/// Cada domínio possui seus próprios schemas nas fases subsequentes.
/// </summary>
public class Dominio
{
    [Key]
    public string Id { get; set; } = string.Empty; // ex: "confeccao", "farmacia"
    
    [Required]
    public string Nome { get; set; } = string.Empty;
    
    public string? Descricao { get; set; }
    
    public bool Ativo { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
