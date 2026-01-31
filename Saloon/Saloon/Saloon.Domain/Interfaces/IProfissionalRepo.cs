using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saloon.Domain.Entities;

namespace Saloon.Domain.Interfaces
{
    public interface IProfissionalRepo
    {
        int Cadastrar(ProfissionalEnty profissional);
        List<ProfissionalEnty> Listar();
        bool Deletar(int id);
        ProfissionalEnty? ConsultarPorId(int id);
    }
}