using System;
using Saloon.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Saloon.ConsoleApp.InterfaceUsuario
{
    public class Agendamento
    {
        private static string opcao = "";

        public static void MenuAgendamento()
        {
            do
            {
                Console.SetCursorPosition(43, 14);
                Console.Write("1 - Novo Agendamento");
                Console.SetCursorPosition(43,15);
                Console.Write("2 - Consultar Agenda");
                Console.SetCursorPosition(43,16);
                Console.Write("3 - Cancelar Agendamento");
                Console.SetCursorPosition(43,18);
                Console.Write("X - Sair");
                Global.Menu2 = Validacao.ValidaMenu2();

            } while (Global.Menu2 != "1" && Global.Menu2 != "2" && Global.Menu2 != "3" && Global.Menu2 != "X");

            using var scope = Program.ServiceProvider.CreateScope();
            var agendamentoServ = scope.ServiceProvider.GetRequiredService<AgendamentoServ>();
            var clienteServ = scope.ServiceProvider.GetRequiredService<ClienteServ>();
            var profissionalServ = scope.ServiceProvider.GetRequiredService<ProfissionalServ>();
            var servicoServ = scope.ServiceProvider.GetRequiredService<ServicoServ>();

            switch (Global.Menu2)
            {
                case "1":
                    Cadastro(agendamentoServ, clienteServ, profissionalServ, servicoServ);
                    break;
                case "2":
                    Consulta(agendamentoServ, clienteServ, profissionalServ, servicoServ);
                    break;
                case "3":
                    Exclusao(agendamentoServ);
                    break;
            }
        }

        private static void Cadastro(AgendamentoServ agendamentoServ, ClienteServ clienteServ, ProfissionalServ profissionalServ, ServicoServ servicoServ)
        {
            Tela.LimpaTela();
            Console.WriteLine("=== NOVO AGENDAMENTO ===");
            
            Console.Write("Data (dd/mm/aaaa): ");
            DateOnly.TryParse(Console.ReadLine(), out DateOnly data);
            
            Console.Write("Hora (hh:mm): ");
            TimeOnly.TryParse(Console.ReadLine(), out TimeOnly hora);

            // Listar Clientes
            Console.WriteLine("\n--- Selecione o Cliente ---");
            var clientes = clienteServ.ListarClientes();
            foreach(var c in clientes) Console.WriteLine($"ID: {c.ID} - {c.Nome}");
            Console.Write("ID Cliente: ");
            int.TryParse(Console.ReadLine(), out int idCliente);

            // Listar Profissionais
            Console.WriteLine("\n--- Selecione o Profissional ---");
            var profissionais = profissionalServ.Listar();
            foreach(var p in profissionais) {
                var cli = clienteServ.ConsultarId(p.IdCliente);
                Console.WriteLine($"ID: {p.ID} - {cli?.Nome}");
            }
            Console.Write("ID Profissional: ");
            int.TryParse(Console.ReadLine(), out int idProfissional);

            // Listar Servicos
            Console.WriteLine("\n--- Selecione o Serviço ---");
            var servicos = servicoServ.Listar();
            foreach(var s in servicos) Console.WriteLine($"ID: {s.ID} - {s.Nome} ({s.PrecoInicial:C})");
            Console.Write("ID Serviço: ");
            int.TryParse(Console.ReadLine(), out int idServico);

            agendamentoServ.Agendar(data, hora, idProfissional, idCliente, idServico);
            Console.WriteLine("\nAgendamento realizado com sucesso!");
            Console.ReadKey();
        }

        private static void Consulta(AgendamentoServ agendamentoServ, ClienteServ clienteServ, ProfissionalServ profissionalServ, ServicoServ servicoServ)
        {
            Tela.LimpaTela();
            Console.WriteLine("=== AGENDA COMPLETA ===");
            var lista = agendamentoServ.Listar();
            foreach(var item in lista)
            {
                var cliente = clienteServ.ConsultarId(item.IdCliente);
                var profissional = profissionalServ.Listar().FirstOrDefault(p => p.ID == item.IdProfissional);
                var nomeProf = profissional != null ? clienteServ.ConsultarId(profissional.IdCliente)?.Nome : "N/A";
                var servico = servicoServ.Listar().FirstOrDefault(s => s.ID == item.IdServico);

                Console.WriteLine($"{item.AgendaData} {item.AgendaHora} | Cliente: {cliente?.Nome} | Prof: {nomeProf} | Serv: {servico?.Nome}");
            }
            Console.ReadKey();
        }

        private static void Exclusao(AgendamentoServ agendamentoServ)
        {
            Tela.LimpaTela();
            Console.WriteLine("=== CANCELAR AGENDAMENTO ===");
            Console.Write("Digite o ID do Agendamento: ");
            if(int.TryParse(Console.ReadLine(), out int id))
            {
                if(agendamentoServ.Cancelar(id))
                    Console.WriteLine("Agendamento cancelado!");
                else
                    Console.WriteLine("Erro ao cancelar ou não encontrado.");
            }
            Console.ReadKey();
        }
    }
}