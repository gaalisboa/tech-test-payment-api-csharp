using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using src.Models;

namespace src.Context
{
    public class VendaContext : DbContext
    {
        public VendaContext(DbContextOptions<VendaContext> options) : base(options)
        {
            
        }

        public DbSet<Venda>? Vendas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Venda>(tabela =>
            {
                tabela.HasKey(venda => venda.VendaId);
                tabela.HasMany(venda => venda.Produtos);
                tabela.HasOne(venda => venda.DadosVendedor);
            });
        }
    }
}