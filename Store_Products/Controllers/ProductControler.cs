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
        //[HttpGet]
        //public  async Task<Product[]> ProductAll()
        //{
        //    Product[] products = new Product[1];
        //    return products;
        //}
        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
              var prodduct =   await _context.Products.Include(u =>u.Id_ProductDataImage).Include(u => u.Category_Id.Image_Category).ToListAsync();


                Parallel.ForEach(prodduct, product =>
                {
                    //if (product.Id_ProductDataImage != null)
                    //{
                        product.Id_ProductDataImage.OriginalImageData = new byte[0];
                    //}

                    //if (product.Category_Id?.Image_Category != null)
                    //{
                        product.Category_Id.Image_Category.OriginalImageData = new byte[0];
                    //}
                });

                //for (var i = 0; i < prodduct.Count; i++)
                //{



                //    prodduct[i].Id_ProductDataImage.OriginalImageData = new byte[0];

                //     prodduct[i].Category_Id.Image_Category.OriginalImageData = new byte[0];
                //}

                return prodduct;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("CategoryId{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsIdCategory(long id)
        {
            try
            {
                var prodduct = await _context.Products.Include(u => u.Id_ProductDataImage).Include(u => u.Category_Id.Image_Category).Include(u=>u.Status).Where(u =>u.Category_Id.Id == id).ToListAsync();


                Parallel.ForEach(prodduct, product =>
                {
                    //if (product.Id_ProductDataImage != null)
                    //{
                    product.Id_ProductDataImage.OriginalImageData = new byte[0];
                    //}

                    //if (product.Category_Id?.Image_Category != null)
                    //{
                    product.Category_Id.Image_Category.OriginalImageData = new byte[0];
                    //}
                });

                //for (var i = 0; i < prodduct.Count; i++)
                //{



                //    prodduct[i].Id_ProductDataImage.OriginalImageData = new byte[0];

                //     prodduct[i].Category_Id.Image_Category.OriginalImageData = new byte[0];
                //}

                return prodduct;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(long id)
        {
            var product = await _context.Products.Include(u => u.Id_ProductDataImage).Include(u => u.Category_Id.Image_Category).Include(u => u.Status).FirstOrDefaultAsync(u =>u.Id ==id);
        
            product.Id_ProductDataImage.OriginalImageData = new byte[0];
            //}

            //if (product.Category_Id?.Image_Category != null)
            //{
            product.Category_Id.Image_Category.OriginalImageData = new byte[0];
            if (product == null)
            {
                return NotFound();
            }

            return product;
                
        }


        // POST: api/Products
        [HttpPost("create")]
        public async Task<ActionResult> PostProduct([FromBody] Product product)
        {
            try
            {
                var CategoryImage = await _context.Category.Include(u => u.Image_Category.ImageCopies).FirstOrDefaultAsync(U => U.Id == product.Category_Id.Id);

                var productImage = await _context.Image.Include(u => u.ImageCopies).FirstOrDefaultAsync(U => U.Id == product.Id_ProductDataImage.Id);
                if (productImage == null)
                {
                    return BadRequest("Invalid Image data.");
                }

                var productStatus = await _context.Status.FirstOrDefaultAsync(U => U.Id == product.Status.Id);
                if (productStatus == null)
                {
                    return BadRequest("Invalid Status data.");
                }

                if (productImage != null)
                {// Если статус существует, присоединяем его к контексту

       

                    _context.Entry(productImage).State = EntityState.Unchanged;
                    product.Id_ProductDataImage = productImage;
                    _context.Entry(productStatus).State = EntityState.Unchanged;

                    product.Status = productStatus;
                    _context.Entry(CategoryImage).State = EntityState.Unchanged;
                    product.Category_Id = CategoryImage;

                }


                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                return Ok(new { id = product.Id, product });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(long id, [FromBody]  Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
