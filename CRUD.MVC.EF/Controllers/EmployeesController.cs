using CRUD.MVC.EF.Interfaces;
using CRUD.MVC.EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.MVC.EF.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _repo;

        public EmployeesController(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var employees = _repo.GetAll();
            return View(employees);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(employee);
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            var emp = _repo.GetById(id);
            return emp == null ? NotFound() : View(emp);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(employee);
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            var emp = _repo.GetById(id);
            return emp == null ? NotFound() : View(emp);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            _repo.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var emp = _repo.GetById(id);
            return emp == null ? NotFound() : View(emp);
        }
    }
}
