using ShopClassLibrary.ModelShop;
using ShopClassLibrary;
using Microsoft.EntityFrameworkCore;

namespace Store_Products.Service
{
    public class ProductService
    {
        private readonly ShopData _context;

        public ProductService(ShopData context)
        {
            _context = context;
        }

        public async Task<List<Product>> SearchProductsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<Product>();
            }

            return await _context.Products
                .Include(p => p.Id_ProductDataImage)
                .Include(p => p.Category_Id.Image_Category)
                .Where(p => p.Name_Product.Contains(query))
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(long id)
        {
            return await _context.Products
                .Include(p => p.Id_ProductDataImage)
                .Include(p => p.Category_Id.Image_Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var existingProduct = await GetProductByIdAsync(product.Id);
            if (existingProduct == null) return false;

            existingProduct.Name_Product = product.Name_Product;
            existingProduct.ProductsDescription = product.ProductsDescription;
            existingProduct.ProductPrice = product.ProductPrice;
            existingProduct.Category_Id = product.Category_Id;

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await GetProductByIdAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

