using Xunit;
using Saloon.Application.Services;
using Saloon.Domain.Entities;
using Saloon.Domain.Interfaces;
using Saloon.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Saloon.Tests
{
    // Fake implementation to avoid Moq dependency for a quick proof
    public class FakeAgendaRepo : IAgendaRepo
    {
        private List<AgendaEnty> _data = new List<AgendaEnty>();
        private int _nextId = 1;

        public int Cadastrar(AgendaEnty agenda)
        {
            agenda.ID = _nextId++;
            _data.Add(agenda);
            return agenda.ID;
        }

        public List<AgendaEnty> Listar() => _data;

        public bool Deletar(int id)
        {
            var item = _data.FirstOrDefault(x => x.ID == id);
            if (item == null) return false;
            _data.Remove(item);
            return true;
        }

        public AgendaEnty? ConsultarPorId(int id) => _data.FirstOrDefault(x => x.ID == id);
    }

    public class AgendamentoTest
    {
        [Fact]
        public void Agendar_Deve_Criar_Agendamento_Com_Sucesso()
        {
            // Arrange
            var fakeRepo = new FakeAgendaRepo();
            var service = new AgendamentoServ(fakeRepo);
            var data = new DateOnly(2025, 01, 10);
            var hora = new TimeOnly(10, 0);

            // Act
            var result = service.Agendar(data, hora, 1, 1, 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.ID);
            Assert.Equal(data, result.AgendaData);
            Assert.Equal(hora, result.AgendaHora);
            Assert.Equal(EnumSituacaoAgenda.Agendado, result.AgendaSituacao);
        }

        [Fact]
        public void Cancelar_Deve_Remover_Agendamento_Existente()
        {
            // Arrange
            var fakeRepo = new FakeAgendaRepo();
            var service = new AgendamentoServ(fakeRepo);
            var result = service.Agendar(DateOnly.FromDateTime(DateTime.Now), TimeOnly.FromDateTime(DateTime.Now), 1, 1, 1);
            int id = result.ID;

            // Act
            var cancelado = service.Cancelar(id);

            // Assert
            Assert.True(cancelado);
            Assert.Null(service.Consultar(id));
        }
    }
}
