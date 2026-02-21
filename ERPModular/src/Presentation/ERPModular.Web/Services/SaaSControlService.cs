using ERPModular.Shared.Domain.Models;
using ERPModular.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ERPModular.Web.Services;

public enum LicenseStatus
{
    Active,
    GracePeriod, // Carência (Até 7 dias após expiração)
    ReadOnly,    // Somente-Leitura (Após 7 dias de carência)
    Expired      // Crítico (Se nem licença houver - opcional)
}

public class SaaSControlService
{
    private readonly SharedDbContext _dbContext;

    public SaaSControlService(SharedDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(LicenseStatus Status, DateTime? RestrictionDate, string? Warning)> GetLicenseStatusAsync(string tenantId, string domainId)
    {
        var licenca = await _dbContext.Licencas
            .AsNoTracking()
            .Where(l => l.TenantId == tenantId && l.DomainId == domainId && l.Ativa)
            .OrderByDescending(l => l.InicioValidade)
            .FirstOrDefaultAsync();

        if (licenca == null) return (LicenseStatus.Expired, null, null);

        string? warning = null;
        var hoje = DateTime.UtcNow;

        // Se a licença foi suspensa ou cancelada manualmente, mantemos acesso ReadOnly (conforme desejo do usuário)
        if (licenca.Status == StatusLicenca.Suspensa || licenca.Status == StatusLicenca.Cancelada)
        {
            return (LicenseStatus.ReadOnly, null, "Licença suspensa/cancelada pelo administrador. Acesso restrito a consulta.");
        }

        // Verificação de proximidade de expiração (Aviso proativo)
        if (licenca.FimValidade.HasValue)
        {
            var diasParaExpirar = (licenca.FimValidade.Value - hoje).TotalDays;
            if (diasParaExpirar > 0 && diasParaExpirar <= 7)
            {
                warning = $"Atenção: Sua licença expira em {(int)Math.Ceiling(diasParaExpirar)} dias. Contate o suporte para renovação.";
            }
        }

        // 1. Vigente
        if (licenca.FimValidade == null || licenca.FimValidade > hoje)
        {
            return (LicenseStatus.Active, null, warning);
        }

        // 2. Carência (7 dias após FimValidade)
        var dataRestricao = licenca.FimValidade.Value.AddDays(7);
        if (hoje <= dataRestricao)
        {
            return (LicenseStatus.GracePeriod, dataRestricao, "Licença expirada. Período de carência ativo.");
        }

        // 3. Restrito (Somente Leitura)
        return (LicenseStatus.ReadOnly, null, "Licença expirada. Acesso restrito a consulta.");
    }
}
