using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Saloon.Domain.Entities;
using Saloon.Domain.Interfaces;

namespace Saloon.Infrastructure.Repositories
{
    public class ClienteObservacaoRepo : IClienteObservacaoRepo
    {
        public ClienteObservacaoRepo()
        {
//            var _clienteRepo = new ClienteRepo();
        }

        public bool Atualizar(ClienteObservacaoEnty clienteObservacao)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand($"UPDATE ClienteObservacao Set IdCliente = '{clienteObservacao.IdCliente}', Observacao = '{clienteObservacao.Observacao}'", connection);
//                var cmd = new SqlCommand("UPDATE ClienteObservacao Set IdCliente = @IdCliente, Observacao = @Observacao", connection);
//                cmd.Parameters.AddWithValue("@IdCliente", clienteObservacao.IdCliente);
//                cmd.Parameters.AddWithValue("@Observacao", clienteObservacao.Observacao);
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        public bool Cadastrar(ClienteObservacaoEnty clienteObservacao)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand($"INSERT INTO ClienteObservacao (IdCliente,Observacao) VALUES ('{clienteObservacao.IdCliente}', '{clienteObservacao.Observacao}')", connection);
//                cmd.Parameters.AddWithValue("@IdCliente", clienteObservacao.IdCliente);
//                cmd.Parameters.AddWithValue("@Observacao", clienteObservacao.Observacao);
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        public bool Deletar(int id)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand($"DELETE FROM ClienteObservacao Where Id = '{id}'", connection);
//                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        public List<ClienteObservacaoEnty> Listar(int idCliente)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand($"Select * from ClienteObservacao IdCliente = '{idCliente}'", connection);
//                cmd.Parameters.AddWithValue("@IdCliente", idCliente);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new List<ClienteObservacaoEnty>
                    {
                        new  ClienteObservacaoEnty
                        {
                            Observacao = reader.GetString(0)
                        }
                    };
                }
                return null;
            }
        }
    }
}