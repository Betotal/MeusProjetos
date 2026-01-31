using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saloon.Domain.Entities
{
    public class ClienteObservacaoEnty
    {
        public int ID { get; set; }
        public int IdCliente { get; set; }
        public string? Observacao { get; set; }
    }
}