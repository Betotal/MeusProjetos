using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Saloon.Domain.Entities;
using Saloon.Domain.Interfaces;

namespace Saloon.Infrastructure.Repositories
{
    public class ClienteRepo : IClienteRepo
    {
        public ClienteRepo()
        {
//            var _clienteRepo = new ClienteRepo();
        }
        bool IClienteRepo.Atualizar(ClienteEnty cliente)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand("UPDATE CLIENTE SET Nome = @Nome, Telefone = @Telefone, Aniversario = @Aniversario WHERE ID = @ID", connection);
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@Telefone", (object)cliente.Telefone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Aniversario", cliente.Aniversario.ToDateTime(TimeOnly.MinValue));
                cmd.Parameters.AddWithValue("@ID", cliente.ID);
                
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        int IClienteRepo.Cadastrar(ClienteEnty cliente, int idUsuario)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand("INSERT INTO CLIENTE (IdUsuario, Nome, Telefone, Aniversario) VALUES (@IdUsuario, @Nome, @Telefone, @Aniversario); SELECT SCOPE_IDENTITY();", connection);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@Telefone", (object)cliente.Telefone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Aniversario", cliente.Aniversario.ToDateTime(TimeOnly.MinValue));
                
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        ClienteEnty? IClienteRepo.ConsultarPorId(int id)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand("SELECT ID, IdUsuario, Nome, Telefone, Aniversario FROM CLIENTE WHERE ID = @Id", connection);
                cmd.Parameters.AddWithValue("@Id", id);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new ClienteEnty
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("ID")),
                        IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                        Nome = reader.GetString(reader.GetOrdinal("Nome")),
                        Telefone = reader.IsDBNull(reader.GetOrdinal("Telefone")) ? null : reader.GetString(reader.GetOrdinal("Telefone")),
                        Aniversario = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("Aniversario")))
                    };
                }
                return null;
            }
        }

        ClienteEnty? IClienteRepo.ConsultarPorNome(string nome)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand("SELECT ID, IdUsuario, Nome, Telefone, Aniversario FROM CLIENTE WHERE Nome = @Nome", connection);
                cmd.Parameters.AddWithValue("@Nome", nome);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new ClienteEnty
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("ID")),
                        IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                        Nome = reader.GetString(reader.GetOrdinal("Nome")),
                        Telefone = reader.IsDBNull(reader.GetOrdinal("Telefone")) ? null : reader.GetString(reader.GetOrdinal("Telefone")),
                        Aniversario = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("Aniversario")))
                    };
                }
                return null;
            }
        }

        bool IClienteRepo.Deletar(int id)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand("DELETE FROM CLIENTE WHERE ID = @Id", connection);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        List<ClienteEnty> IClienteRepo.Listar()
        {
            throw new NotImplementedException();
        }

        List<ClienteEnty> IClienteRepo.ListarPorNome(string nome)
        {
            throw new NotImplementedException();
        }
    }
}
