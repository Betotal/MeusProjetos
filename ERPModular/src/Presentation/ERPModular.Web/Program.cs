using ERPModular.Web.Components;
using ERPModular.Shared.Infrastructure.Persistence;
using ERPModular.Shared.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ERPModular.Web.Data;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<ERPModular.Web.Services.SaaSControlService>();

builder.Services.AddRazorPages();

// context accessor para ler Claims
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ERPModular.Shared.Domain.Interfaces.IExecutionContextAccessor, ERPModular.Shared.Infrastructure.Context.ExecutionContextAccessor>();
builder.Services.AddTransient<ERPModular.Shared.Infrastructure.Persistence.Interceptors.AuditInterceptor>();
builder.Services.AddTransient<ERPModular.Shared.Domain.Interfaces.IEmailSender, ERPModular.Shared.Infrastructure.Services.DebugEmailSender>();

// ========================================
// CONFIGURAÇÃO DE SERVIÇOS COMPARTILHADOS
// ========================================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// 1. Registro do SharedDbContext (Identity e Tabelas Globais)
builder.Services.AddDbContextFactory<SharedDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddScoped(p => p.GetRequiredService<IDbContextFactory<SharedDbContext>>().CreateDbContext());

// 2. Registro do ConfecaoDbContext (Domínio Específico)
builder.Services.AddDbContextFactory<ERPModular.Confecao.Infrastructure.Persistence.ConfecaoDbContext>((sp, options) =>
{
    options.UseNpgsql(connectionString);
    var auditInterceptor = sp.GetRequiredService<ERPModular.Shared.Infrastructure.Persistence.Interceptors.AuditInterceptor>();
    options.AddInterceptors(auditInterceptor);
});
builder.Services.AddScoped(p => p.GetRequiredService<IDbContextFactory<ERPModular.Confecao.Infrastructure.Persistence.ConfecaoDbContext>>().CreateDbContext());

// Serviços de Domínio - Confecção
builder.Services.AddScoped<ERPModular.Confecao.Application.Interfaces.IEstoqueService, ERPModular.Confecao.Infrastructure.Services.EstoqueService>();
builder.Services.AddScoped<ERPModular.Confecao.Application.Interfaces.IPrecoService, ERPModular.Confecao.Infrastructure.Services.PrecoService>();

// 2. Configuração do ASP.NET Core Identity
builder.Services.AddIdentity<ERPUser, IdentityRole>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<SharedDbContext>()
.AddClaimsPrincipalFactory<ERPUserClaimsPrincipalFactory>()
.AddDefaultTokenProviders();

// 3. Configuração de Cookies para Blazor Server
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.Cookie.Name = "ERPModular.Auth";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.SlidingExpiration = true;
});

// 4. Provedor de Estado de Autenticação Revalidável (Monitoramento de IsActive)
builder.Services.AddScoped<AuthenticationStateProvider, ERPModular.Web.Services.RevalidatingIdentityAuthenticationStateProvider<ERPUser>>();
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

// Importante: UseAuthentication antes de UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

// Middleware de Validação de Licença (SaaS)
app.UseMiddleware<ERPModular.Web.Middleware.LicenseValidationMiddleware>();

// Middleware de Contexto (Garantia de Identidade Multi-Tenant)
app.UseMiddleware<ERPModular.Web.Middleware.ExecutionContextMiddleware>();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorPages();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Executa migrações automáticas e o Seed de dados
using (var scope = app.Services.CreateScope())
{
    var sharedContext = scope.ServiceProvider.GetRequiredService<SharedDbContext>();
    await sharedContext.Database.MigrateAsync();
    await DbInitializer.SeedData(app.Services);
}

app.Run();
