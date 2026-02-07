using ERPModular.Shared.Domain.Models;
using ERPModular.Shared.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace ERPModular.Web.Data;

public static class DbInitializer
{
    public static async Task SeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ERPUser>>();
        var context = scope.ServiceProvider.GetRequiredService<SharedDbContext>();

        // 1. Seed de Domínios
        if (!context.Dominios.Any())
        {
            context.Dominios.AddRange(
                new Dominio { Id = "confeccao", Nome = "Segmento de Confecção", Descricao = "Módulo para indústria têxtil e vestuário" },
                new Dominio { Id = "farmacia", Nome = "Segmento de Farmácia", Descricao = "Módulo para farmácias e drogarias" }
            );
            await context.SaveChangesAsync();
        }

        // 2. Seed de Licenças (Vitalícias para teste)
        if (!context.Licencas.Any())
        {
            context.Licencas.AddRange(
                new Licenca { Id = Guid.NewGuid(), TenantId = "tenant-001", DomainId = "confeccao", Ativa = true, InicioValidade = DateTime.UtcNow },
                new Licenca { Id = Guid.NewGuid(), TenantId = "tenant-002", DomainId = "farmacia", Ativa = true, InicioValidade = DateTime.UtcNow }
            );
            await context.SaveChangesAsync();
        }

        // 3. Seed de Roles (Perfis)
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        string[] roles = ["Admin", "Vendedor"];
        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // 4. Seed de Usuários
        
        // Admim Confecção (Já existe, vamos apenas garantir o Role)
        var adminEmail = "admin@erp.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new ERPUser
            {
                UserName = adminEmail, Email = adminEmail, FullName = "Administrador Confecção",
                IsActive = true, DomainId = "confeccao", TenantId = "tenant-001", EmpresaId = "matriz", EmailConfirmed = true
            };
            await userManager.CreateAsync(adminUser, "Admin@123");
        }
        await userManager.AddToRoleAsync(adminUser, "Admin");

        // Vendedor Confecção (Novo usuário para testar níveis de acesso no MESMO Tenant)
        var vendedorEmail = "vendedor@confeccao.com";
        var vendedorUser = await userManager.FindByEmailAsync(vendedorEmail);
        if (vendedorUser == null)
        {
            vendedorUser = new ERPUser
            {
                UserName = vendedorEmail, Email = vendedorEmail, FullName = "Vendedor Confecção",
                IsActive = true, DomainId = "confeccao", TenantId = "tenant-001", EmpresaId = "matriz", EmailConfirmed = true
            };
            await userManager.CreateAsync(vendedorUser, "Vendedor@123");
        }
        await userManager.AddToRoleAsync(vendedorUser, "Vendedor");

        // Usuário Farmácia (Tenant Diferente)
        var farmaciaEmail = "user@farmacia.com";
        var farmaciaUser = await userManager.FindByEmailAsync(farmaciaEmail);
        if (farmaciaUser == null)
        {
            farmaciaUser = new ERPUser
            {
                UserName = farmaciaEmail, Email = farmaciaEmail, FullName = "Gerente Farmácia",
                IsActive = true, DomainId = "farmacia", TenantId = "tenant-002", EmpresaId = "filial-01", EmailConfirmed = true
            };
            await userManager.CreateAsync(farmaciaUser, "Farmacia@123");
        }
        await userManager.AddToRoleAsync(farmaciaUser, "Admin");
    }
}
