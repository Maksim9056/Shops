using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            try
            {


                await _context.Products.AddAsync(Product);
                await _context.SaveChangesAsync();
              Product  =   await _context.Products.Include(u =>u.Status).FirstOrDefaultAsync(u =>u.Name_Product == Product.Name_Product && u.ProductsDescription == Product.ProductsDescription);
            } catch (Exception ex)
            {
                Problem(ex.Message);
            }finally
            {

            }

            return CreatedAtAction($"POST-запрос успешно обработан! Данные: {Product.Name_Product}, {Product.ProductCount}", Product);
        }

    }
}
