using System.Collections.Generic;
using Saloon.Domain.Entities;
using Saloon.Domain.Interfaces;
using Saloon.Domain.Enums;
using System;

namespace Saloon.Application.Services
{
    public class PromocaoServ
    {
        private readonly IPromocaoRepo _promocaoRepo;

        public PromocaoServ(IPromocaoRepo promocaoRepo)
        {
            _promocaoRepo = promocaoRepo;
        }

        public PromocaoEnty? Cadastrar(string nome, DateOnly inicio, DateOnly fim, int idServico, int qtdServico, int porcentagem, EnumDiaSemana diaInicio, EnumDiaSemana diaFim, EnumQuemPaga quemPaga)
        {
            var promocao = new PromocaoEnty
            {
                Nome = nome,
                PromocaoInicial = inicio,
                PromocaoFinal = fim,
                IdServico = idServico,
                QuantidadeServico = qtdServico,
                PorcentagemDesconto = porcentagem,
                DiaSemanaInicial = diaInicio,
                DiaSemanaFinal = diaFim,
                QuemPaga = quemPaga
            };

            int id = _promocaoRepo.Cadastrar(promocao);
            return _promocaoRepo.ConsultarPorId(id);
        }

        public List<PromocaoEnty> Listar()
        {
            return _promocaoRepo.Listar();
        }

        public bool Deletar(int id)
        {
            return _promocaoRepo.Deletar(id);
        }
    }
}