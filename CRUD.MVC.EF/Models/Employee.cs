using System.ComponentModel.DataAnnotations;

namespace CRUD.MVC.EF.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Position { get; set; }

        public decimal Salary { get; set; }
    }
}
