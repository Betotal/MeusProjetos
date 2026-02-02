using Microsoft.EntityFrameworkCore;
using ERPModular.Core.Entities;
using ERPModular.Core.Interfaces;
using System.Linq.Expressions;

namespace ERPModular.Infrastructure.Data;

public class ERPModularDbContext : DbContext
{
    private readonly ITenantProvider _tenantProvider;

    public ERPModularDbContext(DbContextOptions<ERPModularDbContext> options, ITenantProvider tenantProvider)
        : base(options)
    {
        _tenantProvider = tenantProvider;
    }

    // Tabelas (DbSets)
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<UserModulePermission> UserModulePermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Configurações via Fluent API (Explicação: No COBOL definimos o tamanho fixo, aqui definimos regras de banco)
        modelBuilder.Entity<Tenant>(entity => {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Document).IsRequired().HasMaxLength(20);
        });

        modelBuilder.Entity<User>(entity => {
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
        });

        // 2. CONFIGURAÇÃO DE MULTI-TENANCY AUTOMÁTICO (Row-Level Security)
        var currentDomain = _tenantProvider.GetDomain();
        var currentTenantId = _tenantProvider.GetTenantId();
        var currentCompanyId = _tenantProvider.GetCompanyId();

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                
                // Filtro 1: Domain
                var domainCheck = Expression.Equal(
                    Expression.Property(parameter, nameof(BaseEntity.Domain)),
                    Expression.Constant(currentDomain)
                );

                // Filtro 2: TenantId
                var tenantCheck = Expression.Equal(
                    Expression.Property(parameter, nameof(BaseEntity.TenantId)),
                    Expression.Constant(currentTenantId)
                );

                // Filtro 3: CompanyId
                var companyCheck = Expression.Equal(
                    Expression.Property(parameter, nameof(BaseEntity.CompanyId)),
                    Expression.Constant(currentCompanyId)
                );

                // Combinar filtros (AND)
                var combinedBody = Expression.AndAlso(domainCheck, Expression.AndAlso(tenantCheck, companyCheck));
                var lambda = Expression.Lambda(combinedBody, parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();
        var currentDomain = _tenantProvider.GetDomain();
        var currentTenantId = _tenantProvider.GetTenantId();
        var currentCompanyId = _tenantProvider.GetCompanyId();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                
                if (string.IsNullOrEmpty(entry.Entity.Domain))
                    entry.Entity.Domain = currentDomain;

                if (entry.Entity.TenantId == Guid.Empty)
                    entry.Entity.TenantId = currentTenantId;

                if (entry.Entity.CompanyId == Guid.Empty)
                    entry.Entity.CompanyId = currentCompanyId;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
                
                // Proteção contra alteração de contexto
                entry.Property(x => x.Domain).IsModified = false;
                entry.Property(x => x.TenantId).IsModified = false;
                entry.Property(x => x.CompanyId).IsModified = false;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
