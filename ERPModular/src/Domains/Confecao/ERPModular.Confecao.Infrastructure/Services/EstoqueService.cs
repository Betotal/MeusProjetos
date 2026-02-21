using ERPModular.Confecao.Application.Interfaces;
using ERPModular.Confecao.Domain.Entities;
using ERPModular.Confecao.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ERPModular.Confecao.Infrastructure.Services;

public class EstoqueService : IEstoqueService
{
    private readonly ConfecaoDbContext _context;

    public EstoqueService(ConfecaoDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProdutoVariacao>> GetAlertasEstoqueBaixoAsync()
    {
        return await _context.ProdutoVariacoes
            .Include(v => v.Produto)
            .Where(v => v.SaldoAtual <= v.EstoqueMinimo)
            .OrderBy(v => v.Produto!.Nome)
            .ToListAsync();
    }

    public async Task RegistrarMovimentacaoAsync(
        Guid variacaoId, 
        decimal quantidade, 
        TipoMovimentacao tipo, 
        string motivo, 
        string? referencia = null)
    {
        // 1. Obter a variação (com rastreamento para atualização)
        var variacao = await _context.ProdutoVariacoes
            .FirstOrDefaultAsync(v => v.Id == variacaoId);

        if (variacao == null)
            throw new Exception("Variação de produto não encontrada.");

        // 2. Criar o registro de movimentação
        var movimentacao = new MovimentacaoEstoque
        {
            VariacaoId = variacaoId,
            Tipo = tipo,
            Quantidade = quantidade,
            Motivo = motivo,
            ReferenciaDocumento = referencia,
            DataMovimentacao = DateTime.UtcNow
        };

        // 3. Atualizar o saldo da variação baseada no tipo
        if (tipo == TipoMovimentacao.Entrada || 
            tipo == TipoMovimentacao.AjustePositivo || 
            tipo == TipoMovimentacao.Devolucao)
        {
            variacao.SaldoAtual += quantidade;
        }
        else
        {
            variacao.SaldoAtual -= quantidade;
        }

        // 4. Salvar tudo em uma única transação atômica
        _context.MovimentacoesEstoque.Add(movimentacao);
        await _context.SaveChangesAsync();
    }

    public async Task<decimal> GetSaldoAtualAsync(Guid variacaoId)
    {
        return await _context.ProdutoVariacoes
            .Where(v => v.Id == variacaoId)
            .Select(v => v.SaldoAtual)
            .FirstOrDefaultAsync();
    }
}
