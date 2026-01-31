using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Saloon.Domain.Entities;
using Saloon.Domain.Interfaces;
using Saloon.Domain.Enums;
using System.Globalization;

namespace Saloon.Infrastructure.Repositories
{
    public class ServicoRepo : IServicoRepo
    {
        public int Cadastrar(ServicoEnty servico)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                string sql = $"INSERT INTO SERVICO (Nome, PrecoInicial, TempoMedio, Situacao, TempoDeEncaixe) VALUES ('{servico.Nome}', {servico.PrecoInicial.ToString(CultureInfo.InvariantCulture)}, '{servico.TempoMedio}', {(int)servico.Situacao}, '{servico.TempoDeEncaixe}')";
                var cmd = new SqlCommand(sql, connection);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT MAX(ID) FROM SERVICO", connection);
                return (int)cmd.ExecuteScalar();
            }
        }

        public List<ServicoEnty> Listar()
        {
            var lista = new List<ServicoEnty>();
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand("SELECT ID, Nome, PrecoInicial, TempoMedio, Situacao, TempoDeEncaixe FROM SERVICO", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new ServicoEnty
                        {
                            ID = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            PrecoInicial = reader.GetDecimal(2),
                            TempoMedio = TimeOnly.Parse(reader.GetString(3)),
                            Situacao = (EnumSituacao)reader.GetInt32(4),
                            TempoDeEncaixe = TimeOnly.Parse(reader.GetString(5))
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
                var cmd = new SqlCommand($"DELETE FROM SERVICO WHERE ID = {id}", connection);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public ServicoEnty? ConsultarPorId(int id)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand($"SELECT ID, Nome, PrecoInicial, TempoMedio, Situacao, TempoDeEncaixe FROM SERVICO WHERE ID = {id}", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new ServicoEnty
                        {
                            ID = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            PrecoInicial = reader.GetDecimal(2),
                            TempoMedio = TimeOnly.Parse(reader.GetString(3)),
                            Situacao = (EnumSituacao)reader.GetInt32(4),
                            TempoDeEncaixe = TimeOnly.Parse(reader.GetString(5))
                        };
                    }
                }
            }
            return null;
        }
    }
}