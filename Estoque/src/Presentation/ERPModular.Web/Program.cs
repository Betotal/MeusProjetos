using ERPModular.Web.Components;
using ERPModular.Shared.Infrastructure.Persistence;
using ERPModular.Shared.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ERPModular.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRazorPages();

// ========================================
// CONFIGURAÇÃO DE SERVIÇOS COMPARTILHADOS
// ========================================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// 1. Registro do SharedDbContext (Identity e Tabelas Globais)
builder.Services.AddDbContext<SharedDbContext>(options =>
    options.UseNpgsql(connectionString));

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

// Registrar Cascada de AuthenticationState
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

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorPages();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Executa migrações automáticas e o Seed de dados
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SharedDbContext>();
    await dbContext.Database.MigrateAsync();
    await DbInitializer.SeedData(app.Services);
}

app.Run();
