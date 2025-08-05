using CRUD.MVC.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD.MVC.EF.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}
