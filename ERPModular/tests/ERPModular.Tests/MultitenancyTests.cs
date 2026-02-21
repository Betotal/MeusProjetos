using ERPModular.Shared.Domain.Interfaces;
using ERPModular.Shared.Domain.Models;
using ERPModular.Shared.Domain.Primitives;
using ERPModular.Shared.Infrastructure.Persistence;
using ERPModular.Shared.Infrastructure.Persistence.Interceptors;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ERPModular.Tests;

public class MultitenancyTests
{
    private class TestEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
    }

    private class TestDbContext : BaseDbContext
    {
        public TestDbContext(
            DbContextOptions<TestDbContext> options,
            IExecutionContextAccessor contextAccessor,
            TenantInterceptor tenantInterceptor) 
            : base(options, contextAccessor, tenantInterceptor)
        {
        }

        public DbSet<TestEntity> TestEntities { get; set; }
    }

    [Fact]
    public async Task TenantInterceptor_ShouldPopulateAuditFields_WhenAddingEntity()
    {
        // Arrange
        var contextAccessorMock = new Mock<IExecutionContextAccessor>();
        var executionContext = new ERPExecutionContext(
            "confeccao",
            "tenant-001",
            "empresa-01",
            "user-123"
        );
        contextAccessorMock.Setup(x => x.GetContext()).Returns(executionContext);

        var interceptor = new TenantInterceptor(contextAccessorMock.Object);
        
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: "AuditTest")
            .Options;

        using var dbContext = new TestDbContext(options, contextAccessorMock.Object, interceptor);
        var entity = new TestEntity { Name = "Test" };

        // Act
        dbContext.TestEntities.Add(entity);
        await dbContext.SaveChangesAsync();

        // Assert
        entity.TenantId.Should().Be("tenant-001");
        entity.EmpresaId.Should().Be("empresa-01");
        entity.CreatedBy.Should().Be("user-123");
        entity.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async Task GlobalQueryFilter_ShouldFilterByTenantId()
    {
        // Arrange
        var dbName = Guid.NewGuid().ToString();
        var contextAccessorMock = new Mock<IExecutionContextAccessor>();
        var executionContext = new ERPExecutionContext("confeccao", "tenant-001", "empresa-01", "user-123");
        contextAccessorMock.Setup(x => x.GetContext()).Returns(executionContext);

        // Seed: Bypass interceptor to allow different TenantIds
        var optionsSeed = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        using (var dbContext = new TestDbContext(optionsSeed, contextAccessorMock.Object, null!)) // No interceptor for seed
        {
            var e1 = new TestEntity { Name = "T1", TenantId = "tenant-001" };
            var e2 = new TestEntity { Name = "T2", TenantId = "tenant-002" };
            
            dbContext.TestEntities.AddRange(e1, e2);
            await dbContext.SaveChangesAsync();
        }

        // Act
        var interceptor = new TenantInterceptor(contextAccessorMock.Object);
        var optionsQuery = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        using (var dbContext = new TestDbContext(optionsQuery, contextAccessorMock.Object, interceptor))
        {
            var result = await dbContext.TestEntities.ToListAsync();

            // Assert
            result.Should().HaveCount(1);
            result.First().TenantId.Should().Be("tenant-001");
        }
    }
}
