using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class Vendedor
    {
        public Vendedor()
        {
            this.Cpf = "CPF";
            this.Nome = "NOME";
            this.Email = "EMAIL";
            this.Telefone = "TELEFONE";
        }


        public int VendedorId { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}