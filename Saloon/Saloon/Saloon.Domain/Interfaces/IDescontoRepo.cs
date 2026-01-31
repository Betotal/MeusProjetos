using Saloon.Domain.Entities;

namespace Saloon.Domain.Interfaces
{
    public interface IDescontoRepo
    {
        int Cadastrar(DescontoEnty desconto);
        List<DescontoEnty> Listar();
        bool Deletar(int id);
        DescontoEnty? ConsultarPorId(int id);
    }
}