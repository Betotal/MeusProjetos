using ERPModular.Shared.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ERPModular.Shared.Infrastructure.Persistence;

/// <summary>
/// DbContext responsável pelas tabelas globais do sistema (Usuários, Licenças, Domínios).
/// Utiliza o schema 'shared' para isolamento das tabelas de domínio.
/// </summary>
public class SharedDbContext : IdentityDbContext<ERPUser>
{
    public SharedDbContext(DbContextOptions<SharedDbContext> options) : base(options)
    {
    }

    public DbSet<Dominio> Dominios { get; set; }
    public DbSet<Licenca> Licencas { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<AdminAuditLog> AdminAuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define o schema global para todas as tabelas deste contexto
        modelBuilder.HasDefaultSchema("shared");

        // Configurações adicionais
        modelBuilder.Entity<Dominio>().ToTable("Dominios");
        modelBuilder.Entity<Licenca>().ToTable("Licencas");
        modelBuilder.Entity<Tenant>().ToTable("Tenants");
        modelBuilder.Entity<AdminAuditLog>().ToTable("AdminAuditLogs");
    }
}
