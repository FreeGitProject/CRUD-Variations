using CRUD.EF.SP.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUD.EF.SP.DTOs.Products
{
    public class ProductDetailDto
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
