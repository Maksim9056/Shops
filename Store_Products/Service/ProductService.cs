using ShopClassLibrary.ModelShop;
using ShopClassLibrary;
using Microsoft.EntityFrameworkCore;

namespace Store_Products.Service
{
    public class ProductService
    {
        private readonly ShopData _context;
        private readonly ProductProducer _producer;
        private readonly ProductConsumer _consumer;

        public ProductService(ShopData context, ProductProducer producer, ProductConsumer consumer)
        {
            _context = context;
            _producer = producer;
            _consumer = consumer;
        }
        public async Task<List<Product>> GetProductsAsync()
        {
            var products = await _context.Products.Include(u => u.Id_ProductDataImage)
                .Include(u => u.Category_Id.Image_Category)
                .ToListAsync();

            Parallel.ForEach(products, product =>
            {
                product.Id_ProductDataImage.OriginalImageData = new byte[0];
                product.Category_Id.Image_Category.OriginalImageData = new byte[0];
            });

            return products;
        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(long categoryId)
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10)); // Устанавливаем тайм-аут в 10 секунд

            // Сначала пытаемся получить продукт из Kafka
            var product = await _consumer.GetProductListFromKafkaAsync(categoryId, cts.Token);
            if (product != null)
            {
                return product;
            }

            var products = await _context.Products
                .Include(u => u.Id_ProductDataImage)
                .Include(u => u.Category_Id.Image_Category)
                .Include(u => u.Status)
                .Where(u => u.Category_Id.Id == categoryId)
                .ToListAsync();

            //// Сохраняем результат в Kafka
            //foreach (var product in products)
            //{
            //    await _producer.SendProductUpdateAsync(product);
            //}

            Parallel.ForEach(products, product =>
            {
                product.Id_ProductDataImage.OriginalImageData = new byte[0];
                product.Category_Id.Image_Category.OriginalImageData = new byte[0];
            });

            return products;
        }
        public async Task<List<Product>> SearchProductsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<Product>();
            }
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

            // Пытаемся найти продукты через Kafka
            var products = await _consumer.GetProductsByQueryAsync(query, cts.Token);
            if (products != null && products.Any())
            {
                return products;
            }

            // Если в Kafka данные не найдены, выполняем поиск в БД
            products = await _context.Products
                .Include(p => p.Id_ProductDataImage)
                .Include(p => p.Category_Id.Image_Category)
                .Where(p => p.Name_Product.Contains(query))
                .ToListAsync();

            // Сохраняем результат в Kafka
            foreach (var product in products)
            {
                await _producer.SendProductUpdateAsync(product);
            }

            return products;
        }

        public async Task<Product> GetProductByIdAsync(long id)
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

            // Сначала пытаемся получить продукт из Kafka
            var product = await _consumer.GetProductFromKafkaAsync(id, cts.Token);
            if (product != null)
            {
                return product;
            }

            // Если продукт не найден в Kafka, получаем его из БД и сохраняем в Kafka
            product = await _context.Products
                .Include(p => p.Id_ProductDataImage)
                .Include(p => p.Category_Id.Image_Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product != null)
            {
                await _producer.SendProductUpdateAsync(product);
            }

            if (product != null)
            {
                product.Id_ProductDataImage.OriginalImageData = new byte[0];
                product.Category_Id.Image_Category.OriginalImageData = new byte[0];
            }
            return product;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Отправляем добавленный продукт в Kafka
            await _producer.SendProductUpdateAsync(product);
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

            // Обновляем данные продукта в Kafka
            await _producer.SendProductUpdateAsync(existingProduct);
            return true;
        }

        public async Task<bool> DeleteProductAsync(long id)
        {
            var product = await GetProductByIdAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            // Удаляем продукт в Kafka
            await _producer.SendProductDeleteAsync(id);
            return true;
        }
    }
}


