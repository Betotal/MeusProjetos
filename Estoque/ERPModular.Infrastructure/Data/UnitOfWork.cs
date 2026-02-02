using ERPModular.Core.Entities;
using ERPModular.Core.Interfaces;
using ERPModular.Infrastructure.Data;
using ERPModular.Infrastructure.Repositories;
using System.Collections;

namespace ERPModular.Infrastructure.Data;

/// <summary>
/// Implementação do Unit of Work.
/// Gerencia a criação de repositórios sob demanda e garante uma única transação no banco.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ERPModularDbContext _context;
    private Hashtable _repositories = new Hashtable();

    public UnitOfWork(ERPModularDbContext context)
    {
        _context = context;
    }

    public IRepository<T> Repository<T>() where T : BaseEntity
    {
        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
            _repositories.Add(type, repositoryInstance);
        }

        return (IRepository<T>)_repositories[type]!;
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
