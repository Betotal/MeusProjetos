using System;
using Saloon.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saloon.ConsoleApp.InterfaceUsuario
{
    public class Cargo
    {
        private static string opcao = "";

        public static void MenuCargo()
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
            var cargoServ = scope.ServiceProvider.GetRequiredService<CargoServ>();

            switch (Global.Menu2)
            {
                case "1":
                    Cadastro(cargoServ);
                    break;
                case "2":
                    Consulta(cargoServ);
                    break;
                case "3":
                    Exclusao(cargoServ);
                    break;
            }
        }

        private static void Cadastro(CargoServ cargoServ)
        {
            Tela.LimpaTela();
            Console.WriteLine("=== CADASTRO DE CARGO ===");
            
            Console.Write("Nome do Cargo: ");
            string nome = Console.ReadLine();

            cargoServ.Cadastrar(nome);
            Console.WriteLine("Cargo cadastrado com sucesso!");
            Console.ReadKey();
        }

        private static void Consulta(CargoServ cargoServ)
        {
            Tela.LimpaTela();
            Console.WriteLine("=== LISTA DE CARGOS ===");
            var lista = cargoServ.Listar();
            foreach(var item in lista)
            {
                Console.WriteLine($"ID: {item.ID} | Nome: {item.Nome}");
            }
            Console.ReadKey();
        }

        private static void Exclusao(CargoServ cargoServ)
        {
            Tela.LimpaTela();
            Console.WriteLine("=== EXCLUSÃO DE CARGO ===");
            Console.Write("Digite o ID do Cargo: ");
            if(int.TryParse(Console.ReadLine(), out int id))
            {
                if(cargoServ.Deletar(id))
                    Console.WriteLine("Cargo removido!");
                else
                    Console.WriteLine("Erro ao remover ou não encontrado.");
            }
            Console.ReadKey();
        }

    }
}