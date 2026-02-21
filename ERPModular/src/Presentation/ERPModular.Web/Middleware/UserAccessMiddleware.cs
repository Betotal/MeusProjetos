using ERPModular.Shared.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace ERPModular.Web.Middleware;

public class UserAccessMiddleware
{
    private readonly RequestDelegate _next;

    public UserAccessMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, UserManager<ERPUser> userManager)
    {
        var principal = context.User;

        if (principal.Identity?.IsAuthenticated == true)
        {
            // Ignora se for página de Logout ou Erro para não criar loop infinito
            var path = context.Request.Path.Value?.ToLower() ?? "";
            if (!path.Contains("/logout") && !path.Contains("/restricted") && !path.Contains("/error"))
            {
                var user = await userManager.GetUserAsync(principal);
                
                if (user != null && !user.IsActive)
                {
                    context.Response.Redirect("/Account/Restricted");
                    return;
                }
            }
        }

        await _next(context);
    }
}
