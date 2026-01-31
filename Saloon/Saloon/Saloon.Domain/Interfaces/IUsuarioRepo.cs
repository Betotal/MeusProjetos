using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saloon.Domain.Entities;

namespace Saloon.Domain.Interfaces
{
    public interface IUsuarioRepo
    {
        public int GravarUsuario(UsuarioEnty usu);
        public Boolean AlterarUsuario(UsuarioEnty usu);
        public UsuarioEnty? UsuarioPorId(int IdUsuario);
        public UsuarioEnty? UsuarioPorLogin(string login);
    }
}