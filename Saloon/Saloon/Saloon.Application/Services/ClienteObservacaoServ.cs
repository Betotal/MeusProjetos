using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saloon.Domain.Entities;
using Saloon.Domain.Interfaces;


namespace Saloon.Application.Services
{
    public class ClienteObservacaoServ
    {

        private readonly IClienteObservacaoRepo _clienteObservacaoRepo;

        public ClienteObservacaoServ(IClienteObservacaoRepo clienteObservacaoRepo)
        {
            _clienteObservacaoRepo = clienteObservacaoRepo;
        }

        public List<ClienteObservacaoEnty> ListarObservacao(int idCliente) => _clienteObservacaoRepo.Listar(idCliente);

        public void Gravar(int idCliente, string observacao)
        {
            var clienteObs = new ClienteObservacaoEnty {
                IdCliente = idCliente, 
                Observacao = observacao
                };
            _clienteObservacaoRepo.Cadastrar(clienteObs);
        }

        public void Atualizar(int idCliente, string observacao)
        {
            var clienteObs = new ClienteObservacaoEnty {
                IdCliente = idCliente, 
                Observacao = observacao
                };
            _clienteObservacaoRepo.Atualizar(clienteObs);
        }

        public void RemoverCliente(int id) => _clienteObservacaoRepo.Deletar(id);


    }
}