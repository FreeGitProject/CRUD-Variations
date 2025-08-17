using CRUD.EF.SP.Models;
using CRUD.EF.SP.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.EF.SP.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<HomeController> _logger;

        public ProductController(IProductRepository productRepository, ILogger<HomeController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //IFormCollection collection
        public ActionResult Create(Product product)
        {
            try
            {
                _productRepository.CreateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            Product product = _productRepository.GetProduct(id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)//IFormCollection collection)
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
    }
}
