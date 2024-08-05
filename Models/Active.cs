namespace CRUD__PPI.Models
{
    public class Active
    {
        public int id { get; set; }
        public required string nombre { get; set; }
        public required string ticker { get; set; }    
        public required int tipoActivo { get; set; }
        public required decimal precioUnitario { get; set; }

    }
}
