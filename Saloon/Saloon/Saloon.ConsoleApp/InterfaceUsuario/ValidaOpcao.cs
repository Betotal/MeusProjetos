using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saloon.ConsoleApp.InterfaceUsuario
{
    public class Validacao
    {
        public static string ValidaMenu1(string opcao){
            if (string.IsNullOrEmpty(opcao))
            {
                Console.SetCursorPosition(30, 23);
                Console.WriteLine("Selecione uma opção para continuar!");
                Console.ReadKey();
                Tela.LimpaTela();
            }
            else if (opcao != "1" && opcao != "2" && opcao != "3" && opcao != "4" && opcao != "5" && opcao != "6" && opcao != "7" && opcao != "8" && opcao != "9" && opcao != "10" && opcao != "X")
            {
                Console.SetCursorPosition(30, 23);
                Console.WriteLine("Opção Inválida! Tente Novamente.");
                Console.ReadKey();
                Tela.LimpaTela();
            }
            return opcao;
        }

        public static string ValidaMenu2(){
            string opcao;
            do
            {
                Console.SetCursorPosition(33, 21);
                Console.Write("                     ");
                Console.SetCursorPosition(33, 19);
                Console.Write("                         ");

                Console.SetCursorPosition(33, 19);
                Console.Write("Selecione uma opção: ");
                opcao = Console.ReadLine().ToUpper();

                if (string.IsNullOrEmpty(opcao))
                {
                    Console.SetCursorPosition(33, 21);
                    Console.Write("Selecione uma opção!");
                    Console.ReadKey();
                }
                else if (opcao != "1" && opcao != "2" && opcao != "3" && opcao != "X")
                {
                    Console.SetCursorPosition(33, 21);
                    Console.Write("Opção Inválida! ");
                    Console.ReadKey();
                }
            } while (opcao != "1" && opcao != "2" && opcao != "3" && opcao != "X");
            return opcao;
        }
    }
}