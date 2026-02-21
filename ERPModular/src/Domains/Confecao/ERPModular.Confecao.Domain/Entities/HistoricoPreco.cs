using ERPModular.Shared.Domain.Primitives;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPModular.Confecao.Domain.Entities;

/// <summary>
/// Registra o histórico de alterações de preço de uma variação de produto.
/// </summary>
[Table("HistoricoPrecos")]
public class HistoricoPreco : BaseEntity
{
    [Required]
    public Guid VariacaoId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PrecoAntigo { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PrecoNovo { get; set; }

    public DateTime DataAlteracao { get; set; } = DateTime.UtcNow;

    [MaxLength(250)]
    public string? Motivo { get; set; }

    // Relacionamentos
    [ForeignKey("VariacaoId")]
    public virtual ProdutoVariacao? Variacao { get; set; }
}
