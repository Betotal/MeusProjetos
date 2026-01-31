using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saloon.Domain.Entities
{
    public class ProfissionalCargoEnty
    {
        public int ID { get; set; }
        public int IdProfissional { get; set; }
        public int IdCargo { get; set; }    
    }
}