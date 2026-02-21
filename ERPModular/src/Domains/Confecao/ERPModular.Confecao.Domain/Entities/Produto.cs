using ERPModular.Shared.Domain.Primitives;

namespace ERPModular.Confecao.Domain.Entities;

public class Produto : BaseEntity
{
    public string Nome { get; set; } = string.Empty;
    public string Referencia { get; set; } = string.Empty;
    public decimal Preco { get; set; }
}
