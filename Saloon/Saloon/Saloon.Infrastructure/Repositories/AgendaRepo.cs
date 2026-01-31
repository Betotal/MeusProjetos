using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Saloon.Domain.Entities;
using Saloon.Domain.Interfaces;
using Saloon.Domain.Enums;
using System.Globalization;

namespace Saloon.Infrastructure.Repositories
{
    public class AgendaRepo : IAgendaRepo
    {
        public int Cadastrar(AgendaEnty agenda)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                string sql = $"INSERT INTO AGENDA (AgendaData, AgendaHora, DiaSemana, IdProfissional, IdCliente, IdServico, IdClientePromocao, AgendaSituacao) " +
                             $"VALUES ('{agenda.AgendaData}', '{agenda.AgendaHora}', {(int)agenda.DiaSemana}, {agenda.IdProfissional}, {agenda.IdCliente}, {agenda.IdServico}, {agenda.IdClientePromocao}, {(int)agenda.AgendaSituacao})";
                var cmd = new SqlCommand(sql, connection);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT MAX(ID) FROM AGENDA", connection);
                return (int)cmd.ExecuteScalar();
            }
        }

        public List<AgendaEnty> Listar()
        {
            var lista = new List<AgendaEnty>();
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand("SELECT ID, AgendaData, AgendaHora, DiaSemana, IdProfissional, IdCliente, IdServico, IdClientePromocao, AgendaSituacao FROM AGENDA", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new AgendaEnty
                        {
                            ID = reader.GetInt32(0),
                            AgendaData = DateOnly.FromDateTime(reader.GetDateTime(1)),
                            AgendaHora = TimeOnly.FromTimeSpan(reader.GetTimeSpan(2)), // Assuming Time(7) in SQL
                            DiaSemana = (EnumDiaSemana)reader.GetInt32(3),
                            IdProfissional = reader.GetInt32(4),
                            IdCliente = reader.GetInt32(5),
                            IdServico = reader.GetInt32(6),
                            IdClientePromocao = reader.GetInt32(7),
                            AgendaSituacao = (EnumSituacaoAgenda)reader.GetInt32(8)
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
                var cmd = new SqlCommand($"DELETE FROM AGENDA WHERE ID = {id}", connection);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public AgendaEnty? ConsultarPorId(int id)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand($"SELECT ID, AgendaData, AgendaHora, DiaSemana, IdProfissional, IdCliente, IdServico, IdClientePromocao, AgendaSituacao FROM AGENDA WHERE ID = {id}", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new AgendaEnty
                        {
                            ID = reader.GetInt32(0),
                            AgendaData = DateOnly.FromDateTime(reader.GetDateTime(1)),
                            AgendaHora = TimeOnly.FromTimeSpan(reader.GetTimeSpan(2)),
                            DiaSemana = (EnumDiaSemana)reader.GetInt32(3),
                            IdProfissional = reader.GetInt32(4),
                            IdCliente = reader.GetInt32(5),
                            IdServico = reader.GetInt32(6),
                            IdClientePromocao = reader.GetInt32(7),
                            AgendaSituacao = (EnumSituacaoAgenda)reader.GetInt32(8)
                        };
                    }
                }
            }
            return null;
        }
    }
}