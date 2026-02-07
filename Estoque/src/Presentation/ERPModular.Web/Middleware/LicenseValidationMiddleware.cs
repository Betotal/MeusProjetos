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

    public async Task InvokeAsync(HttpContext context, SharedDbContext dbContext)
    {
        var user = context.User;

        // Só valida se o usuário estiver autenticado e não for uma rota de erro/logout/login
        if (user.Identity?.IsAuthenticated == true && 
            !context.Request.Path.Value!.Contains("/Account/") &&
            !context.Request.Path.Value!.Contains("/Error"))
        {
            var tenantId = user.GetTenantId();
            var domainId = user.GetDomainId();

            if (!string.IsNullOrEmpty(tenantId) && !string.IsNullOrEmpty(domainId))
            {
                var licencaAtiva = await dbContext.Licencas
                    .AnyAsync(l => l.TenantId == tenantId && 
                                   l.DomainId == domainId && 
                                   l.Ativa && 
                                   (l.FimValidade == null || l.FimValidade > DateTime.UtcNow));

                if (!licencaAtiva)
                {
                    // Se não tiver licença, redireciona ou bloqueia
                    context.Response.Redirect("/Account/LicenseExpired");
                    return;
                }
            }
        }

        await _next(context);
    }
}
