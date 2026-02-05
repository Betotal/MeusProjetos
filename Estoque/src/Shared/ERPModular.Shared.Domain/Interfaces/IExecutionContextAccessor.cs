using ERPModular.Shared.Domain.Models;

namespace ERPModular.Shared.Domain.Interfaces;

/// <summary>
/// Interface para acessar o contexto de execução atual em qualquer camada.
/// </summary>
public interface IExecutionContextAccessor
{
    ExecutionContext GetContext();
}
