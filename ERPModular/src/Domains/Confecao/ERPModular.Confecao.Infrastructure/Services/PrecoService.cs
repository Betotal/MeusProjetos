using ERPModular.Confecao.Application.Interfaces;
using ERPModular.Confecao.Domain.Entities;
using ERPModular.Confecao.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ERPModular.Confecao.Infrastructure.Services;

public class PrecoService : IPrecoService
{
    private readonly ConfecaoDbContext _context;

    public PrecoService(ConfecaoDbContext context)
    {
        _context = context;
    }

    public async Task AlterarPrecoAsync(Guid variacaoId, decimal novoPreco, string? motivo = null)
    {
        // 1. Obter a variação atual
        var variacao = await _context.ProdutoVariacoes
            .FirstOrDefaultAsync(v => v.Id == variacaoId);

        if (variacao == null)
            throw new Exception("Variação de produto não encontrada.");

        // 2. Só registrar se o preço for realmente diferente
        if (variacao.PrecoVenda == novoPreco)
            return;

        // 3. Criar o registro de histórico
        var historico = new HistoricoPreco
        {
            VariacaoId = variacaoId,
            PrecoAntigo = variacao.PrecoVenda,
            PrecoNovo = novoPreco,
            DataAlteracao = DateTime.UtcNow,
            Motivo = motivo ?? "Alteração manual"
        };

        // 4. Atualizar o preço na variação
        variacao.PrecoVenda = novoPreco;

        // 5. Persistir de forma atômica
        _context.HistoricoPrecos.Add(historico);
        await _context.SaveChangesAsync();
    }

    public async Task<List<HistoricoPreco>> GetHistoricoAsync(Guid variacaoId)
    {
        return await _context.HistoricoPrecos
            .Where(h => h.VariacaoId == variacaoId)
            .OrderByDescending(h => h.DataAlteracao)
            .ToListAsync();
    }
}
