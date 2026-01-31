using System;
using Saloon.Domain.Entities;
using Saloon.Application.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Saloon.ConsoleApp.InterfaceUsuario
{
    public class Cliente
    {
        private static string opcao = "";

        public static void MenuCliente()
        {
            do
            {
                Tela.SafeSetCursor(33, 13);
                Console.Write("1 - Cadastro");
                Tela.SafeSetCursor(33, 14);
                Console.Write("2 - Consulta");
                Tela.SafeSetCursor(33, 15);
                Console.Write("3 - Alteração");
                Tela.SafeSetCursor(33, 17);
                Console.Write("X - Sair");
                Global.Menu2 = Validacao.ValidaMenu2();

            } while (Global.Menu2 != "1" && Global.Menu2 != "2" && Global.Menu2 != "3" && Global.Menu2 != "X");

            Tela.Moldura3();
            switch (Global.Menu2)
            {
                case "1":
                    Cadastro();
                    Tela.SafeSetCursor(53,23);
                    Console.Write(Global.Menu2 == "S"?"Cliente cadastrado com sucesso!":"Cadastro cancelado!");
                    Console.ReadKey();        
                    break;
                    
                case "2":
                    Alteracao();
                    break;
                case "3":
                    Consulta();
                    break;
                default: break;
            }
        }
        private static void Cadastro()
        {
            string nome, telefone;
            DateOnly aniversario;
            List<string> observacoes = new List<string>();
            do 
            {
                LimpaDados();
                Tela.SafeSetCursor(53, 14);
                Console.Write("Nome do Cliente ...: ");

                Tela.SafeSetCursor(53, 15);
                Console.Write("Telefone ..........: ");

                Tela.SafeSetCursor(53, 16);
                Console.Write("Data de aniversário: ");

                Tela.SafeSetCursor(53, 17);
                Console.Write("Observação.........: ");

                Tela.SafeSetCursor(74, 14);
                nome = Console.ReadLine();

                Tela.SafeSetCursor(74, 15);
                telefone = Console.ReadLine();

                bool dataValida;
                do
                {
                    Tela.SafeSetCursor(74, 16);
                    Console.Write("               ");
                    Tela.SafeSetCursor(74, 16);
                    dataValida = DateOnly.TryParse(Console.ReadLine(), out aniversario);
                } while (!dataValida);

                int i = 17;
                string obs;
                do
                {
                    Tela.SafeSetCursor(74, i);
                    obs = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(obs))
                    {
                        observacoes.Add(obs);
                    }
                    i++;
                } while (i < 22 && !string.IsNullOrWhiteSpace(obs));

                Tela.SafeSetCursor(53, 23);
                Console.Write("Confirma a inclusão (S/N/X - Sair)?");
                opcao = Console.ReadLine().ToUpper();
            } while (opcao != "S" && opcao != "X");

            if (opcao == "S")
            {
                using var scope = Program.ServiceProvider.CreateScope();
                var clienteServ = scope.ServiceProvider.GetRequiredService<ClienteServ>();
                var clienteObsServ = scope.ServiceProvider.GetRequiredService<ClienteObservacaoServ>();

                int idCliente = clienteServ.Gravar(nome, telefone, aniversario);
                if (observacoes.Count > 0)
                {
                    foreach (var item in observacoes)
                    {
                        clienteObsServ.Gravar(idCliente, item);
                    }
                }
                Global.Menu2 = "S";
            }else Global.Menu2 = "X";
        }

        private static void LimpaDados(){
            for (int i = 14; i < 23; i++)
            {
                Tela.SafeSetCursor(74, i);
                Console.Write("               ");
            }
            Tela.SafeSetCursor(53, 23);
            Console.Write("                                   ");
        }

        public static void Alteracao()
        {
        }

        public static void Consulta()
        {
        }

    }
}