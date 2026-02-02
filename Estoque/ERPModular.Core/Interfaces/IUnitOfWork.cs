using ERPModular.Core.Entities;

namespace ERPModular.Core.Interfaces;

/// <summary>
/// Interface para o padrão Unit of Work.
/// No COBOL, seria como o controle de COMMIT de uma transação que envolve vários arquivos.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IRepository<T> Repository<T>() where T : BaseEntity;
    Task<int> SaveAsync();
}
