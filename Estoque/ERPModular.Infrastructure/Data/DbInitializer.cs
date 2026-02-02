using ERPModular.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ERPModular.Infrastructure.Data;

public static class DbInitializer
{
    public static void Seed(ERPModularDbContext context)
    {
        // Garante que o banco existe
        context.Database.EnsureCreated();

        // 1. Criar o Tenant de Teste (ID fixo que defini no TenantProvider)
        var defaultTenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        if (!context.Tenants.Any(t => t.Id == defaultTenantId))
        {
            var tenant = new Tenant
            {
                Id = defaultTenantId,
                Domain = "Confeccao",
                TenantId = defaultTenantId,
                CompanyId = defaultTenantId, // Matriz inicial
                Name = "BeTotal - Empresa Teste",
                Document = "123.456.789-00",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                IsTrial = true,
                SubscriptionEndDate = DateTime.UtcNow.AddDays(7)
            };

            context.Tenants.Add(tenant);
        }

        // 2. Criar Módulos Iniciais
        if (!context.Modules.Any())
        {
            context.Modules.AddRange(
                new Module { Name = "Estoque", Description = "Gestão de Insumos e Produtos", Icon = "inventory", Domain = "Confeccao", TenantId = defaultTenantId, CompanyId = defaultTenantId },
                new Module { Name = "Compras", Description = "Ordens de Compra e Fornecedores", Icon = "shopping_cart", Domain = "Confeccao", TenantId = defaultTenantId, CompanyId = defaultTenantId }
            );
        }

        context.SaveChanges();
    }
}
