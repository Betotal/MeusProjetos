using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ERPModular.Shared.Domain.Interfaces;
using ERPModular.Shared.Domain.Primitives;

namespace ERPModular.Shared.Infrastructure.Persistence.Interceptors;

/// <summary>
/// Interceptor que preenche automaticamente os campos de Auditoria e Tenant antes de salvar.
/// </summary>
public class AuditInterceptor : SaveChangesInterceptor
{
    private readonly IExecutionContextAccessor _contextAccessor;

    public AuditInterceptor(IExecutionContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context == null) return base.SavingChangesAsync(eventData, result, cancellationToken);

        var context = _contextAccessor.GetContext();

        foreach (var entry in eventData.Context.ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.CreatedBy = context.UserId;
                entry.Entity.TenantId = context.TenantId;
                entry.Entity.EmpresaId = context.EmpresaId;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedBy = context.UserId;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
