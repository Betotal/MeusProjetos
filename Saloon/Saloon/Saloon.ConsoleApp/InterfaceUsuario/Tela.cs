using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Saloon.ConsoleApp.InterfaceUsuario
{
    public class Tela
    {
        public static void SafeSetCursor(int left, int top)
        {
            try
            {
                if (left >= 0 && left < Console.BufferWidth && top >= 0 && top < Console.BufferHeight)
                {
                    Console.SetCursorPosition(left, top);
                }
            }
            catch (ArgumentOutOfRangeException) { /* Ignore if window is too small */ }
        }

        public static void LimpaTela()
        {
            Console.Clear();
            Moldura();  
            NomeSalao();
        }

        public static void Moldura()
        {
            for (int i = 1; i <= 25; i++)
            {
                if (i == 1 ){
                    for (int j = 1; j <= 90; j++)
                    {
                        SafeSetCursor(j, 1);
                        Console.Write("*");
                        SafeSetCursor(j,25);
                        Console.Write("*");
                    }
 
                }
                SafeSetCursor(1,i);
                Console.Write("*");
                SafeSetCursor(90,i);
                Console.Write("*");
            }
        }

        public static void NomeSalao()
        {
            SafeSetCursor(20,3);
            Console.Write("*************************************");
            SafeSetCursor(20,4);
            Console.Write("***  Bem Vindo ao Sistema Saloon  ***");
            SafeSetCursor(20,5);
            Console.Write("*************************************");
        }

        public static void LimpaMenu2(string Menu1){
            Moldura2(Menu1);
        }

        public static void Moldura2(string Menu1)
        {
            for (int i = 9; i <= 22; i++)
            {
                if (i == 9 ){
                    for (int j = 30; j <= 61; j++)
                    {
                        SafeSetCursor(j, 9);
                        Console.Write("*");
                        SafeSetCursor(j, 22);
                        Console.Write("*");
                    } 
                }

                if (i > 9 && i < 22)
                {
                    SafeSetCursor(31, i);
                    Console.Write("                            ");
                }
                if (i == 10)
                {
                    SafeSetCursor(31, i);
                    switch (Menu1)
                    {
                        case "1":Console.Write(" *    Cadastro de Clientes   *");
                            break;
                        case "2":Console.Write(" * Cadastro de Profissionais *");
                            break;
                        case "3":Console.Write(" *    Cadastro de Serviços   *");
                            break;
                        case "4":Console.Write(" *    Cadastro de Horarios   *");
                            break;
                        case "5":Console.Write(" *     Cadastro de Cargos    *");
                            break;
                        case "6":Console.Write(" *   Cadastro de Descontos   *");
                            break;
                        case "7":Console.Write(" *   Cadastro de Promoçoes   *");
                            break;
                        case "8":Console.Write(" *        Agendamento        *");
                            break;
                    }                    
                }
                if (i == 11)
                {
                    SafeSetCursor(31, i);
                    Console.Write("******************************");
                }
                SafeSetCursor(30, i);
                Console.Write(" *");
                SafeSetCursor(61,i);
                Console.Write("*");                
            }
        }

        public static void Moldura3()
        {
            for (int i = 12; i <= 24; i++)
            {
                if (i == 12 ){
                    for (int j = 50; j <= 89; j++)
                    {
                        SafeSetCursor(j, 12);
                        Console.Write("/");
                        SafeSetCursor(j, 24);
                        Console.Write("/");
                    } 
                }
                

                if (i > 12 && i < 24)
                {
                    SafeSetCursor(51, i);
                    Console.Write("                                ");
                }
                SafeSetCursor(50, i);
                Console.Write(" /");
                SafeSetCursor(89,i);
                Console.Write("/");                
            }
        }

    }
}