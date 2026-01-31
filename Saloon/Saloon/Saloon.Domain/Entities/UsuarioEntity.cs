using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Saloon.Domain.Enums;

namespace Saloon.Domain.Entities
{
    
    //[Table("USUARIO")]
    public class UsuarioEnty
    {
    //    [Key]
        public int ID { get; set; }
    //    [Column("LOGIN")]
        public required string Login { get; set; }
    //    [Column("SENHA")]
        public required string Senha { get; set; }
    //    [Column("SITUACAO")]
        public int Situacao { get; set; }
    
        public UsuarioEnty()
        {
        
        }
        public UsuarioEnty(string login, string senha, int situacao)
        {
            this.Login = !string.IsNullOrWhiteSpace(login)? login: "Sem login";
            this.Senha = !string.IsNullOrWhiteSpace(senha)? senha: "123456";
            this.Situacao = 1;
        }
}

}
