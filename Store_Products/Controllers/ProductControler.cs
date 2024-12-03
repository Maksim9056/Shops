using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopClassLibrary;
using ShopClassLibrary.ModelShop;
using Store_Products.Service;
using System.Collections.Generic;

namespace Store_Products.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductControler: ControllerBase
    {
        private readonly ProductService _productService;

        public ProductControler(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Product>>> SearchProducts(string query, string category, long? minPrice ,long maxPrice)
        {
           
            if (string.IsNullOrWhiteSpace(query) && string.IsNullOrWhiteSpace(category) && string.IsNullOrWhiteSpace(minPrice.ToString())  && string.IsNullOrWhiteSpace(maxPrice.ToString()))
            {
                return BadRequest("At least one search criterion must be specified.");
            }

            var products = await _productService.SearchProductsAsync(query, category, minPrice, maxPrice);
            return Ok(products);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("CategoryId{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoryId(long id)
        {
            var products = await _productService.GetProductsByCategoryIdAsync(id);
            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            return Ok(product);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            var addedProduct = await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = addedProduct.Id }, addedProduct);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("Product ID mismatch.");
            }

            var success = await _productService.UpdateProductAsync(product);
            if (!success)
            {
                return NotFound("Product not found for update.");
            }

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var success = await _productService.DeleteProductAsync(id);
            if (!success)
            {
                return NotFound("Product not found for deletion.");
            }

            return NoContent();
        }
 
    }
}
