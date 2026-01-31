using System.Collections.Generic;
using Saloon.Domain.Entities;
using Saloon.Domain.Interfaces;
using System;

namespace Saloon.Application.Services
{
    public class HorarioServ
    {
        private readonly IHorarioRepo _horarioRepo;

        public HorarioServ(IHorarioRepo horarioRepo)
        {
            _horarioRepo = horarioRepo;
        }

        public HorarioEnty? Cadastrar(TimeOnly inicio, TimeOnly fim)
        {
            var horario = new HorarioEnty
            {
                HorarioInicial = inicio,
                HorarioFinal = fim
            };

            int id = _horarioRepo.Cadastrar(horario);
            return _horarioRepo.ConsultarPorId(id);
        }

        public List<HorarioEnty> Listar()
        {
            return _horarioRepo.Listar();
        }

        public bool Deletar(int id)
        {
            return _horarioRepo.Deletar(id);
        }
    }
}