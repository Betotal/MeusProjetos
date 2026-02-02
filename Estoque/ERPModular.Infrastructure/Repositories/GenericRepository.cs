using ERPModular.Core.Interfaces;
using ERPModular.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ERPModular.Infrastructure.Repositories;

/// <summary>
/// Implementação genérica do Repositório.
/// Aproveita os filtros globais definidos no ERPModularDbContext para garantir o Multi-tenancy.
/// </summary>
public class GenericRepository<T> : IRepository<T> where T : class
{
    protected readonly ERPModularDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(ERPModularDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}
