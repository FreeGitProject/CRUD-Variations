using System.ComponentModel.DataAnnotations;

namespace CRUD.EF.SP.Models
{
    public class Product
    {

        public int Id { get; set; }
        [Required,MaxLength(500),MinLength(3)]
        public required string Name { get; set; }
        [Required]
        public decimal SellPrice { get; set; }
        [Required, Range(1, 100)]
        public int Inventory { get; set; }

        // Foreign Key
        [Required]
        public int CategoryId { get; set; }

        // Navigation Property
        public Category? Category { get; set; }
    }
}
