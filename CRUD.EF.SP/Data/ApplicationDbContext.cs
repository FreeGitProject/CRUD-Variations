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
       public DbSet<Category> Categories { get; set; }

        /*
        How EF Core Knows It’s a Foreign Key
        EF sees the pattern CategoryId + navigation property Category → automatically sets up a foreign key.
        This works because of convention: EF Core looks for <NavigationPropertyName>Id.
        So here:
        Navigation property = Category
        Foreign key property = CategoryId
        EF makes the link automatically.

         */
        //Explicit Configuration (if you want to be 100% clear)
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Product>()
        //        .HasOne(p => p.Category)
        //        .WithMany(c => c.Products)
        //        .HasForeignKey(p => p.CategoryId);
        //}

    }
}
