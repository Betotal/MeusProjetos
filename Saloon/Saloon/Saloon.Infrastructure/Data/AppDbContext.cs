using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Saloon.Domain.Entities;

namespace Saloon.Infrastructure.Data
{

//     public class AppDbContext : DbContext
//     {
//         public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
//         {
//         }

//         // Remova o OnConfiguring se usar DI    
// //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// //        {
// //            if (!optionsBuilder.IsConfigured)
// //            {
// //                optionsBuilder.UseSqlServer(
// //                    @"Server=(localdb)\MSSQLLocalDB;Database=Saloon;Trusted_Connection=True;");
// //            }
// //        }
        
//         public DbSet<UsuarioEnty> Usuarios { get; set; }
//         public DbSet<AcessoEnty> Acessos { get; set; }
//         public DbSet<UsuarioAcessoEnty> UsuarioAcessos { get; set; }
//         public DbSet<ClienteEnty> Clientes { get; set; }
//         public DbSet<ClienteObservacaoEnty> ClienteObservacoes { get; set; }
//         public DbSet<ClientePromocaoEnty> ClientePromocoes { get; set; }
//         public DbSet<PromocaoEnty> Promocoes { get; set; }  
//         public DbSet<ProfissionalEnty> Profissionais { get; set; }
//         public DbSet<CargoEnty> Cargos { get; set; }
//         public DbSet<ServicoEnty> Servicos { get; set; }
//         public DbSet<ServicoConjugadoEnty> ServicoConjugados { get; set; }
//         public DbSet<HorarioEnty> Horarios { get; set; }
//         public DbSet<ProfissionalCargoEnty> ProfissionalCargos { get; set; }
//         public DbSet<ProfissionalServicoEnty> ProfissionalServicos { get; set; }
//         public DbSet<ProfissionalHorarioEnty> ProfissionalHorarios { get; set; }
//         public DbSet<AgendaEnty> Agendamentos { get; set; }
        
//     }
}