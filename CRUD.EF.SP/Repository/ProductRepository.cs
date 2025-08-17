using CRUD.EF.SP.Data;
using CRUD.EF.SP.Models;
using CRUD.EF.SP.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateProduct(Product product)
    {
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
    }

    public void DeleteProduct(int id)
    {
        Product? product = _dbContext.Products.Find(id);
        if (product != null) {
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
        }
        //throw new Exception("Product Not Found");
    }

    public List<Product> GetAllProducts()
    {
       return _dbContext.Products.ToList();

    }

    public Product GetProduct(int id)
    {
        return _dbContext.Products.Find(id);
    }

    public void UpdateProduct(Product product)
    {
        _dbContext.Products.Update(product);
        _dbContext.SaveChanges();
    }
}