using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saloon.Domain.Entities
{
    public class ProfissionalHorarioEnty
    {
        public int ID { get; set; }
        public int IdProfissional { get; set; }
        public int IdHorario { get; set; }
    }
}