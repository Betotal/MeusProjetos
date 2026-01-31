using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saloon.Domain.Entities
{
    public class ServicoConjugadoEnty
    {
        public int ID { get; set; }
        public int IdServicoPrincipal { get; set; } 
        public int IdServicoConjugado1 { get; set; }
        public int IdServicoConjugado2 { get; set; }
        public int PorcentagemDesconto1 { get; set; }
        public int PorcentagemDesconto2 { get; set; }

    }
}