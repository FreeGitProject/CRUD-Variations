using CRUD.EF.SP.Models;

namespace CRUD.EF.SP.Repository
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product GetProduct(int id);
        void DeleteProduct(int id);
        void UpdateProduct(Product product);
        void CreateProduct(Product product);
    }
}