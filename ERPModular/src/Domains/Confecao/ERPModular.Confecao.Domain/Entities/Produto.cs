using ERPModular.Shared.Domain.Primitives;
using System.ComponentModel.DataAnnotations;

namespace ERPModular.Confecao.Domain.Entities;

/// <summary>
/// Representa um produto acabado no domínio de Confecção.
/// </summary>
public class Produto : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Descricao { get; set; }

    [Required]
    [MaxLength(50)]
    public string CodigoReferencia { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Categoria { get; set; }

    public string? FotoUrl { get; set; }

    public bool Ativo { get; set; } = true;

    // Relacionamentos
    public virtual ICollection<ProdutoVariacao> Variacoes { get; set; } = new List<ProdutoVariacao>();
}
