using System.Collections.Generic;
using Saloon.Domain.Entities;
using Saloon.Domain.Interfaces;

namespace Saloon.Application.Services
{
    public class ProfissionalServ
    {
        private readonly IProfissionalRepo _profissionalRepo;

        public ProfissionalServ(IProfissionalRepo profissionalRepo)
        {
            _profissionalRepo = profissionalRepo;
        }

        public ProfissionalEnty? Cadastrar(int idCliente, decimal salarioFixo, int comissao)
        {
            var profissional = new ProfissionalEnty
            {
                IdCliente = idCliente,
                SalarioFixo = salarioFixo,
                PorcentagemComissao = comissao
            };

            int id = _profissionalRepo.Cadastrar(profissional);
            return _profissionalRepo.ConsultarPorId(id);
        }

        public List<ProfissionalEnty> Listar()
        {
            return _profissionalRepo.Listar();
        }

        public bool Deletar(int id)
        {
            return _profissionalRepo.Deletar(id);
        }
    }
}