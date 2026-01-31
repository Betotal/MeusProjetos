
using Saloon.Domain.Entities;
using Saloon.Domain.Interfaces;
using Microsoft.Data.SqlClient;

namespace Saloon.Infrastructure.Repositories
{
    public class UsuarioRepo : IUsuarioRepo
    {
        public UsuarioRepo()
        {
//            var _usuarioRepo = new UsuarioRepo();
        }
        public int GravarUsuario(UsuarioEnty usuario){
            using (var connection = new SqlConnection(Global.StringConnection)) 
            {
                connection.Open();
                var cmd = new SqlCommand($"INSERT INTO Usuario (Login, Senha, Situacao) VALUES ('{usuario.Login}','{usuario.Senha}', {(int)usuario.Situacao})", connection);
//                var cmd = new SqlCommand("INSERT INTO Usuario (Login, Senha, Situacao) VALUES (@Login, @Senha, @Situacao", connection);
//                cmd.Parameters.AddWithValue("@IdUsuario", IUsuarioRepo.Cadastrar(UsuarioEnty usuario));
//                cmd.Parameters.AddWithValue("@Login", usuario.Login);
//                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
//                cmd.Parameters.AddWithValue("@Situacao", (int)usuario.Situacao);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand($"SELECT Id FROM Usuario WHERE Login = '{usuario.Login}' and Senha = '{usuario.Senha}' and Situacao = {(int)usuario.Situacao}", connection);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetInt32(0);
                } else return 0;
           }
        }
        public Boolean AlterarUsuario(UsuarioEnty usuario){
            using (var connection = new SqlConnection(Global.StringConnection)) 
            {
                connection.Open();
                var cmd = new SqlCommand($"UPDATE Usuario Set Login = '{usuario.Login}', Senha = '{usuario.Senha}, 'Situcao ='{(int)usuario.Situacao}'", connection);
//                cmd.Parameters.AddWithValue("@IdUsuario", IUsuarioRepo.Cadastrar(UsuarioEnty usuario));
//                cmd.Parameters.AddWithValue("@Situacao", (int)usuario.Situacao);
                cmd.ExecuteNonQuery();
                return true;
            }

        }
        public UsuarioEnty UsuarioPorId(int id)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {

                connection.Open();
                var cmd = new SqlCommand($"SELECT * FROM USUARIO Where ID = {id}", connection);
//                cmd.Parameters.AddWithValue("@Id", id);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new UsuarioEnty
                    {
                        ID = reader.GetInt32(0),
                        Login = reader.GetString(1),
                        Senha = reader.GetString(2)
                    };
                }
                return null;
            }
        }

        public UsuarioEnty UsuarioPorLogin(string login)
        {
            using (var connection = new SqlConnection(Global.StringConnection))
            {
                connection.Open();
                var cmd = new SqlCommand($"SELECT * FROM USUARIO Where Login = '{login}'", connection);
//                cmd.Parameters.AddWithValue("@Id", id);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new UsuarioEnty
                    {
                        ID = reader.GetInt32(0),
                        Login = reader.GetString(1),
                        Senha = reader.GetString(2)
                    };
                }
                return null;
            }
        }
        // Implement the methods of IUsuarioRepo here
    }
}
