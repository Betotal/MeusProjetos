using System;
using System.Collections.Generic;
using System.Net;

namespace Saloon.ConsoleApp.InterfaceUsuario
{
    public class Principal
    {
        public static string MenuPrincipipal()
        {
            do
            {
                Tela.LimpaTela();

                Tela.SafeSetCursor(30, 7);
                Console.Write("*****     MENU PRINCIPAL     *****");

                Tela.SafeSetCursor(10, 9);
                Console.Write("1 - Cadastro de Clientes");
                Tela.SafeSetCursor(10,10);
                Console.Write("2 - Cadastro de Profissionais");
                Tela.SafeSetCursor(10,11);
                Console.Write("3 - Cadastro de Serviços");
                Tela.SafeSetCursor(10,12);
                Console.Write("4 - Cadastro de Horarios");
                Tela.SafeSetCursor(10,13);
                Console.Write("5 - Cadastro de Cargos");
                Tela.SafeSetCursor(10,14);
                Console.Write("6 - Cadastro de Descontos");
                Tela.SafeSetCursor(10,15);
                Console.Write("7 - Cadastro de Promoçoes");
                Tela.SafeSetCursor(10,16);
                Console.Write("8 - Agendamento");
                Tela.SafeSetCursor(10,17);
                Console.Write("9 - Consulta da Agenda de 7 dias");
                Tela.SafeSetCursor(10,19);
                Console.Write("X - Sair");

                Tela.SafeSetCursor(10, 21);
                Console.Write("Digite a Opção Desejada: ");

                Global.Menu1 = Validacao.ValidaMenu1(Console.ReadLine().ToUpper());

            } while (Global.Menu1 != "1" && 
                     Global.Menu1 != "2" && 
                     Global.Menu1 != "3" && 
                     Global.Menu1 != "4" &&
                     Global.Menu1 != "5" && 
                     Global.Menu1 != "6" && 
                     Global.Menu1 != "7" && 
                     Global.Menu1 != "8" && 
                     Global.Menu1 != "9" && 
                     Global.Menu1 != "X");

            return Global.Menu1;
        }

    }
}