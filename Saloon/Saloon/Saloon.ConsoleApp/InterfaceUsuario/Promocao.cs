using System;
using Saloon.Application.Services;
using Saloon.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Saloon.ConsoleApp.InterfaceUsuario
{
    public class Promocao
    {
        private static string opcao = "";

        public static void MenuPromocao()
        {
            do
            {
                Console.SetCursorPosition(43, 14);
                Console.Write("1 - Cadastro");
                Console.SetCursorPosition(43,15);
                Console.Write("2 - Consulta");
                Console.SetCursorPosition(43,16);
                Console.Write("3 - Exclusão");
                Console.SetCursorPosition(43,18);
                Console.Write("X - Sair");
                Global.Menu2 = Validacao.ValidaMenu2();

            } while (Global.Menu2 != "1" && Global.Menu2 != "2" && Global.Menu2 != "3" && Global.Menu2 != "X");

            using var scope = Program.ServiceProvider.CreateScope();
            var promocaoServ = scope.ServiceProvider.GetRequiredService<PromocaoServ>();
            var servicoServ = scope.ServiceProvider.GetRequiredService<ServicoServ>();

            switch (Global.Menu2)
            {
                case "1":
                    Cadastro(promocaoServ, servicoServ);
                    break;
                case "2":
                    Consulta(promocaoServ, servicoServ);
                    break;
                case "3":
                    Exclusao(promocaoServ);
                    break;
            }
        }

        private static void Cadastro(PromocaoServ promocaoServ, ServicoServ servicoServ)
        {
            Tela.LimpaTela();
            Console.WriteLine("=== CADASTRO DE PROMOÇÃO ===");
            
            Console.Write("Nome da Promoção: ");
            string nome = Console.ReadLine();

            Console.Write("Data Inicial (dd/mm/aaaa): ");
            DateOnly.TryParse(Console.ReadLine(), out DateOnly inicio);
            
            Console.Write("Data Final (dd/mm/aaaa): ");
            DateOnly.TryParse(Console.ReadLine(), out DateOnly fim);

            // Listar Servicos
            var servicos = servicoServ.Listar();
            foreach(var s in servicos) {
                Console.WriteLine($"ID: {s.ID} - Nome: {s.Nome}");
            }
            Console.Write("ID Serviço: ");
            int.TryParse(Console.ReadLine(), out int idServico);

            Console.Write("Qtd Serviço: ");
            int.TryParse(Console.ReadLine(), out int qtd);

            Console.Write("Porcentagem Desconto: ");
            int.TryParse(Console.ReadLine(), out int porcentagem);

            Console.Write("Dia Inicio (0-Dom, 6-Sab): ");
            int.TryParse(Console.ReadLine(), out int diaIni);
            
            Console.Write("Dia Fim (0-Dom, 6-Sab): ");
            int.TryParse(Console.ReadLine(), out int diaFim);

            Console.Write("Quem Paga (0-Salao, 1-Profissional, 2-Ambos): ");
            int.TryParse(Console.ReadLine(), out int quemPaga);

            promocaoServ.Cadastrar(nome, inicio, fim, idServico, qtd, porcentagem, (EnumDiaSemana)diaIni, (EnumDiaSemana)diaFim, (EnumQuemPaga)quemPaga);
            Console.WriteLine("Promoção cadastrada com sucesso!");
            Console.ReadKey();
        }

        private static void Consulta(PromocaoServ promocaoServ, ServicoServ servicoServ)
        {
            Tela.LimpaTela();
            Console.WriteLine("=== LISTA DE PROMOÇÕES ===");
            var lista = promocaoServ.Listar();
            foreach(var item in lista)
            {
                var servico = servicoServ.Listar().FirstOrDefault(s => s.ID == item.IdServico); // Needs optimization later or ConsultarPorId exposed
                string nomeServico = servico != null ? servico.Nome : "N/A";

                Console.WriteLine($"ID: {item.ID} | Nome: {item.Nome} | Serviço: {nomeServico} | Desc: {item.PorcentagemDesconto}%");
            }
            Console.ReadKey();
        }

        private static void Exclusao(PromocaoServ promocaoServ)
        {
            Tela.LimpaTela();
            Console.WriteLine("=== EXCLUSÃO DE PROMOÇÃO ===");
            Console.Write("Digite o ID da Promoção: ");
            if(int.TryParse(Console.ReadLine(), out int id))
            {
                if(promocaoServ.Deletar(id))
                    Console.WriteLine("Promoção removida!");
                else
                    Console.WriteLine("Erro ao remover ou não encontrado.");
            }
            Console.ReadKey();
        }
        
    }
}