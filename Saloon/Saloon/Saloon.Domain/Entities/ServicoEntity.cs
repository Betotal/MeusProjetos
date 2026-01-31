using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saloon.Domain.Enums;

namespace Saloon.Domain.Entities
{
    public class ServicoEnty
    {
        public int ID { get; set; }
        public required string Nome { get; set; }
        public decimal PrecoInicial { get; set; }
        public TimeOnly TempoMedio { get; set; }    
        public EnumSituacao Situacao { get; set; }
        public TimeOnly TempoDeEncaixe { get; set; }
    }
}