using System.ComponentModel.DataAnnotations;

namespace CRUD__PPI.Models
{
    public class AddOrdenDto
    {

        [Required]
        [StringLength(32, MinimumLength = 3)]
        public string Ticker { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public char Operation { get; set; }
        public decimal Price { get; set; }
    }
}
