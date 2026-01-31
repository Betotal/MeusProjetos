using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saloon.Domain.Entities;

namespace Saloon.Domain.Interfaces
{
    public interface IServicoRepo
    {
        int Cadastrar(ServicoEnty servico);
        List<ServicoEnty> Listar();
        bool Deletar(int id);
        ServicoEnty? ConsultarPorId(int id);
    }
}