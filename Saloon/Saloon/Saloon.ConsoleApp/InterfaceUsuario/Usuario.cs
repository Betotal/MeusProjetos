using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saloon.ConsoleApp.InterfaceUsuario
{
    public class Usuario
    {
        private static string opcao = "";

        public static void CadastroUsuario()
        {
            string opcao1 = MenuUsuario();

            switch (opcao1)
            {
                case "1": Cadastro();
                    break;
                case "2": Alteracao();
                    break;
                case "3": Consulta();
                    break;
            }

        }

        public static string MenuUsuario()
        {
            do
            {
                Console.SetCursorPosition(43, 14);
                Console.Write("1 - Cadastro");
                Console.SetCursorPosition(43,15);
                Console.Write("2 - Consulta");
                Console.SetCursorPosition(43,16);
                Console.Write("3 - Alteração");
                Console.SetCursorPosition(43,18);
                Console.Write("X - Sair");
                Global.Menu2 = Validacao.ValidaMenu2();

            } while (Global.Menu2 != "1" && Global.Menu2 != "2" && Global.Menu2 != "3" && Global.Menu2 != "X");

            return Global.Menu2;
        }

        public static void Cadastro(){
            Console.SetCursorPosition(30, 9);
            Console.Write("Nome do Usuário (15 posições): ");
        //    string Usuario.Login = Console.ReadLine();
            Console.SetCursorPosition(30,10);
            Console.Write(" 2 - Consulta");
            Console.SetCursorPosition(30,11);
            Console.Write(" 3 - Alteração");
            Console.SetCursorPosition(30,13);
            Console.Write("X - Sair");

        }

        public static void Alteracao()
        {
        }

        public static void Consulta()
        {
        }
    }
}