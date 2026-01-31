using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Saloon.Domain.Entities;
using Saloon.Domain.Interfaces;
using Saloon.Domain.Enums;
using System.Globalization;

namespace Saloon.Infrastructure.Repositories
{
    public class PromocaoRepo : IPromocaoRepo
    {
        public int Cadastrar(PromocaoEnty promocao)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                // Assumindo estrutura basica, ajustaremos campos de data/enum
                string sql = $"INSERT INTO PROMOCAO (Nome, PromocaoInicial, PromocaoFinal, IdServico, QuantidadeServico, PorcentagemDesconto, DiaSemanaInicial, DiaSemanaFinal, QuemPaga) " +
                             $"VALUES ('{promocao.Nome}', '{promocao.PromocaoInicial}', '{promocao.PromocaoFinal}', {promocao.IdServico}, {promocao.QuantidadeServico}, {promocao.PorcentagemDesconto}, {(int)promocao.DiaSemanaInicial}, {(int)promocao.DiaSemanaFinal}, {(int)promocao.QuemPaga})";
                var cmd = new SqlCommand(sql, connection);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT MAX(ID) FROM PROMOCAO", connection);
                return (int)cmd.ExecuteScalar();
            }
        }

        public List<PromocaoEnty> Listar()
        {
            var lista = new List<PromocaoEnty>();
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand("SELECT ID, Nome, PromocaoInicial, PromocaoFinal, IdServico, QuantidadeServico, PorcentagemDesconto, DiaSemanaInicial, DiaSemanaFinal, QuemPaga FROM PROMOCAO", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new PromocaoEnty
                        {
                            ID = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            PromocaoInicial = DateOnly.FromDateTime(reader.GetDateTime(2)), // Assuming stored as Date/DateTime
                            PromocaoFinal = DateOnly.FromDateTime(reader.GetDateTime(3)),
                            IdServico = reader.GetInt32(4),
                            QuantidadeServico = reader.GetInt32(5),
                            PorcentagemDesconto = reader.GetInt32(6),
                            DiaSemanaInicial = (EnumDiaSemana)reader.GetInt32(7),
                            DiaSemanaFinal = (EnumDiaSemana)reader.GetInt32(8),
                            QuemPaga = (EnumQuemPaga)reader.GetInt32(9)
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
                var cmd = new SqlCommand($"DELETE FROM PROMOCAO WHERE ID = {id}", connection);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public PromocaoEnty? ConsultarPorId(int id)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand($"SELECT * FROM PROMOCAO WHERE ID = {id}", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new PromocaoEnty
                        {
                            ID = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            PromocaoInicial = DateOnly.FromDateTime(reader.GetDateTime(2)),
                            PromocaoFinal = DateOnly.FromDateTime(reader.GetDateTime(3)),
                            IdServico = reader.GetInt32(4),
                            QuantidadeServico = reader.GetInt32(5),
                            PorcentagemDesconto = reader.GetInt32(6),
                            DiaSemanaInicial = (EnumDiaSemana)reader.GetInt32(7),
                            DiaSemanaFinal = (EnumDiaSemana)reader.GetInt32(8),
                            QuemPaga = (EnumQuemPaga)reader.GetInt32(9)
                        };
                    }
                }
            }
            return null;
        }
    }
}