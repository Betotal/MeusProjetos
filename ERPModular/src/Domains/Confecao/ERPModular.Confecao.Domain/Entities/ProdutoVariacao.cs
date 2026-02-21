using ERPModular.Shared.Domain.Primitives;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPModular.Confecao.Domain.Entities;

/// <summary>
/// Representa uma variação específica (tamanho/grade) de um produto.
/// </summary>
[Table("ProdutoVariacoes")]
public class ProdutoVariacao : BaseEntity
{
    [Required]
    public Guid ProdutoId { get; set; }

    [Required]
    [MaxLength(20)]
    public string Tamanho { get; set; } = string.Empty; // P, M, G, 10, 12, etc.

    [Column(TypeName = "decimal(18,2)")]
    public decimal PrecoVenda { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal SaldoAtual { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal EstoqueMinimo { get; set; }

    // Relacionamentos
    [ForeignKey("ProdutoId")]
    public virtual Produto? Produto { get; set; }
}
