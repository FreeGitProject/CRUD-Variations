using CRUD.EF.SP.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD.EF.SP.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
                
        }
       public DbSet<Product> Products { get; set; }
    }
}
