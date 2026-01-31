using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saloon.Domain.Entities
{
    public class ClientePromocaoEnty
    {
        public int ID { get; set; }
        public int IdCliente { get; set; }
        public int IdPromocao { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public DateOnly PromocaoValidade { get; set; }  
    }
}