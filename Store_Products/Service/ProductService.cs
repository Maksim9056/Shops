using ShopClassLibrary.ModelShop;
using ShopClassLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using System.Threading.Tasks;

namespace Store_Products.Service
{
    public class ProductService
    {
        private readonly ShopData _context;
        private readonly IDistributedCache _cache;

        public ProductService(ShopData context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var products = await _context.Products
                .AsNoTracking() // Улучшаем производительность, убирая отслеживание
                .Include(u => u.Id_ProductDataImage)
                .Include(u => u.Category_Id.Image_Category)
                .ToListAsync();

            Parallel.ForEach(products, product =>
            {
                product.Id_ProductDataImage.OriginalImageData = Array.Empty<byte>(); ;
                product.Category_Id.Image_Category.OriginalImageData = Array.Empty<byte>();
            });

            return products;
        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(long categoryId)
        {
            var cacheKey = $"Category_{categoryId}_Products";
            var cachedProducts = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedProducts))
            {
                return JsonConvert.DeserializeObject<List<Product>>(cachedProducts);
            }

            var products = await _context.Products
                .AsNoTracking() // Улучшаем производительность, убирая отслеживание
                .Include(u => u.Id_ProductDataImage)
                .Include(u => u.Category_Id.Image_Category)
                .Include(u => u.Status)
                .Where(u => u.Category_Id.Id == categoryId)
                .ToListAsync();

            Parallel.ForEach(products, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, product =>
            {
                product.Id_ProductDataImage.OriginalImageData = Array.Empty<byte>();
                product.Category_Id.Image_Category.OriginalImageData = Array.Empty<byte>();
            });

            // Кэшируем результат для этой категории
            await CacheListAsync(cacheKey, products, TimeSpan.FromMinutes(10));

            return products;
        }

        public async Task<List<Product>> SearchProductsAsync(string query, string category, long? minPrice, long? maxPrice)
        {
            var cacheKey = $"Search_{query}_{category}_{minPrice}_{maxPrice}_Products";
            var cachedProducts = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedProducts) && cachedProducts != "[]")
            {
                return JsonConvert.DeserializeObject<List<Product>>(cachedProducts) ?? new List<Product>();
            }

            if (minPrice.HasValue && maxPrice.HasValue && minPrice > maxPrice)
            {
                // Меняем местами minPrice и maxPrice
                var temp = minPrice;
                minPrice = maxPrice;
                maxPrice = temp;
            }
            var productsQuery = await _context.Products
                .AsNoTracking()
                 .Include(p => p.Id_ProductDataImage)
                .Include(u => u.Status)
                .Include(p => p.Category_Id.Image_Category)
                  .Where(p =>
        (string.IsNullOrEmpty(query) || p.Name_Product.ToLower().Contains(query.Trim().ToLower())) &&
        (string.IsNullOrEmpty(category) || p.Category_Id.Name_Category.Trim() == category.Trim()) &&
        (!minPrice.HasValue || p.ProductPrice >= Convert.ToInt64(minPrice.Value)) &&
        (!maxPrice.HasValue || p.ProductPrice <= Convert.ToInt64(maxPrice.Value)))
    .ToListAsync();

            var products = productsQuery;

            Parallel.ForEach(products, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, product =>
            {
                product.Id_ProductDataImage.OriginalImageData = Array.Empty<byte>();
                product.Category_Id.Image_Category.OriginalImageData = Array.Empty<byte>();
            });

            await CacheListAsync(cacheKey, products, TimeSpan.FromMinutes(10));
            return products;

            // Кэшируем результат поиска
        }

        public async Task<Product> GetProductByIdAsync(long id)
        {
            var cacheKey = $"Product_{id}";
            var cachedProduct = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedProduct))
            {
                return JsonConvert.DeserializeObject<Product>(cachedProduct);
            }

            var product = await _context.Products
                .AsNoTracking() // Улучшаем производительность, убирая отслеживание
                .Include(p => p.Id_ProductDataImage)
                .Include(p => p.Category_Id.Image_Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product != null)
            {
                product.Id_ProductDataImage.OriginalImageData = new byte[0];
                product.Category_Id.Image_Category.OriginalImageData = new byte[0];

                await CacheProductAsync(cacheKey, product); // Кэшируем продукт
            }

            return product;
        }

        public async Task<Product> AddProductAsync(Product product)
        {

            var CategoryImage = await _context.Category.Include(u => u.Image_Category.ImageCopies).FirstOrDefaultAsync(U => U.Id == product.Category_Id.Id);

            var productImage = await _context.Image.Include(u => u.ImageCopies).FirstOrDefaultAsync(U => U.Id == product.Id_ProductDataImage.Id);
            if (productImage == null)
            {
                return  new Product();
            }

            var productStatus = await _context.Status.FirstOrDefaultAsync(U => U.Id == product.Status.Id);
            if (productStatus == null)
            {
                return new Product();
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



            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Очистка кэша категорий для обновления данных
            var cacheKey = $"Category_{product.Category_Id.Id}_Products";
            await _cache.RemoveAsync(cacheKey);

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

            // Обновляем кэш продукта и категорий
            var cacheKey = $"Product_{product.Id}";
            await CacheProductAsync(cacheKey, product); // Обновление кэша продукта
            await _cache.RemoveAsync($"Category_{product.Category_Id.Id}_Products"); // Очистка кэша категории

            return true;
        }

        public async Task<bool> DeleteProductAsync(long id)
        {
            var product = await GetProductByIdAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            // Удаляем продукт из кэша
            var cacheKey = $"Product_{id}";
            await _cache.RemoveAsync(cacheKey);

            // Очистка кэша категории
            await _cache.RemoveAsync($"Category_{product.Category_Id.Id}_Products");

            return true;
        }

        private async Task CacheProductAsync(string cacheKey, Product product)
        {
            var productJson = JsonConvert.SerializeObject(product);
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };

            await _cache.SetStringAsync(cacheKey, productJson, cacheOptions);
        }

        private async Task CacheListAsync<T>(string cacheKey, List<T> list, TimeSpan expiration)
        {
            var listJson = JsonConvert.SerializeObject(list);
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };

            await _cache.SetStringAsync(cacheKey, listJson, cacheOptions);
        }
    }
}
