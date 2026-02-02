using ERPModular.Web.Components;
using ERPModular.Infrastructure.Data;
using ERPModular.Core.Interfaces;
using ERPModular.Web.Services;
using ERPModular.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazor.Services;

namespace ERPModular.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // 1. Configurar Banco de Dados (PostgreSQL)
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ERPModularDbContext>(options =>
            options.UseNpgsql(connectionString), ServiceLifetime.Transient);

        // 2. Registro do ITenantProvider (Mock inicial)
        builder.Services.AddScoped<ITenantProvider, TenantProvider>();

        // 3. Adicionar MudBlazor Services
        builder.Services.AddMudServices();

        // 4. Registrar Repositórios e Unit of Work (Como Transient para evitar concorrência no Blazor Server)
        builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        builder.Services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        var app = builder.Build();

        // 3. Executar o Seed (Popular o banco inicial)
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ERPModularDbContext>();
                DbInitializer.Seed(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Ocorreu um erro ao popular o banco de dados.");
            }
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
