using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ShopClassLibrary.ModelShop;
using System.Collections.Generic;

namespace Store_Products.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductControler: ControllerBase
    {
        [HttpGet]
        public  async Task<Product[]> ProductAll()
        {
            Product[] products = new Product[1];
            return products;
        }


        [HttpPost]
        public IActionResult ProductCreate([FromBody] Product Product)
        {

            return Ok($"POST-запрос успешно обработан! Данные: {Product.Products}, {Product.ProductCount}");
        }

    }
}
