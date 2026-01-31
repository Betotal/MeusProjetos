using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saloon.Domain.Entities;

namespace Saloon.Domain.Interfaces
{
    public interface IClienteRepo
    {
        public int Cadastrar(ClienteEnty cliente, int idUsuario);
        public bool Atualizar(ClienteEnty cliente);
        public bool Deletar(int id);
        public ClienteEnty? ConsultarPorId(int id);
        public ClienteEnty? ConsultarPorNome(string nome);
        public List<ClienteEnty> Listar();
        public List<ClienteEnty> ListarPorNome(string nome);
        
    }
}