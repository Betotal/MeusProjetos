using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Saloon.Domain.Enums;

namespace Saloon.Domain.Entities
{
    public class PromocaoEnty
    {
        public int ID { get; set; }
        public required string Nome { get; set; }
        public DateOnly PromocaoInicial { get; set; }   
        public DateOnly PromocaoFinal { get; set; }
        public int IdServico { get; set; }
        public int QuantidadeServico { get; set; }
        public int PorcentagemDesconto { get; set; }
        public EnumDiaSemana DiaSemanaInicial { get; set; }
        public EnumDiaSemana DiaSemanaFinal { get; set; }
        public EnumQuemPaga QuemPaga { get; set; }
    }
}