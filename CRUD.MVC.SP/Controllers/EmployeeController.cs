using CRUD.MVC.SP.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.MVC.SP.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDAL _dal;

        public EmployeeController(IConfiguration configuration)
        {
            _dal = new EmployeeDAL(configuration);
        }

        //public IActionResult Index()
        //{
        //    return View(_dal.GetAll());
        //}

        public IActionResult Index(string searchTerm, int page = 1)
        {
            int pageSize = 5;
            var result = _dal.GetAllPagination(searchTerm, page, pageSize);
            ViewBag.SearchTerm = searchTerm;
            return View(result);
        }

        public IActionResult Details(int id)
        {
            return View(_dal.GetById(id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _dal.Insert(emp);
                return RedirectToAction("Index");
            }
            return View(emp);
        }

        public IActionResult Edit(int id)
        {
            return View(_dal.GetById(id));
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _dal.Update(emp);
                return RedirectToAction("Index");
            }
            return View(emp);
        }

        public IActionResult Delete(int id)
        {
            return View(_dal.GetById(id));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _dal.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
