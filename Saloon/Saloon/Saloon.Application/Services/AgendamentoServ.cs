using System.Collections.Generic;
using Saloon.Domain.Entities;
using Saloon.Domain.Interfaces;
using Saloon.Domain.Enums;
using System;

namespace Saloon.Application.Services
{
    public class AgendamentoServ
    {
        private readonly IAgendaRepo _agendaRepo;

        public AgendamentoServ(IAgendaRepo agendaRepo)
        {
            _agendaRepo = agendaRepo;
        }

        public AgendaEnty? Agendar(DateOnly data, TimeOnly hora, int idProfissional, int idCliente, int idServico, int idClientePromocao = 0)
        {
            var agendamento = new AgendaEnty
            {
                AgendaData = data,
                AgendaHora = hora,
                DiaSemana = (EnumDiaSemana)data.DayOfWeek,
                IdProfissional = idProfissional,
                IdCliente = idCliente,
                IdServico = idServico,
                IdClientePromocao = idClientePromocao,
                AgendaSituacao = EnumSituacaoAgenda.Agendado
            };

            int id = _agendaRepo.Cadastrar(agendamento);
            return _agendaRepo.ConsultarPorId(id);
        }

        public List<AgendaEnty> Listar()
        {
            return _agendaRepo.Listar();
        }

        public bool Cancelar(int id)
        {
            var agendamento = _agendaRepo.ConsultarPorId(id);
            if (agendamento != null)
            {
                // In a real scenario, we would update the status, but for now we follow the "Deletar" pattern of other menus or implement status update if repo supports it.
                // Since our generic repo pattern so far uses Deletar, I'll stick to it for consistency unless I add AlterarSituacao.
                return _agendaRepo.Deletar(id);
            }
            return false;
        }

        public AgendaEnty? Consultar(int id)
        {
            return _agendaRepo.ConsultarPorId(id);
        }
    }
}
