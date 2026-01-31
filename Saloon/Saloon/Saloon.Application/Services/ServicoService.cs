using System.Collections.Generic;
using Saloon.Domain.Entities;
using Saloon.Domain.Interfaces;
using System;

namespace Saloon.Application.Services
{
    public class ServicoServ
    {
        private readonly IServicoRepo _servicoRepo;

        public ServicoServ(IServicoRepo servicoRepo)
        {
            _servicoRepo = servicoRepo;
        }

        public ServicoEnty? Cadastrar(string nome, decimal preco, TimeOnly tempo, TimeOnly encaixe)
        {
            var servico = new ServicoEnty
            {
                Nome = nome,
                PrecoInicial = preco,
                TempoMedio = tempo,
                TempoDeEncaixe = encaixe,
                Situacao = Saloon.Domain.Enums.EnumSituacao.Ativo
            };

            int id = _servicoRepo.Cadastrar(servico);
            return _servicoRepo.ConsultarPorId(id);
        }

        public List<ServicoEnty> Listar()
        {
            return _servicoRepo.Listar();
        }

        public bool Deletar(int id)
        {
            return _servicoRepo.Deletar(id);
        }
    }
}