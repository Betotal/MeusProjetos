using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saloon.Domain.Enums;

namespace Saloon.Domain.Entities
{
    public class AcessoEnty
    {
        public int ID { get; set; } 
        public EnumAcesso NivelAcesso { get; set; } 
    }
}