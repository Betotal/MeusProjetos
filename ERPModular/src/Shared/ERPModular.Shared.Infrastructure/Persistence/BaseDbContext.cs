using Microsoft.EntityFrameworkCore;
using ERPModular.Shared.Domain.Interfaces;
using ERPModular.Shared.Domain.Primitives;
using ERPModular.Shared.Infrastructure.Persistence.Interceptors;
using System.Linq.Expressions;

namespace ERPModular.Shared.Infrastructure.Persistence;

/// <summary>
/// DbContext base que implementa isolamento Multi-Schema e Filtros Globais.
/// </summary>
public abstract class BaseDbContext : DbContext
{
    protected readonly IExecutionContextAccessor ContextAccessor;

    protected BaseDbContext(
        DbContextOptions options, 
        IExecutionContextAccessor contextAccessor) : base(options)
    {
        ContextAccessor = contextAccessor;
    }

    /// <summary>
    /// Propriedade utilizada pelo Global Query Filter para obter o TenantId atual.
    /// </summary>
    protected string CurrentTenantId => ContextAccessor.GetContext().TenantId;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var context = ContextAccessor.GetContext();

        // 1. ISOLAMENTO POR DOMÍNIO (SCHEMA)
        // O schema será domain_nomeodominio (ex: domain_confeccao)
        if (!string.IsNullOrEmpty(context.DomainId))
        {
            modelBuilder.HasDefaultSchema($"domain_{context.DomainId.ToLower()}");
        }

        // 2. FILTROS GLOBAIS (QUERY FILTERS)
        // Aplica automaticamente "WHERE TenantId = CurrentTenantId" em todas as consultas de BaseEntity
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(BaseDbContext)
                    .GetMethod(nameof(SetGlobalQueryFilter), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.MakeGenericMethod(entityType.ClrType);

                method?.Invoke(this, new object[] { modelBuilder });
            }
        }

        base.OnModelCreating(modelBuilder);
    }

    private void SetGlobalQueryFilter<TEntity>(ModelBuilder modelBuilder) where TEntity : BaseEntity
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(e => e.TenantId == CurrentTenantId);
    }
}
