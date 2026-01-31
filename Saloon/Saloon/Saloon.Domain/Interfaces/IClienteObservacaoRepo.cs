using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saloon.Domain.Entities;

namespace Saloon.Domain.Interfaces
{
    public interface IClienteObservacaoRepo
    {
        public bool Cadastrar(ClienteObservacaoEnty clienteObservacao);
        public bool Atualizar(ClienteObservacaoEnty clienteObservacao);
        public bool Deletar(int id);
        public List<ClienteObservacaoEnty> Listar(int id);
    }
}