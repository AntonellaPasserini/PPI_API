using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CRUD__PPI.Models
{
    public class OrdenesCuenta
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Id_Accaunt { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        public int Quanity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public char Operation { get; set; }
        public int Status { get; set; }

        public decimal Total_Amount { get; set; }


    }
}
