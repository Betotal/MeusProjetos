using System.Collections.Generic;
using Saloon.Domain.Entities;
using Saloon.Domain.Interfaces;

namespace Saloon.Application.Services
{
    public class CargoServ
    {
        private readonly ICargoRepo _cargoRepo;

        public CargoServ(ICargoRepo cargoRepo)
        {
            _cargoRepo = cargoRepo;
        }

        public CargoEnty? Cadastrar(string nome)
        {
            var cargo = new CargoEnty
            {
                Nome = nome
            };

            int id = _cargoRepo.Cadastrar(cargo);
            return _cargoRepo.ConsultarPorId(id);
        }

        public List<CargoEnty> Listar()
        {
            return _cargoRepo.Listar();
        }

        public bool Deletar(int id)
        {
            return _cargoRepo.Deletar(id);
        }
    }
}