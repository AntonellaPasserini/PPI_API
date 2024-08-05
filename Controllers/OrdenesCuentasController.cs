using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUD__PPI.Context;
using CRUD__PPI.Models;
using System.Diagnostics.Metrics;
using System.IO;
using Humanizer;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CRUD__PPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesCuentasController : ControllerBase
    {

        private readonly OrdenesCuentaBDContext _context;

        public OrdenesCuentasController(OrdenesCuentaBDContext context)
        {
            _context = context;      
        }

        // GET: api/OrdenesCuentas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenesCuenta>>> GetOrdenesCuentas()
        {
            return await _context.OrdenesCuentas.ToListAsync();
        }

        // GET: api/OrdenesCuentas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenesCuenta>> GetOrdenesCuenta(int id)
        {
            var ordenesCuenta = await _context.OrdenesCuentas.FindAsync(id);

            if (ordenesCuenta == null)
            {
                return NotFound();
            }

            return ordenesCuenta;
        }

        // PUT: api/OrdenesCuentas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/status")]
        public async Task<IActionResult> PutStatusDTO(int id, string status)
        {
            var ordenesCuenta = await _context.OrdenesCuentas.FindAsync(id);
            
            if (id != ordenesCuenta?.Id)
            {
                return BadRequest();
            }

            if (status != null) { 
            if (status.ToLower() != "cancel" && status.ToLower() != "done")
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }


            ordenesCuenta.Status = status.ToLower() == "done" ? 1 : 3;

            _context.Entry(ordenesCuenta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenesCuentaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrdenesCuentas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IEnumerable<AddOrdenDto>>> AddOrdenDto(AddOrdenDto addOrdenDto)
        {
            if (!Char.ToLower(addOrdenDto.Operation).Equals("c") || !Char.ToLower(addOrdenDto.Operation).Equals("v"))
            {
                BadRequest();
            }
            var precio = 0M;
            var parcialAmount = 0M;
            var totalAmount = 0M;
            var commisions = 0M;
            var active = _context.Actives.Where(c => c.ticker == addOrdenDto.Ticker).FirstOrDefault();
       
            switch (active?.tipoActivo)
            {
                case 1:
                    if (addOrdenDto.Quantity > 0) { 
                    precio = (active.precioUnitario);
                    parcialAmount = precio * addOrdenDto.Quantity;
                    commisions = parcialAmount * (6/10) / 100;
                    totalAmount = (int)(parcialAmount + commisions + commisions * 21  / 100);
                    break;
                    }
                    BadRequest();
                    break;
                case 2:
                    if (addOrdenDto.Price > 0)
                    {

                        if (addOrdenDto.Quantity > 0)
                        {
                            precio = addOrdenDto.Price;
                            parcialAmount = addOrdenDto.Price * addOrdenDto.Quantity;
                            commisions = parcialAmount * (2/100);
                            totalAmount = parcialAmount + commisions + commisions * 21 / 100;
                            break;
                        }
                        BadRequest();
                        break;
                    }
                    BadRequest();
                    break;

                case 3:
                    if (addOrdenDto.Quantity > 0)
                    {
                        precio = active.precioUnitario;
                        totalAmount = precio * addOrdenDto.Quantity;
                        break;
                    }
                    BadRequest();
                    break;
                default:
                    BadRequest();
                    break;
            }
            var ordenesCuenta = new OrdenesCuenta()
            {
                Id_Accaunt = active.id,
                Name = active.nombre,    
                Quanity = addOrdenDto.Quantity,  
                Operation = Char.ToLower(addOrdenDto.Operation),
                Status = 0,
                Price = precio,
                Total_Amount = totalAmount
            };
            _context.OrdenesCuentas.Add(ordenesCuenta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrdenesCuenta), new { id = ordenesCuenta.Id }, ordenesCuenta);
        }

        // DELETE: api/OrdenesCuentas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdenesCuenta(int id)
        {
            var ordenesCuenta = await _context.OrdenesCuentas.FindAsync(id);
            if (ordenesCuenta == null)
            {
                return NotFound();
            }

            _context.OrdenesCuentas.Remove(ordenesCuenta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdenesCuentaExists(int id)
        {
            return _context.OrdenesCuentas.Any(e => e.Id == id);
        }
    }
}
