using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saloon.Domain.Entities
{
    public class HorarioEnty
    {
        public int ID { get; set; }
        public TimeOnly HorarioInicial { get; set; }    
        public TimeOnly HorarioFinal { get; set; }  
    }
}