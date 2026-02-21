using ERPModular.Confecao.Domain.Entities;

namespace ERPModular.Confecao.Application.Interfaces;

public interface IPrecoService
{
    /// <summary>
    /// Altera o preço de uma variação e registra o histórico de forma atômica.
    /// </summary>
    Task AlterarPrecoAsync(Guid variacaoId, decimal novoPreco, string? motivo = null);

    /// <summary>
    /// Retorna o histórico de preços de uma variação específica.
    /// </summary>
    Task<List<HistoricoPreco>> GetHistoricoAsync(Guid variacaoId);
}
