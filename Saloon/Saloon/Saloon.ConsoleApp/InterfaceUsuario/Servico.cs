using System;
using Saloon.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saloon.ConsoleApp.InterfaceUsuario
{
    public class Servico
    {
        private static string opcao = "";

        public static void MenuServico()
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
            var servicoServ = scope.ServiceProvider.GetRequiredService<ServicoServ>();

            switch (Global.Menu2)
            {
                case "1":
                    Cadastro(servicoServ);
                    break;
                case "2":
                    Consulta(servicoServ);
                    break;
                case "3":
                    Exclusao(servicoServ);
                    break;
            }
        }

        private static void Cadastro(ServicoServ servicoServ)
        {
            Tela.LimpaTela();
            Console.WriteLine("=== CADASTRO DE SERVIÇO ===");
            
            Console.Write("Nome do Serviço: ");
            string nome = Console.ReadLine();

            Console.Write("Preço Inicial: ");
            decimal.TryParse(Console.ReadLine(), out decimal preco);

            Console.Write("Tempo Médio (hh:mm): ");
            TimeOnly.TryParse(Console.ReadLine(), out TimeOnly tempo);

            Console.Write("Tempo de Encaixe (hh:mm): ");
            TimeOnly.TryParse(Console.ReadLine(), out TimeOnly encaixe);

            servicoServ.Cadastrar(nome, preco, tempo, encaixe);
            Console.WriteLine("Serviço cadastrado com sucesso!");
            Console.ReadKey();
        }

        private static void Consulta(ServicoServ servicoServ)
        {
            Tela.LimpaTela();
            Console.WriteLine("=== LISTA DE SERVIÇOS ===");
            var lista = servicoServ.Listar();
            foreach(var item in lista)
            {
                Console.WriteLine($"ID: {item.ID} | Nome: {item.Nome} | Preço: {item.PrecoInicial} | Tempo: {item.TempoMedio}");
            }
            Console.ReadKey();
        }

        private static void Exclusao(ServicoServ servicoServ)
        {
            Tela.LimpaTela();
            Console.WriteLine("=== EXCLUSÃO DE SERVIÇO ===");
            Console.Write("Digite o ID do Serviço: ");
            if(int.TryParse(Console.ReadLine(), out int id))
            {
                if(servicoServ.Deletar(id))
                    Console.WriteLine("Serviço removido!");
                else
                    Console.WriteLine("Erro ao remover ou não encontrado.");
            }
            Console.ReadKey();
        }
    }
}