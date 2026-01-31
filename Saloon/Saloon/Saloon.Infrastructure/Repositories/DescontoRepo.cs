using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Saloon.Domain.Entities;
using Saloon.Domain.Interfaces;

namespace Saloon.Infrastructure.Repositories
{
    public class DescontoRepo : IDescontoRepo
    {
        public int Cadastrar(DescontoEnty desconto)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                string sql = $"INSERT INTO DESCONTO (IdCliente, PorcentagemDesconto) VALUES ({desconto.IdCliente}, {desconto.PorcentagemDesconto})";
                var cmd = new SqlCommand(sql, connection);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT MAX(ID) FROM DESCONTO", connection);
                return (int)cmd.ExecuteScalar();
            }
        }

        public List<DescontoEnty> Listar()
        {
            var lista = new List<DescontoEnty>();
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand("SELECT ID, IdCliente, PorcentagemDesconto FROM DESCONTO", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new DescontoEnty
                        {
                            ID = reader.GetInt32(0),
                            IdCliente = reader.GetInt32(1),
                            PorcentagemDesconto = reader.GetInt32(2)
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
                var cmd = new SqlCommand($"DELETE FROM DESCONTO WHERE ID = {id}", connection);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public DescontoEnty? ConsultarPorId(int id)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand($"SELECT ID, IdCliente, PorcentagemDesconto FROM DESCONTO WHERE ID = {id}", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new DescontoEnty
                        {
                            ID = reader.GetInt32(0),
                            IdCliente = reader.GetInt32(1),
                            PorcentagemDesconto = reader.GetInt32(2)
                        };
                    }
                }
            }
            return null;
        }
    }
}