using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Saloon.Domain.Entities;
using Saloon.Domain.Interfaces;

namespace Saloon.Infrastructure.Repositories
{
    public class ProfissionalRepo : IProfissionalRepo
    {
        public int Cadastrar(ProfissionalEnty profissional)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand($"INSERT INTO PROFISSIONAL (IdCliente, SalarioFixo, PorcentagemComissao) VALUES ({profissional.IdCliente}, {profissional.SalarioFixo.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {profissional.PorcentagemComissao})", connection);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT MAX(ID) FROM PROFISSIONAL", connection);
                return (int)cmd.ExecuteScalar();
            }
        }

        public List<ProfissionalEnty> Listar()
        {
            var lista = new List<ProfissionalEnty>();
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand("SELECT ID, IdCliente, SalarioFixo, PorcentagemComissao FROM PROFISSIONAL", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new ProfissionalEnty
                        {
                            ID = reader.GetInt32(0),
                            IdCliente = reader.GetInt32(1),
                            SalarioFixo = reader.GetDecimal(2),
                            PorcentagemComissao = reader.GetInt32(3)
                        });
                    }
                }
            }
            return lista;
        }

        public bool Deletar(int id)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand($"DELETE FROM PROFISSIONAL WHERE ID = {id}", connection);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public ProfissionalEnty? ConsultarPorId(int id)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand($"SELECT ID, IdCliente, SalarioFixo, PorcentagemComissao FROM PROFISSIONAL WHERE ID = {id}", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new ProfissionalEnty
                        {
                            ID = reader.GetInt32(0),
                            IdCliente = reader.GetInt32(1),
                            SalarioFixo = reader.GetDecimal(2),
                            PorcentagemComissao = reader.GetInt32(3)
                        };
                    }
                }
            }
            return null;
        }
    }
}
