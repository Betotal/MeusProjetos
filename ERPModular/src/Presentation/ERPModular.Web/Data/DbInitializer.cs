using ERPModular.Shared.Domain.Models;
using ERPModular.Shared.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ERPModular.Web.Data;

public static class DbInitializer
{
    public static async Task SeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ERPUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var context = scope.ServiceProvider.GetRequiredService<SharedDbContext>();

        // 1. Seed de Domínios
        if (!context.Dominios.Any())
        {
            context.Dominios.AddRange(
                new Dominio { Id = "confecao", Nome = "Segmento de Confecção", Descricao = "Indústria têxtil e vestuário" },
                new Dominio { Id = "farmacia", Nome = "Segmento de Farmácia", Descricao = "Farmácias e drogarias" },
                new Dominio { Id = "fabrica", Nome = "Segmento de Fábrica", Descricao = "Indústrias e Manufatura" }
            );
            await context.SaveChangesAsync();
        }

        // 2. Seed de Tenants (Empresas)
        if (!context.Tenants.Any())
        {
            context.Tenants.AddRange(
                new Tenant { Id = "tenant-001", NomeFantasia = "Confecção Estrela", DomainId = "confecao", Ativo = true },
                new Tenant { Id = "tenant-002", NomeFantasia = "Farmácia Central", DomainId = "farmacia", Ativo = true },
                new Tenant { Id = "tenant-003", NomeFantasia = "Fábrica de Móveis 1", DomainId = "fabrica", Ativo = true },
                new Tenant { Id = "tenant-004", NomeFantasia = "Fábrica de Peças 2", DomainId = "fabrica", Ativo = true }
            );
            await context.SaveChangesAsync();
        }

        // 3. Seed de Licenças (Cenários de Teste)
        if (!context.Licencas.Any())
        {
            var hoje = DateTime.UtcNow;
            context.Licencas.AddRange(
                // Ativas
                new Licenca { Id = Guid.NewGuid(), TenantId = "tenant-001", DomainId = "confecao", Ativa = true, InicioValidade = hoje.AddMonths(-1) },
                new Licenca { Id = Guid.NewGuid(), TenantId = "tenant-002", DomainId = "farmacia", Ativa = true, InicioValidade = hoje.AddMonths(-1) },
                
                // Fábrica 1: Expirada há 10 dias (Bloqueio total)
                new Licenca { Id = Guid.NewGuid(), TenantId = "tenant-003", DomainId = "fabrica", Ativa = true, InicioValidade = hoje.AddMonths(-1), FimValidade = hoje.AddDays(-10) },
                
                // Fábrica 2: Expirada há 1 dia (Carência/Alerta)
                new Licenca { Id = Guid.NewGuid(), TenantId = "tenant-004", DomainId = "fabrica", Ativa = true, InicioValidade = hoje.AddMonths(-1), FimValidade = hoje.AddDays(-1) }
            );
            await context.SaveChangesAsync();
        }

        // 4. Seed de Roles
        string[] roles = ["SuperAdmin", "Owner", "Admin", "User"];
        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // 5. Seed de Usuários
        
        // --- SUPER ADMIN ---
        var superAdmin = await userManager.FindByEmailAsync("dono@erp.com");
        if (superAdmin == null)
        {
            superAdmin = new ERPUser
            {
                UserName = "dono@erp.com", Email = "dono@erp.com", FullName = "Proprietário do Sistema",
                DisplayName = "Administrador", IsActive = true, EmailConfirmed = true, InvitationAccepted = true
            };
            await userManager.CreateAsync(superAdmin, "Super@123!");
            await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
        }

        // --- USUÁRIOS POR TENANT (LOOP) ---
        var allTenants = await context.Tenants.ToListAsync();
        foreach (var tenant in allTenants)
        {
            // 0. Dono (Owner - Função: Diretor, Nível: Total)
            await CreateUserIfNotExists(userManager, 
                $"dono.{tenant.Id}@teste.com", "Dono", "Owner", tenant, "Diretor", "Total");

            // 1. Administrador (Admin - Função: Gerente, Nível: Total)
            await CreateUserIfNotExists(userManager, 
                $"admin.{tenant.Id}@teste.com", "Administrador", "Admin", tenant, "Gerente", "Total");

            // 2. Vendedor (User - Função: Vendedor, Nível: Operacional)
            await CreateUserIfNotExists(userManager, 
                $"vendedor.{tenant.Id}@teste.com", "Vendedor", "User", tenant, "Vendedor", "Operacional");

            // 3. Consulta (User - Função: Auxiliar, Nível: Consulta)
            await CreateUserIfNotExists(userManager, 
                $"consulta.{tenant.Id}@teste.com", "Consulta", "User", tenant, "Auxiliar", "Consulta");
        }
    }

    private static async Task CreateUserIfNotExists(UserManager<ERPUser> userManager, string email, string namePrefix, string role, Tenant tenant, string funcao, string nivelAcesso)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            user = new ERPUser
            {
                UserName = email, Email = email, FullName = $"{namePrefix} - {tenant.NomeFantasia}",
                DisplayName = namePrefix, IsActive = true, DomainId = tenant.DomainId, TenantId = tenant.Id, EmpresaId = "matriz", 
                Funcao = funcao, NivelAcesso = nivelAcesso,
                EmailConfirmed = true, InvitationAccepted = true
            };
            await userManager.CreateAsync(user, "Teste@123!");
            await userManager.AddToRoleAsync(user, role);
        }
    }
}
