using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using src.Enums;
using src.Context;
using src.Models;
using System.Net;

namespace src.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly VendaContext _context;

        public VendaController(VendaContext context)
        {
            _context = context;
        }

        [HttpPost("RegistrarVenda")]
        public IActionResult RegistrarVenda(Venda venda)
        {

            if (venda.Produtos == null || !venda.Produtos.Any())
                return BadRequest(new
                {
                    msg = "A venda deve conter pelo menos um produto"
        });

            _context.Vendas.Add(venda);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterVendaPorId), new { id = venda.VendaId }, venda);
        }

        [HttpGet("ObterVenda/{id}")]
        public ActionResult<Venda> ObterVendaPorId(int id)
        {
            var venda = _context.Vendas
                        .Include(v => v.DadosVendedor)
                        .Include(v => v.Produtos)
                        .Where(venda => venda.VendaId == id);

            if (!venda.Any())
                return NotFound();

            return Ok(venda);
        }

        [HttpPut("AtualizarVenda/{id}")]
        public IActionResult AtualizarVenda(int id, EnumStatusVenda novoStatus)
        {
            bool statusValido = false;
            var venda = _context.Vendas.Find(id);

            if (venda == null)
                return NotFound();
            
            if (venda.StatusVenda == EnumStatusVenda.AguardandoPagamento) {
                if (novoStatus == EnumStatusVenda.PagamentoAprovado ||
                    novoStatus == EnumStatusVenda.Cancelada)
                    {
                       statusValido = true; 
                    }
            
            } else if (venda.StatusVenda == EnumStatusVenda.PagamentoAprovado) {
                if (novoStatus == EnumStatusVenda.EnviadoParaTransportador ||
                    novoStatus == EnumStatusVenda.Cancelada)
                    {
                        statusValido = true; 
                    }

            } else if (venda.StatusVenda == EnumStatusVenda.EnviadoParaTransportador) {
                if (novoStatus == EnumStatusVenda.Entregue)
                {
                    venda.StatusVenda = novoStatus;
                }
            }

            if (statusValido)
                venda.StatusVenda = novoStatus;
            else
                return BadRequest(new { msg =
                    $"O status da venda: {venda.StatusVenda} não pode ser " +
                    $"alterado para {novoStatus}"
                });

            _context.Vendas.Update(venda);
            _context.SaveChanges();

            return Ok();
        }
    }
}