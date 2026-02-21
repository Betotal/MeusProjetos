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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Chama a base para aplicar Filters e Schema dinâmico
        base.OnModelCreating(modelBuilder);

        // Configurações específicas do domínio Confecção
        modelBuilder.Entity<Produto>().ToTable("Produtos");
    }
}
