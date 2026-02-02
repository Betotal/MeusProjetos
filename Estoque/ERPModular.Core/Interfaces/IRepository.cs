using System.Linq.Expressions;

namespace ERPModular.Core.Interfaces;

/// <summary>
/// Interface genérica para o padrão Repositório.
/// No COBOL, seria como definir os comandos padrão de leitura/escrita para qualquer arquivo.
/// </summary>
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
