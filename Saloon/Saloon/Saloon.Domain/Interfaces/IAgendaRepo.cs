using Saloon.Domain.Entities;

namespace Saloon.Domain.Interfaces
{
    public interface IAgendaRepo
    {
        int Cadastrar(AgendaEnty agenda);
        List<AgendaEnty> Listar();
        bool Deletar(int id);
        AgendaEnty? ConsultarPorId(int id);
    }
}