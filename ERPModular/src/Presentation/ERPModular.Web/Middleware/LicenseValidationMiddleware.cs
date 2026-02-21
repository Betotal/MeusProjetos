using Microsoft.EntityFrameworkCore;
using ERPModular.Shared.Infrastructure.Persistence;
using ERPModular.Shared.Infrastructure.Extensions;

namespace ERPModular.Web.Middleware;

public class LicenseValidationMiddleware
{
    private readonly RequestDelegate _next;

    public LicenseValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, SharedDbContext dbContext, ERPModular.Web.Services.SaaSControlService saasService)
    {
        var user = context.User;

        var path = context.Request.Path.Value?.ToLower() ?? "";
        bool isEssentialAuthPath = path.Contains("/account/login") || 
                                   path.Contains("/account/logout") || 
                                   path.Contains("/account/restricted") ||
                                   path.Contains("/account/licenseexpired");

        if (user.Identity?.IsAuthenticated == true && 
            !isEssentialAuthPath &&
            !path.Contains("/error"))
        {
            var userId = user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // 1. VERIFICAÇÃO PRIORITÁRIA: Usuário Ativo
            if (!string.IsNullOrEmpty(userId))
            {
                // Usamos AsNoTracking para evitar problemas de concorrência com outros componentes Blazor
                var dbUser = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
                if (dbUser != null && !dbUser.IsActive)
                {
                    context.Response.Redirect("/Account/Restricted?reason=user_blocked");
                    return;
                }
            }

            var tenantId = user.GetTenantId();
            var domainId = user.GetDomainId();

            // 2. Verificar Licença
            if (!string.IsNullOrEmpty(tenantId) && !string.IsNullOrEmpty(domainId))
            {
                var (status, restrictionDate, warning) = await saasService.GetLicenseStatusAsync(tenantId, domainId);

                // Armazena no contexto para uso na UI (MainLayout / NavMenu)
                context.Items["LicenseStatus"] = status;
                context.Items["RestrictionDate"] = restrictionDate;
                context.Items["LicenseWarning"] = warning;

                if (status == ERPModular.Web.Services.LicenseStatus.Expired)
                {
                    context.Response.Redirect("/Account/LicenseExpired");
                    return;
                }
            }
        }

        await _next(context);
    }
}
