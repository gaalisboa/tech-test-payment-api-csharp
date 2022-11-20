using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using src.Enums;

namespace src.Models
{
    public class Venda
    {
        public Venda()
        {
            DadosVendedor = new Vendedor();
            Produtos = new List<Produto>();
            Data = DateTime.Now;
            StatusVenda = EnumStatusVenda.AguardandoPagamento;
        }


        public int VendaId { get; set; }
        public Vendedor DadosVendedor { get; set; }
        public DateTime Data { get; set; }
        public List<Produto> Produtos { get; set; }
        public EnumStatusVenda StatusVenda { get; set; }
    }
}