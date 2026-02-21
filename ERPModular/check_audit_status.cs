using ERPModular.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SharedDbContext>(options => options.UseNpgsql(connectionString));

using var host = builder.Build();
using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SharedDbContext>();
    
    Console.WriteLine("--- VERIFICANDO DATABASE ---");
    
    try 
    {
        var usersCount = await db.Users.CountAsync();
        Console.WriteLine($"Usuários no banco: {usersCount}");
        
        var auditCount = await db.AdminAuditLogs.CountAsync();
        Console.WriteLine($"Logs de Auditoria: {auditCount}");
        
        if (auditCount > 0)
        {
            var lastLog = await db.AdminAuditLogs.OrderByDescending(l => l.DataHora).FirstOrDefaultAsync();
            Console.WriteLine($"Último Log: {lastLog?.Acao} em {lastLog?.DataHora}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"ERRO AO ACESSAR DADOS: {ex.Message}");
    }
    
    Console.WriteLine("----------------------------");
}
