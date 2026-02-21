using ERPModular.Confecao.Domain.Entities;
using ERPModular.Shared.Domain.Interfaces;
using ERPModular.Shared.Infrastructure.Persistence;
using ERPModular.Shared.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace ERPModular.Confecao.Infrastructure.Persistence;

public class ConfecaoDbContext : BaseDbContext
{
    public ConfecaoDbContext(
        DbContextOptions<ConfecaoDbContext> options, 
        IExecutionContextAccessor contextAccessor) 
        : base(options, contextAccessor)
    {
    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<ProdutoVariacao> ProdutoVariacoes { get; set; }
    public DbSet<MovimentacaoEstoque> MovimentacoesEstoque { get; set; }
    public DbSet<HistoricoPreco> HistoricoPrecos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Chama a base para aplicar Filters e Schema dinâmico
        base.OnModelCreating(modelBuilder);

        // Configurações específicas do domínio Confecção
        modelBuilder.Entity<Produto>().ToTable("Produtos");
        modelBuilder.Entity<ProdutoVariacao>().ToTable("ProdutoVariacoes");
        modelBuilder.Entity<MovimentacaoEstoque>().ToTable("MovimentacoesEstoque");
        modelBuilder.Entity<HistoricoPreco>().ToTable("HistoricoPrecos");

        // Relacionamento Produto -> Variacoes
        modelBuilder.Entity<Produto>()
            .HasMany(p => p.Variacoes)
            .WithOne(v => v.Produto)
            .HasForeignKey(v => v.ProdutoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
