using Saloon.Domain.Entities;

namespace Saloon.Domain.Interfaces
{
    public interface IPromocaoRepo
    {
        int Cadastrar(PromocaoEnty promocao);
        List<PromocaoEnty> Listar();
        bool Deletar(int id);
        PromocaoEnty? ConsultarPorId(int id);
    }
}