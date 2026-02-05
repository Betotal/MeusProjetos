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
    private readonly TenantInterceptor _tenantInterceptor;

    protected BaseDbContext(
        DbContextOptions options, 
        IExecutionContextAccessor contextAccessor,
        TenantInterceptor tenantInterceptor) : base(options)
    {
        ContextAccessor = contextAccessor;
        _tenantInterceptor = tenantInterceptor;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Adiciona o interceptor de auditoria e tenant
        optionsBuilder.AddInterceptors(_tenantInterceptor);
        
        base.OnConfiguring(optionsBuilder);
    }

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
        // Aplica automaticamente "WHERE TenantId = X" em todas as consultas de BaseEntity
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var body = Expression.Equal(
                    Expression.Property(parameter, nameof(BaseEntity.TenantId)),
                    Expression.Constant(context.TenantId)
                );
                
                var lambda = Expression.Lambda(body, parameter);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }

        base.OnModelCreating(modelBuilder);
    }
}
