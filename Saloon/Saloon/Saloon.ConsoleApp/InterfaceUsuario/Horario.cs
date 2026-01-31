using System;
using Saloon.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saloon.ConsoleApp.InterfaceUsuario
{
    public class Horario
    {
        private static string opcao = "";

        public static void MenuHorario()
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
            var horarioServ = scope.ServiceProvider.GetRequiredService<HorarioServ>();

            switch (Global.Menu2)
            {
                case "1":
                    Cadastro(horarioServ);
                    break;
                case "2":
                    Consulta(horarioServ);
                    break;
                case "3":
                    Exclusao(horarioServ);
                    break;
            }
        }

        private static void Cadastro(HorarioServ horarioServ)
        {
            Tela.LimpaTela();
            Console.WriteLine("=== CADASTRO DE HORÁRIO ===");
            
            Console.Write("Horário Inicial (hh:mm): ");
            TimeOnly.TryParse(Console.ReadLine(), out TimeOnly inicio);

            Console.Write("Horário Final (hh:mm): ");
            TimeOnly.TryParse(Console.ReadLine(), out TimeOnly fim);

            horarioServ.Cadastrar(inicio, fim);
            Console.WriteLine("Horário cadastrado com sucesso!");
            Console.ReadKey();
        }

        private static void Consulta(HorarioServ horarioServ)
        {
            Tela.LimpaTela();
            Console.WriteLine("=== LISTA DE HORÁRIOS ===");
            var lista = horarioServ.Listar();
            foreach(var item in lista)
            {
                Console.WriteLine($"ID: {item.ID} | Início: {item.HorarioInicial} | Fim: {item.HorarioFinal}");
            }
            Console.ReadKey();
        }

        private static void Exclusao(HorarioServ horarioServ)
        {
            Tela.LimpaTela();
            Console.WriteLine("=== EXCLUSÃO DE HORÁRIO ===");
            Console.Write("Digite o ID do Horário: ");
            if(int.TryParse(Console.ReadLine(), out int id))
            {
                if(horarioServ.Deletar(id))
                    Console.WriteLine("Horário removido!");
                else
                    Console.WriteLine("Erro ao remover ou não encontrado.");
            }
            Console.ReadKey();
        }
        
    }
}