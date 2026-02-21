using ERPModular.Confecao.Domain.Entities;

namespace ERPModular.Confecao.Application.Interfaces;

public interface IEstoqueService
{
    /// <summary>
    /// Registra uma movimentação e atualiza o saldo da variação de forma atômica.
    /// </summary>
    Task RegistrarMovimentacaoAsync(
        Guid variacaoId, 
        decimal quantidade, 
        TipoMovimentacao tipo, 
        string motivo, 
        string? referencia = null);

    /// <summary>
    /// Retorna uma lista de variações que estão com saldo igual ou abaixo do estoque mínimo.
    /// </summary>
    Task<List<ProdutoVariacao>> GetAlertasEstoqueBaixoAsync();

    /// <summary>
    /// Retorna o saldo atualizado de uma variação.
    /// </summary>
    Task<decimal> GetSaldoAtualAsync(Guid variacaoId);
}
