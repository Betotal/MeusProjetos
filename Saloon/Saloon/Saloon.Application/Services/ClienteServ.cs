using System;
using System.Collections.Generic;

using Saloon.Domain.Entities;
using Saloon.Domain.Interfaces;    


namespace Saloon.Application.Services
{
    public class ClienteServ
    {
        //verificar se existe o cliente

        //verificar se os campos estão preenchidos

        //regra do negócio
        // metodos da interface 

        private readonly IClienteRepo _clienteRepo;
        private readonly UsuarioServ _usuarioServ;

        public ClienteServ(IClienteRepo clienteRepo, UsuarioServ usuarioServ)
        {
            _clienteRepo = clienteRepo;
            _usuarioServ = usuarioServ;
        }

        public List<ClienteEnty> ListarClientes() => _clienteRepo.Listar();

        public ClienteEnty ConsultarId(int id) => _clienteRepo.ConsultarPorId(id);
        public ClienteEnty ConsultarNome(string nome) => _clienteRepo.ConsultarPorNome(nome);

        public int Gravar(string nome, string telefone, DateOnly aniversario)
        {
            int IdUsuario = _usuarioServ.Gravar(nome, nome);

            var cliente = _clienteRepo.ConsultarPorNome(nome);
            if (cliente != null)
                return cliente.ID;

            cliente = new ClienteEnty {
                Nome = nome, 
                Telefone = telefone,
                Aniversario = aniversario,
                IdUsuario = IdUsuario
                };
            return _clienteRepo.Cadastrar(cliente, IdUsuario);
        }
        public void Atualizar(string nome, string telefone, DateOnly aniversario)
        {
            var cliente = new ClienteEnty {
                Nome = nome, 
                Telefone = telefone,
                Aniversario = aniversario
                };
            _clienteRepo.Atualizar(cliente);
        }

        public void RemoverCliente(int id) => _clienteRepo.Deletar(id);
    }   
}