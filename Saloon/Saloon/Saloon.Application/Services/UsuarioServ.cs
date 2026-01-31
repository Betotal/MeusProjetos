using System;
using Saloon.Domain.Interfaces;
using Saloon.Domain.Entities;
using Saloon.Domain.Enums;
namespace Saloon.Application.Services
{
    public class UsuarioServ
    {
    //[Table("USUARIO")]
        public readonly IUsuarioRepo _usuarioRepo;

        public UsuarioServ()
        {
        }

        public UsuarioServ(IUsuarioRepo usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }

        public int Gravar(string login, string senha)
        {
            var usuario = _usuarioRepo.UsuarioPorLogin(login);
            if (usuario != null)
                return usuario.ID;

            EnumSituacao status = EnumSituacao.Ativo;
            usuario = new UsuarioEnty {
                Login = login, 
                Senha = senha,
                Situacao = (int)status
            };

            return _usuarioRepo.GravarUsuario(usuario);
        }
        public bool Alterar(string login, string senha, EnumSituacao status)
        {
            var usuario = new UsuarioEnty {
                Login = login, 
                Senha = senha,
                Situacao = (int)status
                };
            _usuarioRepo.AlterarUsuario(usuario);
            return true;
        }

        public UsuarioEnty UsuarioPorId(int id) => _usuarioRepo.UsuarioPorId(id);
        public UsuarioEnty UsuarioPorLogin(string login) => _usuarioRepo.UsuarioPorLogin(login);
    }
}
