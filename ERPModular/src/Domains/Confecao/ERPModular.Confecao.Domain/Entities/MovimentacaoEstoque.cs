using ERPModular.Shared.Domain.Primitives;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPModular.Confecao.Domain.Entities;

public enum TipoMovimentacao
{
    Entrada,
    Saida,
    AjustePositivo,
    AjusteNegativo,
    Devolucao
}

/// <summary>
/// Registra cada movimentação física de estoque para auditoria e rastreabilidade.
/// </summary>
[Table("MovimentacoesEstoque")]
public class MovimentacaoEstoque : BaseEntity
{
    [Required]
    public Guid VariacaoId { get; set; }

    [Required]
    public TipoMovimentacao Tipo { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal Quantidade { get; set; }

    public DateTime DataMovimentacao { get; set; } = DateTime.UtcNow;

    [MaxLength(250)]
    public string Motivo { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? ReferenciaDocumento { get; set; } // NF, Pedido, etc.

    // Relacionamentos
    [ForeignKey("VariacaoId")]
    public virtual ProdutoVariacao? Variacao { get; set; }
}
