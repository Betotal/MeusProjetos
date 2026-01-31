using Saloon.Domain.Entities;

namespace Saloon.Domain.Interfaces
{
    public interface ICargoRepo
    {
        int Cadastrar(CargoEnty cargo);
        List<CargoEnty> Listar();
        bool Deletar(int id);
        CargoEnty? ConsultarPorId(int id);
    }
}