using System;
using Saloon.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Saloon.ConsoleApp.InterfaceUsuario
{
    public class Desconto
    {
        private static string opcao = "";

        public static void MenuDesconto()
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
            var descontoServ = scope.ServiceProvider.GetRequiredService<DescontoServ>();
            var clienteServ = scope.ServiceProvider.GetRequiredService<ClienteServ>();

            switch (Global.Menu2)
            {
                case "1":
                    Cadastro(descontoServ, clienteServ);
                    break;
                case "2":
                    Consulta(descontoServ, clienteServ);
                    break;
                case "3":
                    Exclusao(descontoServ);
                    break;
            }
        }

        private static void Cadastro(DescontoServ descontoServ, ClienteServ clienteServ)
        {
            Tela.LimpaTela();
            Console.WriteLine("=== CADASTRO DE DESCONTO ===");
            
            // Listar Clientes
            var clientes = clienteServ.ListarClientes();
            if(!clientes.Any()) {
                Console.WriteLine("Nenhum cliente disponível.");
                Console.ReadKey();
                return;
            }

            foreach(var c in clientes) {
                Console.WriteLine($"ID: {c.ID} - Nome: {c.Nome}");
            }

            Console.Write("ID do Cliente: ");
            if(int.TryParse(Console.ReadLine(), out int idCliente))
            {
                Console.Write("Porcentagem (%): ");
                int.TryParse(Console.ReadLine(), out int porcentagem);

                descontoServ.Cadastrar(idCliente, porcentagem);
                Console.WriteLine("Desconto cadastrado com sucesso!");
            }
            Console.ReadKey();
        }

        private static void Consulta(DescontoServ descontoServ, ClienteServ clienteServ)
        {
            Tela.LimpaTela();
            Console.WriteLine("=== LISTA DE DESCONTOS ===");
            var lista = descontoServ.Listar();
            foreach(var item in lista)
            {
                var cliente = clienteServ.ConsultarId(item.IdCliente);
                string nome = cliente != null ? cliente.Nome : "Desconhecido";
                Console.WriteLine($"ID: {item.ID} | Cliente: {nome} | Desconto: {item.PorcentagemDesconto}%");
            }
            Console.ReadKey();
        }

        private static void Exclusao(DescontoServ descontoServ)
        {
            Tela.LimpaTela();
            Console.WriteLine("=== EXCLUSÃO DE DESCONTO ===");
            Console.Write("Digite o ID do Desconto: ");
            if(int.TryParse(Console.ReadLine(), out int id))
            {
                if(descontoServ.Deletar(id))
                    Console.WriteLine("Desconto removido!");
                else
                    Console.WriteLine("Erro ao remover ou não encontrado.");
            }
            Console.ReadKey();
        }
        
    }
}