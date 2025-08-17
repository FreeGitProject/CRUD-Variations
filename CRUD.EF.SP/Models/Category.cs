using System.ComponentModel.DataAnnotations;

namespace CRUD.EF.SP.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public required string Name { get; set; }

        // Navigation property (one category has many products)
        public ICollection<Product>? Products { get; set; }
    }
}
