using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Win32.SafeHandles;

namespace Saloon.Domain.Entities;

public class ClienteEnty
{
    private int _ID { get; set; }
    private int _IdUsuario { get; set; }
    private string? _Nome { get; set; }
    private string? _Telefone { get; set; }
    private DateOnly _Aniversario { get; set; }

    public ClienteEnty()
    {
        
    }
    public ClienteEnty(string nome, string telefone, string aniversario)
    {
        this.Nome = !string.IsNullOrWhiteSpace(nome)? nome: "Sem nome";
        this.Telefone = !string.IsNullOrWhiteSpace(telefone)? telefone: "987654321";
        this.Aniversario = DateOnly.TryParseExact(aniversario, "dd/MM/yyyy", out DateOnly result) ? result : default;
    } 

    public int ID { get => _ID; set => _ID = value; }
    public int IdUsuario { get => _IdUsuario; set => _IdUsuario = value; }

    public string Nome
    {
        get => _Nome;
        set => _Nome = value;
    }

    public string Telefone
    {
        get => _Telefone;
        set => _Telefone = value;
    }

    public DateOnly Aniversario
    {
        get => _Aniversario;
        set => _Aniversario = value;
    }

}
