using ERPModular.Shared.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ERPModular.Web.Middleware;

/// <summary>
/// Middleware que valida se o contexto de execução (Domínio, Tenant, Empresa)
/// está corretamente preenchido para usuários autenticados.
/// </summary>
public class ExecutionContextMiddleware
{
    private readonly RequestDelegate _next;

    public ExecutionContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IExecutionContextAccessor contextAccessor)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var path = context.Request.Path.Value?.ToLower() ?? "";
            
            // Ignora rotas que não exigem contexto completo ou que são de erro/saída
            var isStaticFile = path.Contains(".") && !path.EndsWith(".razor");
            var isIdentityFlow = path.Contains("/account/") || path.Contains("/login") || path.Contains("/logout");
            var isPublic = path == "/" || path.Contains("/error") || path.Contains("/restricted");
            var isBlazorInternal = path.Contains("/_blazor") || path.Contains("/_framework") || path.Contains("/negotiate");

            if (!isStaticFile && !isIdentityFlow && !isPublic && !isBlazorInternal)
            {
                // SuperAdmin não precisa ter DomainId ou TenantId vinculados (contexto global)
                if (context.User.IsInRole("SuperAdmin"))
                {
                    await _next(context);
                    return;
                }

                var executionContext = contextAccessor.GetContext();

                // Validação CRÍTICA: Se logado, DEVE ter Domain e Tenant
                if (string.IsNullOrEmpty(executionContext.DomainId) || 
                    string.IsNullOrEmpty(executionContext.TenantId))
                {
                    // Se o contexto estiver quebrado, força logout ou redireciona
                    context.Response.Redirect("/Account/Login?returnUrl=" + context.Request.Path);
                    return;
                }
            }
        }

        await _next(context);
    }
}
