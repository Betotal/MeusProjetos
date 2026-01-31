using Saloon.Domain.Entities;

namespace Saloon.Domain.Interfaces
{
    public interface IHorarioRepo
    {
        int Cadastrar(HorarioEnty horario);
        List<HorarioEnty> Listar();
        bool Deletar(int id);
        HorarioEnty? ConsultarPorId(int id);
    }
}