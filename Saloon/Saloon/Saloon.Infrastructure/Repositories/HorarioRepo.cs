using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Saloon.Domain.Entities;
using Saloon.Domain.Interfaces;

namespace Saloon.Infrastructure.Repositories
{
    public class HorarioRepo : IHorarioRepo
    {
        public int Cadastrar(HorarioEnty horario)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                string sql = $"INSERT INTO HORARIO (HorarioInicial, HorarioFinal) VALUES ('{horario.HorarioInicial}', '{horario.HorarioFinal}')";
                var cmd = new SqlCommand(sql, connection);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT MAX(ID) FROM HORARIO", connection);
                return (int)cmd.ExecuteScalar();
            }
        }

        public List<HorarioEnty> Listar()
        {
            var lista = new List<HorarioEnty>();
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand("SELECT ID, HorarioInicial, HorarioFinal FROM HORARIO", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new HorarioEnty
                        {
                            ID = reader.GetInt32(0),
                            HorarioInicial = TimeOnly.Parse(reader.GetString(1)),
                            HorarioFinal = TimeOnly.Parse(reader.GetString(2))
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
                var cmd = new SqlCommand($"DELETE FROM HORARIO WHERE ID = {id}", connection);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public HorarioEnty? ConsultarPorId(int id)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand($"SELECT ID, HorarioInicial, HorarioFinal FROM HORARIO WHERE ID = {id}", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new HorarioEnty
                        {
                            ID = reader.GetInt32(0),
                            HorarioInicial = TimeOnly.Parse(reader.GetString(1)),
                            HorarioFinal = TimeOnly.Parse(reader.GetString(2))
                        };
                    }
                }
            }
            return null;
        }
    }
}