using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Saloon.ConsoleApp.InterfaceUsuario;
using Saloon.Domain.Interfaces;
using Saloon.Infrastructure.Repositories;
using Saloon.Application.Services;

namespace Saloon.ConsoleApp
{
    class Program
    {
        public static IServiceProvider ServiceProvider;

        static void Main(string[] args)
        {
             // Configuração de Injeção de Dependência
             var services = new ServiceCollection();
             
             // Repositórios
             services.AddScoped<IClienteRepo, ClienteRepo>();
             services.AddScoped<IUsuarioRepo, UsuarioRepo>();
             services.AddScoped<IClienteObservacaoRepo, ClienteObservacaoRepo>();
             
             // Serviços
             services.AddScoped<ClienteServ>();
             services.AddScoped<UsuarioServ>();
             services.AddScoped<ClienteObservacaoServ>();
             services.AddScoped<ProfissionalServ>();
             services.AddScoped<ServicoServ>();
             services.AddScoped<HorarioServ>();
             services.AddScoped<CargoServ>();
             services.AddScoped<DescontoServ>();
             services.AddScoped<PromocaoServ>();
             services.AddScoped<AgendamentoServ>();

             services.AddScoped<IProfissionalRepo, ProfissionalRepo>();
             services.AddScoped<IServicoRepo, ServicoRepo>();
             services.AddScoped<IHorarioRepo, HorarioRepo>();
             services.AddScoped<ICargoRepo, CargoRepo>();
             services.AddScoped<IDescontoRepo, DescontoRepo>();
             services.AddScoped<IPromocaoRepo, PromocaoRepo>();
             services.AddScoped<IAgendaRepo, AgendaRepo>();

             ServiceProvider = services.BuildServiceProvider();

             // Configuração do Global.StringConnection (temporário até refatorar Infra)
             // var config = new ConfigurationBuilder()... (assumindo que já existe logica ou hardcoded)
             // Para o exemplo, mantemos o hardcoded do Global se existir, ou definimos aqui se necessário.
             // Global.StringConnection = ...; // Já deve estar definido em Global ou lido.

            //            Console.WriteLine("Hello, World!");

            //       // String de conexão para SQLLocalDB
            //        string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=Saloon;Trusted_Connection=True;";

            // Carrega o appsettings.json
            //        var config = new ConfigurationBuilder()
            //            .SetBasePath(Directory.GetCurrentDirectory()) // importante para achar o arquivo
            //            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //            .Build();

            #region Uso do Entity
            // Lê a string de conexão
            //            var connectionString = config.GetConnectionString("DefaultConnection");
            //
            // Faz o uso do DBContext, pelo AppDbContext
            //            var options = new DbContextOptionsBuilder<AppDbContext>()
            //                .UseSqlServer(connectionString)
            //                .Options;
            //
            // Cria o contexto
            //            using var context = new AppDbContext(options);
            //            var connectionString = config.GetConnectionString("DefaultConnection");
            #endregion

            // ABERTURA E FECHAMENTO DA CONEXAO
            // using (var connection = new SqlConnection(connectionString))
            // {
            //     connection.Open();
            //     Console.WriteLine("Conexão realizada com sucesso!");

            //     // Exemplo de consulta simples
            //     string sql = "SELECT COUNT(*) FROM Usuarios";
            //     using (var command = new SqlCommand(sql, connection))
            //     {
            //         int count = (int)command.ExecuteScalar();
            //         Console.WriteLine($"Total de usuários: {count}");
            //     }
            // }


            //                context.Database.EnsureCreated();
            //            Console.WriteLine("Conexão realizada com sucesso!");
            //            Console.ReadKey();

            do
            {

                Principal.MenuPrincipipal();

                if (Global.Menu1 != "X")
                {
                    Tela.LimpaMenu2(Global.Menu1);

                    switch (Global.Menu1)
                    {
                        case "1":
                            Cliente.MenuCliente();
                            break;
                        case "2":
                            Profissional.MenuProfissional();
                            break;
                        case "3":
                            Servico.MenuServico();
                            break;
                        case "4":
                            Horario.MenuHorario();
                            break;
                        case "5":
                            Cargo.MenuCargo();
                            break;
                        case "6":
                            Desconto.MenuDesconto();
                            break;
                        case "7":
                            Promocao.MenuPromocao();
                            break;
                        case "8":
                            Agendamento.MenuAgendamento();
                            break;
                    }
                }
            } while (Global.Menu1 != "X");
            Tela.LimpaTela();
            Tela.SafeSetCursor(30, 15);
            Console.WriteLine("Obrigado por usar os nossos serviços.");
            Console.ReadKey();
        }
    }
}


// See https://aka.ms/new-console-template for more information
