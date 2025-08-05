using CRUD.MVC.EF.Data;
using CRUD.MVC.EF.Interfaces;
using CRUD.MVC.EF.Models;

namespace CRUD.MVC.EF.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAll() => _context.Employees.ToList();
        public Employee GetById(int id) => _context.Employees.Find(id);
        public void Add(Employee employee) => _context.Employees.Add(employee);
        public void Update(Employee employee) => _context.Employees.Update(employee);
        public void Delete(int id)
        {
            var emp = GetById(id);
            if (emp != null) _context.Employees.Remove(emp);
        }
        public void Save() => _context.SaveChanges();
    }
}
