using System.Collections.Generic;
using Saloon.Domain.Entities;
using Saloon.Domain.Interfaces;

namespace Saloon.Application.Services
{
    public class DescontoServ
    {
        private readonly IDescontoRepo _descontoRepo;

        public DescontoServ(IDescontoRepo descontoRepo)
        {
            _descontoRepo = descontoRepo;
        }

        public DescontoEnty? Cadastrar(int idCliente, int porcentagem)
        {
            var desconto = new DescontoEnty
            {
                IdCliente = idCliente,
                PorcentagemDesconto = porcentagem
            };

            int id = _descontoRepo.Cadastrar(desconto);
            return _descontoRepo.ConsultarPorId(id);
        }

        public List<DescontoEnty> Listar()
        {
            return _descontoRepo.Listar();
        }

        public bool Deletar(int id)
        {
            return _descontoRepo.Deletar(id);
        }
    }
}