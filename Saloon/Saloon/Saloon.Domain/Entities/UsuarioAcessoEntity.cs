using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saloon.Domain.Entities
{
    public class UsuarioAcessoEnty
    {
        public int ID { get; set; }
        public int IdUsuario { get; set; }
        public int IdAcesso { get; set; }
    }
}