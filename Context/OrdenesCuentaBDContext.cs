using CRUD__PPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD__PPI.Context
{
    public class OrdenesCuentaBDContext : DbContext
    {
        public OrdenesCuentaBDContext(DbContextOptions<OrdenesCuentaBDContext> options) : base(options)
        {

        }
        public DbSet<OrdenesCuenta> OrdenesCuentas { get; set; }
        public DbSet<Active> Actives { get; set; }

        private static readonly Active[] SeedActives = 
        {
           new () { id= 1, ticker= "AAPL",nombre= "Apple",tipoActivo= 1,precioUnitario= 177.97M },
           new () { id= 2,ticker= "GOOGL",nombre= "Alphabet Inc",tipoActivo= 1,precioUnitario= 138.21M},
           new () { id= 3,ticker= "MSFT",nombre= "Microsoft",tipoActivo= 1,precioUnitario= 329.04M},
           new () { id= 4,ticker= "KO",nombre= "Coca Cola",tipoActivo= 1,precioUnitario= 58.3M},
           new () { id= 5,ticker= "WMT",nombre= "Walmart",tipoActivo= 1,precioUnitario= 163.42M},
           new () { id= 6,ticker= "AL30",nombre= "BONOS ARGENTINA USD 2030 L.A",tipoActivo= 2,precioUnitario= 307.4M},
           new () { id= 7,ticker= "GD30",nombre= "Bonos Globales Argentina USD Step Up 2030",tipoActivo= 2,precioUnitario= 336.1M},
           new () { id= 8,ticker= "Delta.Pesos",nombre= "Delta Pesos Clase A",tipoActivo= 3,precioUnitario= 0.0181M},
           new () { id= 9,ticker= "Fima.Premium",nombre= "Fima Premium Clase A",tipoActivo= 3,precioUnitario= 0.0317M},
        };

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Active>().HasData(SeedActives);
        }
    }
}
