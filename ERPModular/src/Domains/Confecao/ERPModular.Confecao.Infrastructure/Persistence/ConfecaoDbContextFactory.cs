using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ERPModular.Confecao.Infrastructure.Persistence;
using ERPModular.Shared.Domain.Interfaces;
using ERPModular.Shared.Domain.Models;

namespace ERPModular.Confecao.Infrastructure.Persistence;

/// <summary>
/// Fábrica utilizada pelas ferramentas do EF Core (CLI) para criar o DbContext em tempo de design.
/// Isso resolve o problema de dependência do IExecutionContextAccessor (que não existe no CLI).
/// </summary>
public class ConfecaoDbContextFactory : IDesignTimeDbContextFactory<ConfecaoDbContext>
{
    public ConfecaoDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ConfecaoDbContext>();
        
        // Em tempo de design, usamos a string de conexão padrão (ajuste se necessário para seu ambiente)
        // Nota: O CLI do EF geralmente consegue ler do appsettings, mas aqui passamos uma base.
        optionsBuilder.UseNpgsql("Host=localhost;Database=erpmodular;Username=postgres;Password=password");

        return new ConfecaoDbContext(optionsBuilder.Options, new DesignTimeContextAccessor());
    }
}

/// <summary>
/// Acessor de contexto fake para tempo de design.
/// </summary>
internal class DesignTimeContextAccessor : IExecutionContextAccessor
{
    public ERPExecutionContext GetContext()
    {
        // Retorna um contexto padrão "confecao" para que a migração seja gerada no schema correto
        return new ERPExecutionContext("confecao", "design-tenant", "design-empresa", "design-user");
    }
}
