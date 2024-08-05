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
    }
}