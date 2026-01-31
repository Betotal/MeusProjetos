namespace Saloon.Domain.Entities;

public class ProfissionalEnty
{
    public int ID { get; set; }
    public int IdCliente { get; set; }
    public decimal SalarioFixo { get; set; }
    public int PorcentagemComissao { get; set; }
}
