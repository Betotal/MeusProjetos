using System;
using Saloon.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saloon.ConsoleApp.InterfaceUsuario
{
    public class Profissional
    {
        private static string opcao = "";

        public static void MenuProfissional()
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
            var profissionalServ = scope.ServiceProvider.GetRequiredService<ProfissionalServ>();
            var clienteServ = scope.ServiceProvider.GetRequiredService<ClienteServ>();

            switch (Global.Menu2)
            {
                case "1":
                    Cadastro(profissionalServ, clienteServ);
                    break;
                case "2":
                    Consulta(profissionalServ, clienteServ); 
                    break;
                case "3":
                    Exclusao(profissionalServ);
                    break;
            }
        }

        private static void Cadastro(ProfissionalServ profissionalServ, ClienteServ clienteServ)
        {
            Tela.LimpaTela();
            Console.WriteLine("=== CADASTRO DE PROFISSIONAL ===");
            
            // List existing clients to link
            var clientes = clienteServ.ListarClientes();
            if(!clientes.Any()) {
                Console.WriteLine("Nenhum cliente cadastrado. Cadastre um cliente primeiro.");
                Console.ReadKey();
                return;
            }

            foreach(var c in clientes) {
                Console.WriteLine($"ID: {c.ID} - Nome: {c.Nome}");
            }

            Console.Write("Digite o ID do Cliente para tornar Profissional: ");
            if(int.TryParse(Console.ReadLine(), out int idCliente)) {
                Console.Write("Salário Fixo: ");
                decimal.TryParse(Console.ReadLine(), out decimal salario);
                
                Console.Write("Comissão (%): ");
                int.TryParse(Console.ReadLine(), out int comissao);

                profissionalServ.Cadastrar(idCliente, salario, comissao);
                Console.WriteLine("Profissional cadastrado com sucesso!");
            } else {
                Console.WriteLine("ID Inválido.");
            }
            Console.ReadKey();
        }

        private static void Consulta(ProfissionalServ profissionalServ, ClienteServ clienteServ)
        {
            Tela.LimpaTela();
            Console.WriteLine("=== LISTA DE PROFISSIONAIS ===");
            var lista = profissionalServ.Listar();
            foreach(var item in lista)
            {
                var cliente = clienteServ.ConsultarId(item.IdCliente);
                string nome = cliente != null ? cliente.Nome : "Desconhecido";
                Console.WriteLine($"ID: {item.ID} | Nome: {nome} | Salário: {item.SalarioFixo} | Comissão: {item.PorcentagemComissao}%");
            }
            Console.ReadKey();
        }

        private static void Exclusao(ProfissionalServ profissionalServ)
        {
            Tela.LimpaTela();
            Console.WriteLine("=== EXCLUSÃO DE PROFISSIONAL ===");
            Console.Write("Digite o ID do Profissional: ");
            if(int.TryParse(Console.ReadLine(), out int id))
            {
                if(profissionalServ.Deletar(id))
                    Console.WriteLine("Profissional removido!");
                else
                    Console.WriteLine("Erro ao remover ou não encontrado.");
            }
            Console.ReadKey();
        }
    }
}