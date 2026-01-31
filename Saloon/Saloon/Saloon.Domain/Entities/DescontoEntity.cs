using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saloon.Domain.Entities
{
    public class DescontoEnty
    {
        public int ID { get; set; }
        public int IdCliente { get; set; }  
        public int PorcentagemDesconto { get; set; }
    }
}