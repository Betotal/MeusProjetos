using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saloon.Domain.Enums;

namespace Saloon.Domain.Entities
{
    public class AgendaEnty
    {
        public int ID { get; set; }
        public DateOnly AgendaData { get; set; }    
        public TimeOnly AgendaHora { get; set; }
        public EnumDiaSemana DiaSemana { get; set; }
        public int IdProfissional { get; set; }
        public int IdCliente { get; set; }
        public int IdServico { get; set; } 
        public int IdClientePromocao { get; set; }
        public EnumSituacaoAgenda AgendaSituacao { get; set; }
    }
}