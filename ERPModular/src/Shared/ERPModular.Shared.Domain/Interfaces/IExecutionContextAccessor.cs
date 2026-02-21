using ERPModular.Shared.Domain.Models;

namespace ERPModular.Shared.Domain.Interfaces;

/// <summary>
/// Interface para acessar o contexto de execução atual.
/// </summary>
public interface IExecutionContextAccessor
{
    ERPExecutionContext GetContext();
}
