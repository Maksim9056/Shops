using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ShopClassLibrary;
using ShopClassLibrary.ModelShop;
using System.Collections.Generic;

namespace Store_Products.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductControler: ControllerBase
    {
        private readonly ShopData _context;
        public ProductControler(ShopData context)
        {
            _context = context;
        }
        [HttpGet]
        public  async Task<Product[]> ProductAll()
        {
            Product[] products = new Product[1];
            return products;
        }


        [HttpPost]
        public async Task<IActionResult> ProductCreate([FromBody] Product Product)
        {
            await  _context.AddAsync(Product);

            return Ok($"POST-запрос успешно обработан! Данные: {Product.Products}, {Product.ProductCount}");
        }

    }
}
