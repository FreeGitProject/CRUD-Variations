using CRUD.EF.SP.Data;
using CRUD.EF.SP.Models;
using CRUD.EF.SP.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace CRUD.EF.SP.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public ProductController(IProductRepository productRepository, ILogger<HomeController> logger, IConfiguration configuration, ApplicationDbContext context)
        {
            _productRepository = productRepository;
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }

        public ActionResult Index()
        {
            _logger.LogInformation("Get Product List");
            List<Product> products = _productRepository.GetAllProducts();
            return View(products);
        }

        public ActionResult Details(int id)
        {
            Product product = _productRepository.GetProduct(id);
            return View(product);
        }

        public ActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //IFormCollection collection
        public ActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _productRepository.CreateProduct(product);
                    return RedirectToAction(nameof(Index));
                }
               
            }
            catch
            {
                return View();
            }
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        public ActionResult Edit(int id)
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            Product product = _productRepository.GetProduct(id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)//IFormCollection collection)
        {
            try
            {
                _productRepository.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            Product product = _productRepository.GetProduct(id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _productRepository.DeleteProduct(id);   
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public JsonResult GetCategories() 
        {
            List<Category> categories = _context.Categories.ToList();

            return Json(categories);

        }

        [HttpPost]
        public JsonResult IsProductNameAvailable(string name)
        {
            bool exists = false;
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand("procCheckProductExists",con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", name);

                    con.Open();
                    var result = cmd.ExecuteScalar();

                    exists = Convert.ToBoolean(result); 
                }
            }
            return Json(exists); 
        }

    }
}
