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

namespace CRUD__PPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesCuentasController : ControllerBase
    {
        private static readonly List<Active> Actives = new List<Active>
        {
           new Active { id= 1, ticker= "AAPL",nombre= "Apple",tipoActivo= 1,precioUnitario= 177.97M },
           new Active { id= 2,ticker= "GOOGL",nombre= "Alphabet Inc",tipoActivo= 1,precioUnitario= 138.21M},
           new Active { id= 3,ticker= "MSFT",nombre= "Microsoft",tipoActivo= 1,precioUnitario= 329.04M},
           new Active { id= 4,ticker= "KO",nombre= "Coca Cola",tipoActivo= 1,precioUnitario= 58.3M},
           new Active { id= 5,ticker= "WMT",nombre= "Walmart",tipoActivo= 1,precioUnitario= 163.42M},
           new Active { id= 6,ticker= "AL30",nombre= "BONOS ARGENTINA USD 2030 L.A",tipoActivo= 2,precioUnitario= 307.4M},
           new Active { id= 7,ticker= "GD30",nombre= "Bonos Globales Argentina USD Step Up 2030",tipoActivo= 2,precioUnitario= 336.1M},
           new Active { id= 8,ticker= "Delta.Pesos",nombre= "Delta Pesos Clase A",tipoActivo= 3,precioUnitario= 0.0181M},
           new Active { id= 9,ticker= "Fima.Premium",nombre= "Fima Premium Clase A",tipoActivo= 3,precioUnitario= 0.0317M},
        };

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
            var active = Actives.Find(x => x.ticker.Contains(addOrdenDto.Ticker));
       
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
