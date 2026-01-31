using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Saloon.Domain.Entities;
using Saloon.Domain.Interfaces;

namespace Saloon.Infrastructure.Repositories
{
    public class CargoRepo : ICargoRepo
    {
        public int Cadastrar(CargoEnty cargo)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                string sql = $"INSERT INTO CARGO (Nome) VALUES ('{cargo.Nome}')";
                var cmd = new SqlCommand(sql, connection);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT MAX(ID) FROM CARGO", connection);
                return (int)cmd.ExecuteScalar();
            }
        }

        public List<CargoEnty> Listar()
        {
            var lista = new List<CargoEnty>();
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand("SELECT ID, Nome FROM CARGO", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new CargoEnty
                        {
                            ID = reader.GetInt32(0),
                            Nome = reader.GetString(1)
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
                var cmd = new SqlCommand($"DELETE FROM CARGO WHERE ID = {id}", connection);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public CargoEnty? ConsultarPorId(int id)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand($"SELECT ID, Nome FROM CARGO WHERE ID = {id}", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new CargoEnty
                        {
                            ID = reader.GetInt32(0),
                            Nome = reader.GetString(1)
                        };
                    }
                }
            }
            return null;
        }
    }
}